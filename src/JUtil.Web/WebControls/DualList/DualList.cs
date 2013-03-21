using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Text;

[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.DualList.images.Clear.png", "image/png")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.DualList.images.MoveToR.png", "image/png")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.DualList.images.MoveToL.png", "image/png")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.DualList.images.AllToL.png", "image/png")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.DualList.images.AllToR.png", "image/png")]

[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.DualList.css.DualList.css", "text/css")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.DualList.scripts.jQuery.dualListBox-1.3.js", "text/javascript")]

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// DualList
    /// </summary>
    public class DualList : CompositeControl
    {
        /// <summary>
        /// TagKey
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        Panel box1 = new Panel();
        TextBox box1Filter = new TextBox();
        ImageButton box1Clear = new ImageButton();
        ListBox box1View = new ListBox();
        ListBox box1Storage = new ListBox();

        Panel box2 = new Panel();
        TextBox box2Filter = new TextBox();
        ImageButton box2Clear = new ImageButton();
        ListBox box2View = new ListBox();
        ListBox box2Storage = new ListBox();

        HtmlGenericControl ul = new HtmlGenericControl("ul");
        ImageButton to2 = new ImageButton();
        ImageButton allTo2 = new ImageButton();
        ImageButton allTo1 = new ImageButton();
        ImageButton to1 = new ImageButton();

        private void testdata(int start, ListBox lb)
        {
            lb.Items.Clear();

            for (int i = start; i <= start+10; i++)
            {
                lb.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        /// <summary>
        /// CreateChildControls
        /// </summary>
        protected override void CreateChildControls()
        {
            CssClass = "dual-listbox clearfix";

            testdata(1, box1View);
            testdata(11, box2View);

            box1.CssClass = "select-wrapper";
            box1.Controls.Add(box1Filter);
            box1Clear.CssClass = "clear-button";
            box1Clear.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "JUtil.Web.WebControls.DualList.images.Clear.png");
            box1.Controls.Add(box1Clear);
            box1.Controls.Add(box1View);
            box1Storage.Attributes.Add("style", "display:none;");
            box1.Controls.Add(box1Storage);

            HtmlGenericControl li = null;

            li = new HtmlGenericControl("li");
            to2.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "JUtil.Web.WebControls.DualList.images.MoveToR.png");
            li.Controls.Add(to2);
            ul.Controls.Add(li);

            li = new HtmlGenericControl("li");
            allTo2.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "JUtil.Web.WebControls.DualList.images.AllToR.png");
            li.Controls.Add(allTo2);
            ul.Controls.Add(li);

            li = new HtmlGenericControl("li");
            allTo1.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "JUtil.Web.WebControls.DualList.images.AllToL.png");
            li.Controls.Add(allTo1);
            ul.Controls.Add(li);

            li = new HtmlGenericControl("li");
            to1.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "JUtil.Web.WebControls.DualList.images.MoveToL.png");
            li.Controls.Add(to1);
            ul.Controls.Add(li);

            box2.CssClass = "select-wrapper";
            box2.Controls.Add(box2Filter);
            box2Clear.CssClass = "clear-button";
            box2Clear.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "JUtil.Web.WebControls.DualList.images.Clear.png");
            box2.Controls.Add(box2Clear);
            box2.Controls.Add(box2View);
            box2Storage.Attributes.Add("style", "display:none;");
            box2.Controls.Add(box2Storage);

            Controls.Add(box1);
            Controls.Add(ul);
            Controls.Add(box2);
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            Type type = this.GetType();
            ClientScriptManager.RegisterEmbeddedCSS(type, "JUtil.Web.WebControls.DualList.css.DualList.css");

            ClientScriptManager.RegisterEmbeddedJs(type, "JUtil.Web.WebControls.jQuery.jquery.1.4.4.js");

            List<string> lstWebResource = new List<string>();
            lstWebResource.Add("JUtil.Web.WebControls.DualList.scripts.jQuery.dualListBox-1.3.js");
            ClientScriptManager.RegisterCompositeScript(lstWebResource);

            string scripts = GenApplicationLoadScript();
            ClientScriptManager.RegisterClientApplicationLoadScript(this, scripts);
        }

        private string GenApplicationLoadScript()
        {
            // $.configureBoxes();

            StringBuilder scripts = new StringBuilder();

            scripts.Append("$.configureBoxes({");

            scripts.Append("box1View   : '" + box1View.ClientID + "',");
            scripts.Append("box1Storage   : '" + box1Storage.ClientID + "',");
            scripts.Append("box1Filter   : '" + box1Filter.ClientID + "',");
            scripts.Append("box1Clear   : '" + box1Clear.ClientID + "',");

            scripts.Append("box2View   : '" + box2View.ClientID + "',");
            scripts.Append("box2Storage   : '" + box2Storage.ClientID + "',");
            scripts.Append("box2Filter   : '" + box2Filter.ClientID + "',");
            scripts.Append("box2Clear   : '" + box2Clear.ClientID + "',");

            scripts.Append("to1   : '" + to1.ClientID + "',");
            scripts.Append("allTo1   : '" + allTo1.ClientID + "',");
            scripts.Append("to2   : '" + to2.ClientID + "',");
            scripts.Append("allTo2   : '" + allTo2.ClientID + "'");

            /*
            transferMode: 'move',
            sortBy: 'text',
            useFilters: true,
            useCounters: true,
            useSorting: true,
            selectOnSubmit: true
             */
            

            scripts.Append("});");

            
            return scripts.ToString();
        }

        /// <summary>
        /// Render
        /// </summary>
        protected override void Render(HtmlTextWriter writer)
        {
            ListBox[] lbs = { box1View, box1Storage, box2View, box2Storage };

            foreach (ListBox lb in lbs)
                Page.ClientScript.RegisterForEventValidation(lb.UniqueID);

            base.Render(writer);
        }

        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                string text = string.Empty;

                foreach(ListItem item in box2View.Items)
                {
                    text += item.Value;
                }

                return text;
            }
        }

    } // end of DualList
}
