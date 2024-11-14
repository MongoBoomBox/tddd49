// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Diagnostics;
// using System.Linq;
// using System.Net;
// using System.Net.Sockets;
// using System.Runtime.CompilerServices;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows;

// namespace ChatApp.Model
// {
//     internal class NetworkManager : INotifyPropertyChanged
//     {

//         private NetworkStream stream;

//         public event PropertyChangedEventHandler PropertyChanged;

//         private void OnPropertyChanged(string propertyName = "")
//         {
//             if (PropertyChanged != null)
//             {
//                 PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

//             }
//         }

//         private string message;
//         public string Message
//         {
//             get { return message; }
//             set { message = value; OnPropertyChanged("Message"); }
//         }

//         public void startServerAsHost(string ip, int port)
//         {
//             Task.Factory.StartNew(() => {
//                 var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
//                 TcpListener server = new TcpListener(ipEndPoint);
//                 TcpClient endPoint = null;
//                 try
//                 {
//                     server.Start();
//                     System.Diagnostics.Debug.WriteLine("Start listening...");
//                     endPoint = server.AcceptTcpClient();
//                     System.Diagnostics.Debug.WriteLine("Connection accepted!");
//                     handleConnection(endPoint);
//                 }
//                 catch 
//                 {
//                     Debug.WriteLine("Error: ");
//                 }
//             });            
//         }

//         public void joinServerAsClient(string ip, int port)
//         {
//             Task.Factory.StartNew(() => {
//                 var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
//                 TcpListener server = new TcpListener(ipEndPoint);
//                 TcpClient endPoint = new TcpClient();
//                 try
//                 {
//                     System.Diagnostics.Debug.WriteLine("Connecting to the server...");
//                     endPoint.Connect(ipEndPoint);
//                     System.Diagnostics.Debug.WriteLine("Connection established!");
//                     handleConnection(endPoint);
//                 }
//                 finally                    
//                 {
//                     endPoint.Close();
//                 }
//             }); 
//         }

//         public void startConnection(string ip, int port, string type)
//         {


//             Task.Factory.StartNew(() =>
//             {
//                 bool secondTry = false;
//                 var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
//                 TcpListener server = new TcpListener(ipEndPoint);
//                 TcpClient endPoint = null;
//                 try
//                 {

//                     server.Start();
//                     System.Diagnostics.Debug.WriteLine("Start listening...");
//                     endPoint = server.AcceptTcpClient();
//                     System.Diagnostics.Debug.WriteLine("Connection accepted!");
//                     handleConnection(endPoint);

//                 }
//                 catch
//                 {

//                     secondTry = true;
//                 }


//                 if (secondTry)
//                 {

//                     endPoint = new TcpClient();
//                     try
//                     {
//                         System.Diagnostics.Debug.WriteLine("Connecting to the server...");
//                         endPoint.Connect(ipEndPoint);
//                         System.Diagnostics.Debug.WriteLine("Connection established!");
//                         handleConnection(endPoint);
//                     }
//                     finally                    {
//                         endPoint.Close();

//                     }
//                 }
//             });

//         }
//         private void handleConnection(TcpClient endPoint)
//         {
//             stream = endPoint.GetStream();
//             while (true)
//             {
//                 var buffer = new byte[1024];
//                 int received = stream.Read(buffer, 0, 1024);
//                 var message = Encoding.UTF8.GetString(buffer, 0, received);
//                 this.Message = message;

//             }

