using ServersClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ClientChat
{
    /// <summary>
    /// Interaction logic for LogToChat.xaml
    /// </summary>
    public partial class LogToChat : Window
    {
        // socket and stream to middleware server
        TcpClient socketToMWServ;
        NetworkStream mwstream;

        // socket and stream to the chat server
        TcpClient clientSocket;
        NetworkStream serverStream;

        // a list to hold the Active servers received from Middleware
        public List<ActiveChatServ> listOfServers;
        public string IpAddress;
        public int Port;
        public LogToChat()
        {
            InitializeComponent();           
        }
            
        // Click Handler for the Search for servers button
        private void servSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // instantiate and connect to the middleware socket
                socketToMWServ = new TcpClient();
                mwstream = default(NetworkStream);
                socketToMWServ.Connect("127.0.0.1", 5000);
                // form a message with REQ to ask for a list of servers available
                string msgToMW = "REQ servers $";
                // get the stream
                mwstream = socketToMWServ.GetStream();
                // encode and send the message
                byte[] msgToMWByte = Encoding.ASCII.GetBytes(msgToMW);                
                mwstream.Write(msgToMWByte, 0, msgToMWByte.Length);
                mwstream.Flush();
                // read back the middleware response
                byte[] availableServsByte = new byte[4096];
                int count = mwstream.Read(availableServsByte, 0, availableServsByte.Length);
                string returndata = Encoding.ASCII.GetString(availableServsByte, 0, count);

                // take the string up to the terminator $
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                // create a XML serializer object 
                XmlSerializer serializer = new XmlSerializer(typeof(List<ActiveChatServ>));
                // deserialise the list of servers from XML and save them into the listOfServers
                listOfServers = serializer.Deserialize(new StringReader(returndata)) as List<ActiveChatServ>;
                // variable to help populate the list of active servers
                int i = 0;
                // Create a button for each available server
                foreach (ActiveChatServ server in listOfServers)
                {
                    i++;
                    Button servBtn = new Button();
                    servBtn.Content = "Room "+i + " (" +server.IpAddress+", "+server.Port+")";
                    // making it pretty
                    servBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    servBtn.VerticalAlignment = VerticalAlignment.Center;
                    servBtn.Background = new SolidColorBrush(Colors.BlueViolet);
                    servBtn.Foreground= new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    servBtn.Padding = new Thickness(2);
                    servBtn.Margin = new Thickness(5);
                    // add Click Handler for each one
                    servBtn.Click += (o, ev) =>
                    {
                        MakeConnection(server.IpAddress, server.Port);
                    };
                    // add them to the layout container
                    serverList.Children.Add(servBtn);
                }

                // hide the get servers button as we dont need it anymore
                servSearchBtn.Visibility = Visibility.Collapsed;
                welcomeBlock.Visibility = Visibility.Collapsed;
                // show the servers instead       
                secondWelcome.Visibility = Visibility.Visible;
                serverList.Visibility = Visibility.Visible;
                nicknameBox.Visibility = Visibility.Visible;

            }
            catch(SocketException)
            {
                // not implemented
            }
            finally
            {
                // Close the middleware stream and socket as we dont need them anymore
                mwstream.Close();
                socketToMWServ.Close();
            }         

        }
        // Click Handler function for Server buttons
        // Creates socket, connects and sends a message to the Chat server with the nickname
        // also opends the Chat Window and hides the log on window
        private void MakeConnection(string ipAddress, int port)
        {
            clientSocket = new TcpClient();                
            clientSocket.Connect(ipAddress, port);
            // get the network stream
            serverStream = clientSocket.GetStream();
            // send the Nickname terminated with $
            byte[] outStream = Encoding.ASCII.GetBytes(nicknameBox.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            // When all is done show the Chat window
            ChatWindow ChatWindow = new ChatWindow(clientSocket);
            // Set the welcome message
            ChatWindow.welcomeMsg.Text = ("Hello " + nicknameBox.Text + ", you are now connected to Chat Room: " + ipAddress + "," + port);
            ChatWindow.Show();
            // hide the logon window
            this.Visibility = Visibility.Hidden;           
          
        }
    }
}
