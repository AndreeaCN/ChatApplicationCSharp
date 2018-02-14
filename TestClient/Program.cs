using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServersClasses;
using System.Xml.Serialization;
using System.IO;

namespace TestClient
{
    class Program
    {       
        static void Main(string[] args)
        {
            try
            {
                // create TCP client
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");
                // Connect to the middleware server
                tcpclnt.Connect("127.0.0.1", 5000);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
                Console.Write("Enter the string to be transmitted : ");
                // enter any string, does not matter for this test                
                string str = Console.ReadLine() + "$";
                // get the network stream
                NetworkStream stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");
                // write the bytes to the middleware serv
                stm.Write(ba, 0, ba.Length);
                stm.Flush();
                string listOfServs = null;
                // get buffer ready for reading from the middleware
                byte[] bb = new byte[4096];
                int k = stm.Read(bb, 0, bb.Length);
                listOfServs = Encoding.ASCII.GetString(bb, 0, k);
                // read up to the $ terminator
                listOfServs = listOfServs.Substring(0, listOfServs.IndexOf("$"));
                // create a serializer obj that takes in a list of servers
                XmlSerializer serializer = new XmlSerializer(typeof(List<ActiveChatServ>));
                // deserialise the list of servers into the server list
                List<ActiveChatServ> ServList = serializer.Deserialize(new StringReader(listOfServs)) as List<ActiveChatServ>;
                // Just for testing, print to console the results
                foreach(ActiveChatServ serv in ServList)
                {
                    Console.WriteLine("The ip address is: {0}", serv.IpAddress);
                    Console.WriteLine("The port number is: {0}", serv.Port);
                }               

                Console.WriteLine("Hit Enter to Close");
                Console.ReadLine();

                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
       
    }
}
