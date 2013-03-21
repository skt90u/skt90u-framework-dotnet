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

public partial class ProgrammaticallyAddUpdatePanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UpdatePanel updatePanel = new UpdatePanel();
        updatePanel.ID = "up1";

        Label currentTime = new Label();
        currentTime.ID = "CurrentTime";


        if (ScriptManager1.IsInAsyncPostBack)
            currentTime.Text = string.Format("Dynamically updated at: {0} ", DateTime.Now.ToLongTimeString());
        else
            currentTime.Text = string.Format("Updated at: {0} ", DateTime.Now.ToLongTimeString());

        Button button1 = new Button();
        button1.ID = "button1";
        button1.Text = "Update";

        updatePanel.ContentTemplateContainer.Controls.Add(currentTime);
        updatePanel.ContentTemplateContainer.Controls.Add(button1);

        Page.Form.Controls.Add(updatePanel);
        
    }
}
