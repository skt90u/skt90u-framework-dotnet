using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JUtil.EmailSender;
using Message = JUtil.EmailSender.Message;

namespace JUtil.WinForm
{
    public partial class LicenseSender : Form
    {
        private List<License> SelectedItemTags;

        public LicenseSender(List<License> SelectedItemTags)
        {
            InitializeComponent();

            this.SelectedItemTags = SelectedItemTags;
        }

        private void btnOK_Click(object o, EventArgs e)
        {
            string Email = tbEmail.Text;

            if (Email.Length == 0)
                throw new ArgumentNullException("Email");

            if(false == JUtil.Validation.IsValidEmail(Email))
                throw new Exception(String.Format("Email不符合正確格式({0})", Email));
            
            string account = "skt90u@gmail.com";
            string passwd = "490410056";

            string from = "skt90u@gmail.com";

            GmailSender sender = new GmailSender(
                account,
                passwd);

            sender.AsyncSend = true;

            string softwareName = string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach(License license in SelectedItemTags)
            {
                sb.AppendFormat("({0}) - {1}\r\n\r\n", license.comment, license.serialNumber);

                softwareName = license.softwareName;
            }

            string body = sb.ToString();
            string title = string.Format("{0} 產品金鑰", softwareName);

            Message message = new Message(from, Email, title, body);

            message.Encoding = Encoding.UTF8;

            sender.Send(message);
            
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
