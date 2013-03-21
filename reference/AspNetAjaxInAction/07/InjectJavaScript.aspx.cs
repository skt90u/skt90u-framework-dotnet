using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InjectJavaScript : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void InjectScript_Click(object sender, EventArgs e)
    {
        string msg = string.Format("alert(\"{0}\");",
                                  "You've done this before, haven't you?");

        ScriptManager.RegisterStartupScript(Button1, typeof(Button),
                                           "clickTest",
                                            msg, true);

    }
}
