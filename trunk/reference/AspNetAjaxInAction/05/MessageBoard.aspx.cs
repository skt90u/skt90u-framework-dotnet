using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MessageBoardPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsAuthenticated)
        {
            LoginLink.InnerText = Resources.SR.Logout;
        }
        else
        {
            LoginLink.InnerText = Resources.SR.Login;
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClientIDs", String.Format("var __LoginLinkID = '{0}'; var __LoginID = '{1}';", LoginLink.ClientID, Login1.ClientID), true);
    }
    protected void Subject_Init(object sender, EventArgs e)
    {
        TextBox control = (TextBox)sender;
        ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "__SubjectID", String.Format("var __SubjectID = '{0}';", control.ClientID), true);
        control.Text = Profile.AutoSavedMessage.Subject;

    }
    protected void MessageText_Init(object sender, EventArgs e)
    {
        TextBox control = (TextBox)sender;
        ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "__TextID", String.Format("var __TextID = '{0}';", control.ClientID), true);
        control.Text = Profile.AutoSavedMessage.Text;
    }
}
