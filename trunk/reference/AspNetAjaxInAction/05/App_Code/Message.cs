using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AspNetAjaxInAction
{
    /// <summary>
    /// Summary description for Message
    /// </summary>
    public class Message
    {

        #region Properties

        private string postedBy;
        public string PostedBy
        {
            get { return this.postedBy; }
            set { this.postedBy = value; }
        }

        private DateTime datePosted;
        public DateTime DatePosted
        {
            get { return this.datePosted; }
            set { this.datePosted = value; }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        private int messageID;
        public int MessageID
        {
            get { return this.messageID; }
            set { this.messageID = value; }
        }

        private string subject;
        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }

        #endregion

        #region Constructors

        public Message()
        {

        }

        #endregion

    }

}