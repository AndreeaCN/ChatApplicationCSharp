using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ServerChat
{
    public class HandleClient
    {
        TcpClient clientSocket;
        NetworkStream networkStream;
        string clNo;
        bool leavingFlag = false;
        

        public void startClient(TcpClient inClientSocket, string clientName)
        {
            clientSocket = inClientSocket;
            clNo = clientName;            
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            byte[] bytesFrom = new byte[4096];
            string dataFromClient = null;
          
            while(!leavingFlag)
            {
                try
                {                    
                    networkStream = clientSocket.GetStream();
                    int count = networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    dataFromClient = Encoding.ASCII.GetString(bytesFrom, 0, count);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    string cmd = new string(dataFromClient.Take(4).ToArray());
                    if (cmd == "200L" )
                    {
                        Console.WriteLine(clNo + " is leaving the chat");
                        Program.broadcast(clNo + " is leaving the chat", clNo, 2);
                        // break the while loop
                        leavingFlag = true;
                        
                    }
                    else
                    {
                        Console.WriteLine(clNo + " : " + dataFromClient);
                        Program.broadcast(clNo + " : " + dataFromClient, clNo, 1);
                    }              
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }//end while
            networkStream.Close();
            clientSocket.Close();
            return;
        }//end doChat
    }
}
