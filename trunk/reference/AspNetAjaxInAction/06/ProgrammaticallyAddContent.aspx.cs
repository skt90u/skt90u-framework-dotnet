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

public partial class ProgrammaticallyAddContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Button button1 = new Button();
        button1.ID = "Button1";
        button1.Text = "Update";

        Label label1 = new Label();
        label1.ID = "Label1";
        label1.Text = string.Format("Updated at: {0} ", DateTime.Now.ToLongTimeString());

        UpdatePanel1.ContentTemplateContainer.Controls.Add(label1);
        UpdatePanel1.ContentTemplateContainer.Controls.Add(button1);
    }
}