//         }
//         public void sendChar(string str)
//         {
//             Task.Factory.StartNew(() =>
//             {
//                 var buffer = Encoding.UTF8.GetBytes(str);
//                 stream.Write(buffer, 0, str.Length);
//             });
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Model
{
    internal class NetworkManager : INotifyPropertyChanged
    {

        private NetworkStream stream;
        private TcpClient client;
        // private StreamWriter writer;
        // private StreamReader reader;

        public bool listeningForUser;
        public bool connected;
        public bool connecting;

        public bool connectAccept;
        public bool connectDeny;



        public bool? clientvalue = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        public void startServerAsHost(string ip, int port)
        {
            listeningForUser = true;

            Task.Factory.StartNew(() => {
                try
                {
                    var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    TcpListener server = new TcpListener(ipEndPoint);
                    server.Start();
                    while (listeningForUser)
                    {
                        client = server.AcceptTcpClient()
                        stream = client

                    }
                }
            });

            Task.Factory.StartNew(() => {
                var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                TcpListener server = new TcpListener(ipEndPoint);
                try
                {
                    server.Start();
                    System.Diagnostics.Debug.WriteLine("Start listening...");
                    client = server.AcceptTcpClient();
                    System.Diagnostics.Debug.WriteLine("Connection accepted!");
                    handleConnection(client);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(("Error in startServerAsHost: " + e));
                }
                finally
                {
                    Debug.WriteLine(("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
                    Console.WriteLine(("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
                    server.Stop();
                }
            });
        }

        public void joinServerAsClient(string ip, int port)
        {
            Task.Factory.StartNew(() => {
                var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                client = new TcpClient();
                // try
                // {
                //     while (!endPoint.Connected)
                //     {
                //         endPoint.Connect(ipEndPoint);
                //     }

                //     clientvalue = true;
                //     handleConnection(endPoint);
                // }
                // catch (Exception e)
                // {
                //     clientvalue = false;
                //     Console.WriteLine("Exception: ", e);
                // }

                // finally                    
                // {
                //     endPoint.Close();
                //     clientvalue = false;
                // }

                try
                {
                    System.Diagnostics.Debug.WriteLine("Connecting to the server...");
                    client.Connect(ipEndPoint);
                    System.Diagnostics.Debug.WriteLine("Connection established!");
                    clientvalue = true;
                    handleConnection(client);

                }
                catch (Exception e)
                {
                    clientvalue = false;
                    Debug.WriteLine(("Error in joinServerAsClient: " + e));
                }
                finally
                {

                    Debug.WriteLine(("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB"));
                    Console.WriteLine(("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB"));
                    client.Close();
                    clientvalue = null;
                }
            });


        }



        // public void startConnection(string ip, int port, string type)
        // {


        //     Task.Factory.StartNew(() =>
        //     {
        //         bool secondTry = false;
        //         var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        //         TcpListener server = new TcpListener(ipEndPoint);
        //         TcpClient endPoint = null;
        //         try
        //         {

        //             server.Start();
        //             System.Diagnostics.Debug.WriteLine("Start listening...");
        //             endPoint = server.AcceptTcpClient();
        //             System.Diagnostics.Debug.WriteLine("Connection accepted!");
        //             handleConnection(endPoint);

        //         }
        //         catch
        //         {

        //             secondTry = true;
        //         }


        //         if (secondTry)
        //         {

        //             endPoint = new TcpClient();
        //             try
        //             {
        //                 System.Diagnostics.Debug.WriteLine("Connecting to the server...");
        //                 endPoint.Connect(ipEndPoint);
        //                 System.Diagnostics.Debug.WriteLine("Connection established!");
        //                 handleConnection(endPoint);
        //             }
        //             finally                    {
        //                 endPoint.Close();

        //             }
        //         }
        //     });

        // }


        private void handleConnection(TcpClient endPoint)
        {
            stream = endPoint.GetStream();
            while (true)
            {
                var buffer = new byte[1024];
                int received = stream.Read(buffer, 0, 1024);
                var message = Encoding.UTF8.GetString(buffer, 0, received);
                this.Message = message;

            }

        }
        public void sendChar(string str)
        {
            Task.Factory.StartNew(() =>
            {
                var buffer = Encoding.UTF8.GetBytes(str);
                stream.Write(buffer, 0, str.Length);
            });
        }
    }
}
