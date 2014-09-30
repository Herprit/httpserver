using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
   
    public class HttpServer
    {
        private const string RN = "\r\n"; // skifterlinje

        public static readonly int DefaultPort = 8080;

        TcpClient connectionSocket;
        private string[] _stringA;

        public HttpServer(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        public void steams()
        {
            NetworkStream ns = connectionSocket.GetStream();
           
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; 

            // request
            string message = sr.ReadLine();
            Console.WriteLine("Client: " + message);
             
            //split /
            string[] splitAR = new string[3];
            splitAR = message.Split(' ');
            Console.WriteLine("tester: " + splitAR.GetValue(1));
   

            //Response
            string answer = "GET /HTTP/1.0 200 OK\r\n\r\nHello World";
            sw.WriteLine(answer);
            Console.WriteLine(answer);

       
           



        }

    }
}
