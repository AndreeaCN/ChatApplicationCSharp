using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServersClasses
{
    [Serializable]
   public class ActiveChatServ
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public ActiveChatServ()
        {

        }
        
        public ActiveChatServ(string ipAddr, int port)
        {
            IpAddress = ipAddr;
            Port = port;
        }
        public override string ToString()
        {
            return string.Format("{0},{1}", IpAddress, Port);
        }
    }
}
