using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Collections.Generic;
using System.Xml.Serialization;
using ServersClasses;
using System.IO;

namespace ClientChat
{
    // adapted from :
    // http://csharp.net-informations.com/communications/csharp-chat-server-programming.htm
    /// <summary>
    //
    /// </summary>
    public partial class ChatWindow : Window
    {
        string readData = null;
        bool leaving = false;
       NetworkStream serverStream;

        public ChatWindow(TcpClient clientSocket)
        {
            InitializeComponent();
            serverStream = clientSocket.GetStream();
            Thread ctThread = new Thread(getMessage);
            ctThread.Start();

        }

        //Handler for the Send button. Gets the text from the messageBox, adds a $ terminating character and 
        // writes it to the Network Stream
        private void sendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] outStream =Encoding.ASCII.GetBytes(messageBox.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            messageBox.Text = " "; // reset the chat box
        }
        //Function to get the messages sent by the server from the other chat clients
        //Is run on background by a different thread
        private void getMessage()
        {
            while (!leaving)
            {
                // set a buffer
                byte[] inStream = new byte[4096];  
                // read from the network stream              
                int count = serverStream.Read(inStream, 0, inStream.Length);
                // use the count variable to get the string from bytes
                string returndata = Encoding.ASCII.GetString(inStream,0, count);
                // check if the string contains the SERVOK message that means the client has been disconnected
                if (returndata.Contains("SERVOK"))
                {
                    readData = "Disconnected from Chat";
                    // end the while loop 
                    leaving = true;                    
                }                              
                readData = returndata;
                msg();     // call the function that updates the UI                        
            }
            // out of the while loop, client must be leaving, close the stream and the socket
            serverStream.Close();            
            // call return to get the thread to exit as well
            return;
        }

        // The function that updates the chatView with the messages from everyone
        private void msg()
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => msg());
            else
            {
                // If message contains join is a user joining so update only the user box                
                if(readData.Contains("JOIN"))
                {
                    string user = readData.Substring(4);
                    connectedUsersBox.Text += user + Environment.NewLine;
                }
                else
                    chatView.Text += Environment.NewLine + readData;
            }                        
        }


        // Function to handle the Leave Chat button
        // Sends a message to the server that starts with 200L so the server knows is a leaving message
        private void leaveBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] outStream = Encoding.ASCII.GetBytes("200L"+ "user is leaving" + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();       
        }     
   
    }
}
