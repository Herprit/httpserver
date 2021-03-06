﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventLogExample;

namespace httpserver
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //opretter forbindelse til port ; 8080. Starter socket.

            TcpListener serverSocket = new TcpListener(8080);
            serverSocket.Start();

            Log.WriteInfo("Server Startet");

            ////kan accepter flere clienter på en gang.
            while (true)
            {

              TcpClient  tcpconnection = serverSocket.AcceptTcpClient();
                Task.Factory.StartNew(() =>
                {
                    HttpServer httpServer = new HttpServer(tcpconnection);
                    httpServer.connectionHandler();

                    Thread.Sleep(100);
                    Log.WriteInfo("Nyclient");

                    //tcpconnection lukket! derefter kører den igennem igen.
                    tcpconnection.Close();

                });

            }

        }



    }
}





