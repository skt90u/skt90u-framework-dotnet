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

public partial class ServerCentric : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Departments_SelectedIndexChanged(object sender, EventArgs e)
    {
        EmployeeResults.Text = string.Format("Employee count: {0}",
            HumanResources.GetEmployeeCount(Departments.SelectedValue));

        System.Threading.Thread.Sleep(2000);
    }
}
