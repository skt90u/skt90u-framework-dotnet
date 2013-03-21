using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JUtil.EmailSender {
    /// <summary>
    /// See http://stackoverflow.com/questions/704636/sending-email-through-gmail-smtp-server-with-c
    /// </summary>
    public class GmailSender: SmtpSender {
        
        public static void UT()
        {
            GmailSender sender = new GmailSender(
                "skt90u@gmail.com", 
                "490410056");

            sender.AsyncSend = true;

            Message message = new Message("skt90u@gmail.com", "skt90u@gmail.com", DateTime.Now.ToString(), "初中的體育老師說：誰敢再穿裙子上我的課，就罰她倒立");

            message.Encoding = Encoding.UTF8;

            sender.Send(message);
        }

        public GmailSender(string accountEmailAddress, string accountPassword) : base("smtp.gmail.com")
        {
            Port = 587;
            UserName = accountEmailAddress;
            Password = accountPassword;
            EnableSsl = true;
        }

        protected override void ConfigureSender(Message message) {
            if (!this.HasCredentials)
            {
                throw new Exception("Gmail Sender requires account email address and password for authentication");
            }

            base.ConfigureSender(message);

        }

    }
}
