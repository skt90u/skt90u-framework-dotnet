using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class WorkingWithWebServices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string HelloEmployee(AspNetAjaxInAction.Employee emp)
    {
        return string.Format("Hello {0} {1}.", emp.First, emp.Last);
    }
}
