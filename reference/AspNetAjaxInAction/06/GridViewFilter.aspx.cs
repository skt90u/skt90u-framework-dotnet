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
using System.Text;

public partial class GridViewFilter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            GridView1.Sort("FirstName", SortDirection.Ascending);
    }

    protected void FilterText_TextChanged(object sender, EventArgs e)
    {
        UpdateFilter();
        System.Threading.Thread.Sleep(2000);
    }

    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        UpdateFilter();
    }

    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        UpdateFilter();
    }

    protected void Filter_Click(object sender, EventArgs e)
    {
        UpdateFilter();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;

        if (String.IsNullOrEmpty(SqlDataSource1.FilterExpression))
            return;

        // Lookup the column index
        int colIndex = GetColumnIndex(GridView1.SortExpression);
        TableCell cell = e.Row.Cells[colIndex];

        string cellText = cell.Text;
        int leftIndex = cellText.IndexOf(FilterText.Text, StringComparison.OrdinalIgnoreCase);
        int rightIndex = leftIndex + FilterText.Text.Length;

        StringBuilder builder = new StringBuilder();
        builder.Append(cellText, 0, leftIndex);
        builder.Append("<span class=\"highlight\">");
        builder.Append(cellText, leftIndex, rightIndex - leftIndex);
        builder.Append("</span>");
        builder.Append(cellText, rightIndex, cellText.Length - rightIndex);

        cell.Text = builder.ToString();
    }

    private void UpdateFilter()
    {
        string filterExpression = null;

        if (!String.IsNullOrEmpty(FilterText.Text))
            filterExpression = string.Format("[{0}] LIKE '%{1}%'", GridView1.SortExpression, FilterText.Text);

        SqlDataSource1.FilterExpression = filterExpression;

    }

    private int GetColumnIndex(string columnName)
    {
        for (int i = 0; i < GridView1.Columns.Count; i++)
        {
            BoundField field = GridView1.Columns[i] as BoundField;
            if (field != null && field.DataField == columnName)
                return i;
        }

        return -1;
    }

}
