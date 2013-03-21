using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.Preview.UI.Controls;
using HistoryEventArgs=Microsoft.Web.Preview.UI.Controls.HistoryEventArgs;

public partial class LogicalNavigation : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Blogs.SelectedValue))
            RssDataSource1.Url = Blogs.SelectedValue;
    }

    protected void Blogs_Changed(object sender, EventArgs e)
    {
        History1.AddHistoryPoint("blogState", Blogs.SelectedIndex);
        RssDataSource1.Url = Blogs.SelectedValue;
        Posts.DataBind();
    }

    protected void NavigateHistory(object sender, HistoryEventArgs e)
    {
        int index = 0;
        if (e.State.ContainsKey("blogState"))        
            index = int.Parse(e.State["blogState"].ToString());        

        Blogs.SelectedIndex = index;
        RssDataSource1.Url = Blogs.Items[index].Value;
        Posts.DataBind();
    }

}