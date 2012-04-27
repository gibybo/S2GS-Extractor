using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace s2gsAutomationForms
{
    class Server
    {
        private Server() { }
        static Server()
        {
            thisUS = new Server();
            thisUS.url = urlTemplate.Replace("{server}", "US");
            thisUS.name = "America";
            thisUS.magic = new byte[] { 0x73, 0x32, 0x67, 0x73, 0x00, 0x00, 0x55, 0x53 };

            thisEU = new Server();
            thisEU.url = urlTemplate.Replace("{server}", "EU");
            thisEU.name = "Europe";
            thisEU.magic = new byte[] { 0x73, 0x32, 0x67, 0x73, 0x00, 0x00, 0x45, 0x55 };

        }

        static string urlTemplate = "http://{server}.depot.battle.net:1119/{hash}.s2gs";

        static Server thisEU;
        static Server thisUS;

        string url;
        public string Url
        {
            get { return url; }
        }
        byte[] magic;
        public byte[] Magic
        {
            get { return magic; }
        }
        string name;
        public string Name
        {
            get { return name; }
        }

        public static Server EU
        {
            get
            {
                return thisEU;
            }
        }
        public static Server US
        {
            get
            {
                return thisUS;
            }
        }

        public static Server[] Servers
        {
            get
            {
                return new Server[] { EU, US };
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
