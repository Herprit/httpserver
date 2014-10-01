using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{

    public class HttpServer
    {
        private const string RN = "\r\n"; // skifterlinje 

        private static readonly string RootCatalog = @"c:/Users/Herprit/Desktop/WebServer/TEST.HTML";

        public static readonly int DefaultPort = 8080;

        private TcpClient connectionSocket;
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

           
            
            //Spørger efter fil findes!
            if(File.Exists(RootCatalog))
            {
                string answer = "HTTP/1.0 200 OK \r\n\r\n";
                sw.Write(answer);
                
                //Læser fra flien på computeren. Lavet en try/catch, hvis nu filen ikke findes
                try
                {
                    FileStream fs = File.OpenRead(RootCatalog);
                    fs.CopyTo(sw.BaseStream);
                    sw.BaseStream.Flush();
                    sw.Flush();

                }
                catch (Exception)
                {
                   
                }

            }

            else
            {
                string answer1 = "HTTP/1.0 404 Not Found" + RN + RN;
                sw.Write(answer1);
                Console.WriteLine("File Not Found");
            }


            connectionSocket.Close();
        }


    }
}
