using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ServersClasses;

namespace MiddleWare
{
    class Program
    {
       

        static void Main(string[] args)
        {
            string servListAsXml = null;           
            List<ActiveChatServ> listServs = new List<ActiveChatServ>();
            
            //Console.ReadLine();

            TcpListener MWServ = null;
       
            try
            {
                // Set port and address for the middleware serv
                int port = 5000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                
                // TcpListener server = new TcpListener(port);
                MWServ = new TcpListener(localAddr, port);

                // Start listening for client requests.
                MWServ.Start();

                // Buffer for reading data
                byte[] bytes = new byte[4096];
                string data = null;
             //   string address = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = MWServ.AcceptTcpClient();

                    Console.WriteLine("Connected!");
                    data = null;
                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();
                    int count = stream.Read(bytes, 0, bytes.Length);
                    // use count to determine the length of the string
                    data = Encoding.ASCII.GetString(bytes, 0, count);
                    // Read up to the terminator character $
                    data = data.Substring(0, data.IndexOf("$"));
                    // check if its a message from a ChatServer
                    if (data.Contains("REG"))
                    {
                        string[] serverReg = data.Split(',');
                        listServs.Add(new ActiveChatServ(serverReg[1], int.Parse(serverReg[2])));
                        Console.WriteLine("Added new server {0}, {1}", serverReg[1], serverReg[2]);
                        string confirmation = "OK$";
                        byte[] confirmByte = new byte[4096];
                        confirmByte = Encoding.ASCII.GetBytes(confirmation);
                        stream.Write(confirmByte, 0, confirmByte.Length);
                        stream.Flush();
                        stream.Close();
                        client.Close();
                    }
                    else
                    {                    
                        XmlSerializer serializer = new XmlSerializer(typeof(List<ActiveChatServ>));
                        using (StringWriter writer = new StringWriter())
                        {
                            serializer.Serialize(writer, listServs);
                            servListAsXml = writer.ToString();
                        }

                        Console.WriteLine(servListAsXml);
                        Console.WriteLine("Client says {0}", data);
                        string msg = servListAsXml + "$";
                        byte[] returnBytes = new byte[4096];
                        returnBytes = Encoding.ASCII.GetBytes(msg);
                        stream.Write(returnBytes, 0, returnBytes.Length);
                        stream.Flush();
                        Console.WriteLine("Sent address as : {0}", msg);
                        // close the stream
                        stream.Close();
                        // Shutdown and end connection
                        client.Close();
                    }// end else
                } // end while true
            } // end try
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                MWServ.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
