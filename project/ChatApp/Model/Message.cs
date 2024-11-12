using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model
{
    public class Message
    {
        // chat, connect_request, connect_accept, connect_deny, disconnect, buzz
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string sender;
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string contents;
        public string Contents
        {
            get { return contents; }
            set { contents = value; }
        }

        public Message(String type, String sender, String date = null, String contents = "")
        {
            this.type = type;
            this.sender = sender;
            this.date = date;
            this.contents = contents;

            // Set date to now
            if (date == null) { this.date = DateTime.Now.ToString("s", DateTimeFormatInfo.InvariantInfo); }
        }

        public override string ToString()
        {
            if (type == "buzz")
            {
                return "[" + sender + " BUZZED]";
            }
            else if (type == "")
            {
                return "[" + sender + "]: " + contents;
            }
            return "[" + sender + "]: " + contents;
        }
    }
}
