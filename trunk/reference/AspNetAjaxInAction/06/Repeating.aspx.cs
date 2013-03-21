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

public partial class Repeating : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void UpdateStock_Click(object sender, EventArgs e)
    {
        Button button = (Button) sender;							 
        Label price = (Label) button.NamingContainer.FindControl("StockPrice");	   
        Label stock = (Label) button.NamingContainer.FindControl("StockName"); 

        price.Text = LookupStockPrice(stock.Text);				        
    }

    private string LookupStockPrice(string name)
    {
      string price = "$0.00";
      switch (name)
      {
          case "STOCK1":
              price = "$10.45";
              break;

          case "STOCK2":
              price = "$4.00";
              break;

          case "STOCK3":
              price = "$5.58";
              break;

          default: 
              break;                
      }
      return price;
    }

}
