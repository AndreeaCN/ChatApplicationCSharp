using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace ServerChat
{
    // adapted from
    //http://csharp.net-informations.com/communications/csharp-chat-server-programming.htm
    class Program
    {
        // data structure to hold the clients
        // takes in a string - the name - and a TCPClient object
        public static Dictionary<string, TcpClient> ClientList = new Dictionary<string, TcpClient>();
        public static bool someoneLeaving = false;
        public static int port;
     

        static int Main(string[] args)
        {

            // Test if input arguments were supplied:
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a port number");                
                return 1;
            }
            bool test = int.TryParse(args[0], out port);
            if (test == false)
            {
                Console.WriteLine("Please enter a port number in numeric form....");                
                return 1;
            }
            //Contact the MW server to let it know we re here#
            Registration();

            // instantiate the TCP client ( socket)
            TcpClient clientSocket = new TcpClient();
            TcpListener server = null;
            try
            {
                // set the port and local address of the server
              //  int port = 5001;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");                        

                // Create the server socket
                server = new TcpListener(localAddr, port);
                // Start listening for client requests.
                server.Start();
                Console.WriteLine("Chat Server Started with IP:{0} and Port: {1}",localAddr, port);
                // Buffer for reading data
                byte[] bytesFrom = new byte[4096];
                string dataFromClient = null;

                // Enter the listening loop.
                while (true)
                {                   
                    Console.Write("Waiting for a connection... ");
                    // Perform a blocking call to accept requests.                   
                    clientSocket = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    dataFromClient = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = clientSocket.GetStream();
                   // Read from the stream and save the number of bytes read as count
                    int count = stream.Read(bytesFrom, 0, bytesFrom.Length);
                   // use count to determine the length of the string
                    dataFromClient = Encoding.ASCII.GetString(bytesFrom, 0, count);
                    // Read up to the terminator character $
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    // add the client to the Dictionary
                    ClientList[dataFromClient] = clientSocket;
                   
                    // broadcast the Joined message to everyone including the just joined client
                    broadcast("JOIN"+dataFromClient, dataFromClient, 0);
                    
                    Console.WriteLine(dataFromClient + " Joined chat room ");
                    // create a handler object to deal with the chatting for the client on a new thread
                    HandleClient client = new HandleClient();
                    client.startClient(clientSocket, dataFromClient);                                
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Shutdown and end connection
                clientSocket.Close();
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("exit");
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
            return 0;
        }

        public static void Registration()
        {
            TcpClient socketToMW = new TcpClient();
            socketToMW.Connect("127.0.0.1", 5000);
            string msgForMW = "REG,127.0.0.1," + port + "$";
            NetworkStream streamMW = socketToMW.GetStream();
            byte[] msgForMWByte = new byte[500];
            msgForMWByte = Encoding.ASCII.GetBytes(msgForMW);
            // send the request to the middleware
            streamMW.Write(msgForMWByte, 0, msgForMWByte.Length);
            streamMW.Flush();
            int c = streamMW.Read(msgForMWByte, 0, msgForMWByte.Length);
            string returndata = Encoding.ASCII.GetString(msgForMWByte, 0, c);
            // take the string up to the terminator $
            returndata = returndata.Substring(0, returndata.IndexOf("$"));
            Console.WriteLine(returndata);
            streamMW.Close();
            socketToMW.Close();
           
        }

        /// <summary>
        /// Function to send messages to all connected clients
        /// </summary>
        /// <param name="msg"> The message read from the stream</param>
        /// <param name="uName">The nickname of the user</param>
        /// <param name="flag"> The flag(bool) that indicates what sort of message it is. False for a joining message and true for a regular message </param>

      
        public static void broadcast(string msg, string uName, int flag)
        {
            TcpClient broadcastSocket;
            
            
            if (flag == 2)
            {
                broadcastSocket = ClientList[uName];
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                byte[] broadcastBytes = null;
                broadcastBytes = Encoding.ASCII.GetBytes("SERVOK"+" $");
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
                broadcastStream.Close();
                broadcastSocket.Close();
                ClientList.Remove(uName);
            }
           
            foreach (TcpClient clientSock in ClientList.Values)
            {
                broadcastSocket = clientSock;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                byte[] broadcastBytes = null;
                // Determine the type / format of message to send
                broadcastBytes = Encoding.ASCII.GetBytes(msg);
                // write it to each of the sockets in the hashtable
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();

            }
           
            
            
            // iterate through all the TCP Clients saved in the dictionary
           
          
        }
    }
}
