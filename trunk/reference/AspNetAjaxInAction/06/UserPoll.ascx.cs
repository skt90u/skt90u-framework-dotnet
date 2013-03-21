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

public partial class UserPoll : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        // To keep things simple for the demo, we are just going 
        // to hide the panel that submits the poll and show the 
        // panel that displays the feedback.
        pnlSubmitPoll.Visible = false;
        pnlPollReceived.Visible = true;
    }
}
