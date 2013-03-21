using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientSideEventViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Update the current time if it's a normal postback (not an asychronous one) 
        // or just a typical page load.
        if (!Page.IsPostBack)
            LastUpdated1.Text = DateTime.Now.ToLongTimeString();

    }

    protected void SlowUpdate_Click(object sender, EventArgs e)
    {
        LastUpdated1.Text = DateTime.Now.ToLongTimeString();

        // Demo purposes only - do not call Sleep in production!!!
        // 
        // We call Sleep in here to slow down the processing and give us
        // a longer window of opportunity to test out the Abort and Cancel
        // postback logic (see client-script).        
        System.Threading.Thread.Sleep(5000);
    }

    protected void FastUpdate_Click(object sender, EventArgs e)
    {
        LastUpdated1.Text = DateTime.Now.ToLongTimeString();
    }

    protected void ThrowError_Click(object sender, EventArgs e)
    {
        throw new InvalidOperationException("Nice throw!");
    }
}
