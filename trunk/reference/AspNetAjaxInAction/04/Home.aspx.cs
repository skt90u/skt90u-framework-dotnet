using System;
using System.Web;
using System.Web.UI;


public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Find the ScriptManager on the page
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        if (scriptManager.IsInAsyncPostBack)
        { 
            // We are doing something cool!
        }


        // Register a control that performs a postback so that 
        // it be used as a trigger for some update panels.
        //scriptManager.RegisterAsyncPostBackControl(NewsTimer);
    }

    protected void Genres_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateGenre();
    }

    private void UpdateGenre()
    {
        GenreSource.DataFile = Genres.SelectedValue;
        GenreNews.DataBind();
        System.Threading.Thread.Sleep(2000);
    }
    protected void UpdateNews(object sender, EventArgs e)
    {
        UpdateGenre();
        //GenrePanel.Update();
    }
    protected void CommentDetails_ItemInserting(object sender, System.Web.UI.WebControls.DetailsViewInsertEventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
    }
}
