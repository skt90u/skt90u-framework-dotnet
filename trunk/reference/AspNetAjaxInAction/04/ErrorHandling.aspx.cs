using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorHandling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(scriptManager_AsyncPostBackError);
        
        // Comment out this line to allow the redirect.
        scriptManager.AllowCustomErrorsRedirect = false;
    }

    void scriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).AsyncPostBackErrorMessage = "We're sorry, an unexpected error has occurred.";
    }

    protected void ThrowError_Click(object sender, EventArgs e)
    {
        throw new Exception("Lookout!");
    }
}
