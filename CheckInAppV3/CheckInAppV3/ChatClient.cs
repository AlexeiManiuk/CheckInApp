using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CheckInAppV3.checkInRef;

namespace CheckInAppV3
{
    public class ChatClient
    {
        private string CurrentUser { get; set; }
        private string SendTo { get; set; }
        private string Message { get; set; }
        private DateTime SendTime { get; set; }

        public ChatClient(string currentUser, string sendTo)
        {
            CurrentUser = currentUser;
            SendTo = sendTo;
        }
        


        public void SendMessage()
        {
            Message = "Hello!";
            Check_InService client = new Check_InService();
            
        }

    }
}