﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace NoGameFoundClient
{
    class ServerConnection
    {
        Socket server;
        IPAddress ip;
        IPEndPoint port;
        bool connected = false;
        static ServerConnection connection = null;

        //initializes server connection
        private ServerConnection()
        {
            this.ip = IPAddress.Parse("147.83.117.22");
            this.port = new IPEndPoint(this.ip, 5060);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        //returns server connection
        public static ServerConnection getInstance()
        {
          
            if (connection == null)
            {
                connection = new ServerConnection();
            }
            return connection;
        }


        //conects to server
        public int ConnectToServer()
        {
            Debug.Log("Connecting to server");
            connected = true;
            try
            {
                server.Connect(port);//Intentamos conectar el socket     
                Console.WriteLine("Connected");
                   
                return 0;
            }
            catch (SocketException)
            {
                Console.WriteLine("Server Connection Error");
                return -1;

            }
        }

        //sends message to server
        public void SendMessage(String message)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
            server.Send(msg);
        }

        //listens for message from server
        public String ListenForMessage()
        {
          
                byte[] data = new byte[1024];
                int dataSize = server.Receive(data);
                String msg = Encoding.ASCII.GetString(data, 0, dataSize);
                return msg;
           
        }
        
        //closes connection with server
        public void DisconnectFromServer()
        {
            string mensaje = "0/";

            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        public bool isConnected()
        {
            return connected;
        }
        public void setConnected(bool connected)
        {
            this.connected = connected;
        }

   
    }

}
