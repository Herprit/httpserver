using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EventLogExample;

namespace httpserver
{


    public class HttpServer
    {
        private const string RN = "\r\n"; // skifterlinje  //clrf

        private string date = "Date: " + DateTime.Now.ToString();

        private string Length = "content lenght: ";

        private string contentType = "Content type: text/html";

        private static readonly string RootCatalog = @"c:/Users/Herprit/Desktop/WebServer/1COpgave.HTML";

        public static readonly int DefaultPort = 8080;

        public TcpClient connectionSocket;

        private string[] _stringA;


        //Constructor til classen, som har en connectionsocket som parameter.
        public HttpServer(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }


        // ConnectionHandler
        public void connectionHandler()
        {
            NetworkStream ns = connectionSocket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;



            string statuslinee = GetStatusLine(sr);
            if (statuslinee == null)
            {
                return;
            }

            string getfile = Getfile(sw);
            if (getfile == null)
            {
                return;
            }

            connectionSocket.Close();
            ns.Close();


        }

        //Getfile
        public string Getfile(StreamWriter sw)
        {
            //Spørger efter fil findes!
            if (File.Exists(RootCatalog))
            {
                string statusline = "HTTP/1.0 200 OK \r\n\r\n";
                sw.Write(statusline);
                Console.WriteLine("HTTP/1.0 200 OK" + RN + date);

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
                Console.WriteLine("HTTP/1.0 200 OK");
                string answer1 = "HTTP/1.0 404 Not Found" + RN + RN + date;
                sw.Write(answer1);
                Console.WriteLine("File Not Found");

                Log.WriteInfo("Response Fil not found");
            }

            return "HTTP/1.0 404 Not Found";
        }

        //GetstatusLine request
        public string GetStatusLine(StreamReader sr)
        {
            string message = sr.ReadLine();
            Console.WriteLine("Request: " + message + RN + date + RN + Length + RootCatalog.Length + RN + contentType + RN);
            Log.WriteInfo("Client Request");

            //split /
            string[] splitAR = new string[3];
            splitAR = message.Split(' ');
            Console.WriteLine(" " + splitAR.GetValue(1));
            return "HTTP/1.0 200 OK" + RN + date;


        }

        // kunne lave en reponse metode for mere overskulighed. 

        //public string response(StreamWriter sw)
        //{
        //    string statusline = "HTTP/1.0 200 OK \r\n\r\n";
        //    sw.Write(statusline);
        //    Console.WriteLine("HTTP/1.0 200 OK" + RN + date);
        //    return "HTTP/1.0 200 OK";
        //}
    }

}



