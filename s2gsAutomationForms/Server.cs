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

            thisKR = new Server();
            thisKR.url = urlTemplate.Replace("{server}", "KR");
            thisKR.name = "Korea";
            thisKR.magic = new byte[] { };

            thisSEA = new Server();
            //thisSEA.url = urlTemplate.Replace("{server}", ""); //SEA.depot.battle.net:1119 isn't the right one.
            thisSEA.name = "South-East Asia";
            thisSEA.magic = new byte[] { };
        }

        static string urlTemplate = "http://{server}.depot.battle.net:1119/{hash}.s2gs";

        static Server thisEU;
        static Server thisUS;
        static Server thisKR;
        static Server thisSEA;

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
        public static Server KR
        {
            get
            {
                return thisKR;
            }
        }
        
        public static Server SEA
        {
            get
            {
                return thisSEA;
            }
        }

        public static Server[] Servers
        {
            get
            {
                return new Server[] { US, EU };
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
