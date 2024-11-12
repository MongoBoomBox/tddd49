using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows;

// NOT MODEL

namespace ChatApp.Model
{
    public class TCP
    {
        TcpClient client;
        NetworkStream stream;
        int port;
        string adress;

        public TCP(string IPAdress, int port)
        {
            adress = IPAdress;
            this.port = port;
        }

        //public void sendMessage(Message message)
        //{
        //    // get message length - send
        //    // send message
        //    // ===================
        //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message.Contents);
        //    stream.Write(msg, 0, msg.Length); // CATCH EXCEPTION HERE
        //}

        //public event EventHandler<MessageRecievedEventArgs> messageRecieved;

        //public void recieveMessage()
        //{
        //    try
        //    {
        //        // Buffer for reading data
        //        Byte[] bytes = new Byte[256];
        //        String data = null;
        //        int i;

        //        // Loop to receive all the data sent by the client.
        //        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        //        {
        //            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

        //            // Message recieved event containing message
        //            messageRecieved?.Invoke(this, new MessageRecievedEventArgs(new Message("Other", "X", data)));
        //        }
        //    }
        //    catch (IOException e)
        //    {
        //        // close()
        //        // throw event closed
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }

        //}

        public void startServer()
        {
            TcpListener server = null;

            try
            {
                server = new TcpListener(IPAddress.Parse(adress), port);

                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                Console.Write("Waiting for a connection... ");

                client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                stream = client.GetStream();

                /*
                while (true)
                {
                    string message = recieveMessage();
                    Console.WriteLine(message);
                }
                */
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

        public void startClient()
        {
            try
            {
                // Create TCP client
                client = new TcpClient();

                while (!client.Connected)
                {
                    client.Connect(adress, port);
                }

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();
                stream = client.GetStream();

                /*
                while (true)
                {
                    string message = Console.ReadLine();
                    sendMessage(message);
                }
                */
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }

        public void close()
        {
            client.Close();
            stream.Close();
        }
    }

    /*
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageRecievedEventArgs(Message m)
        {
            message = m;
        }
        
        public Message message { get; set; }
    }
    */
}
