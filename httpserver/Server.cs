using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Server
    {
        static void Main(string[] args)
        {
             TcpListener serverSocket = new TcpListener(8080);
             serverSocket.Start();

            TcpClient tcpConnection = serverSocket.AcceptTcpClient();
            Console.WriteLine("Server Online");

            HttpServer httpServer = new HttpServer(tcpConnection);

            httpServer.steams();
           
          
            tcpConnection.Close();
        }
    }
}
