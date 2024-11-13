using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using ChatApp.Model;
using ChatApp.ViewModel.Commands;
using ChatApp.View;
using System.Diagnostics;

namespace ChatApp.ViewModel
{
    internal class ChatViewModel : INotifyPropertyChanged
    {

        internal string name = "NAME";
        internal int port = 3000;
        internal string ip = "127.0.0.1";


        internal string chat = "test123";


        private NetworkManager NetworkManager { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ChatViewModel(NetworkManager networkManager)
        {
            NetworkManager = networkManager;
            // networkManager.PropertyChanged += myModel_PropertyChanged;
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
                OnPropertyChanged("Port");
            }

        }

        public string IP {
            
            
            get
            {
                return ip;
            }
            set
            {
                ip = value;
                OnPropertyChanged("IP");
            } 
        }

        public string Chat
        {
            get
            {
                return chat;
            }
            set
            {
                chat = value;
                OnPropertyChanged("Chat");
            }
        }

        // private void startConnection()
        // {
        //     NetworkManager.startConnection(ip, port, "");
        // }

        private ICommand host;
        public ICommand Host
        {
            get
            {
                if (host == null)
                {
                    host = new HostCommand(this);
                }
                return host;
            }
            set
            {
                host = value;
            }
        }

        public void hostServer()
        {
            try
            {
                //Thread t = new Thread(startConnection);
                //t.Start();
                //gör det som ska göras
                NetworkManager.startServerAsHost(ip,port);
                ChatView chat = new ChatView();
                chat.DataContext = this;
                chat.Show();
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error in hostServer: " + e);
            }
        }
        private ICommand join;
        public ICommand Join
        {
            get
            {
                if (join == null)
                {
                    join = new JoinCommand(this);
                }
                return join;
            }
            set
            {
                join = value;
            }
        }

        public void joinServer()
        {  
            NetworkManager.joinServerAsClient(ip, port);
            
            while( NetworkManager.clientvalue == null)
            {}
            if (NetworkManager.clientvalue == true)
            { 
              
                ChatView chat = new ChatView();
                chat.DataContext = this;
                //chat.ShowDialog();
                chat.Show();
            }
            else
            {
                MessageBox.Show("Server at ip " + ip + " and port " + port + " is not open!");
                NetworkManager.clientvalue = null;
            }
        }
    }
}
