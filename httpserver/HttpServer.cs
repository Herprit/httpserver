using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        public static readonly int DefaultPort = 8080;
        

        private TcpClient connectionSocket;

        public HttpServer(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }


        

    }
}
