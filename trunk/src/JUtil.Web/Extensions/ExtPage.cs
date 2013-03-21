using System.Web.UI;
using System.Web.UI.HtmlControls;
using JUtil.Web.WebControls;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance Page functionality</summary>
    public static class ExtPage
    {
        /// <summary>insert css-link in head tag</summary>
        public static void RegisterCSS(this Page aPage, string csslocation)
        {
            if (string.IsNullOrEmpty(csslocation))
                return;
            if (ContainsStyleLink(aPage, csslocation))
                return;
            Control cssLink = CreateStyleLink(csslocation);
            aPage.Header.Controls.Add(cssLink);
        }

        /// <summary>a helper function for create a css-link control</summary>
        private static Control CreateStyleLink(string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("rel", "Stylesheet");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("href", href);
            return link;
        }

        private static bool ContainsStyleLink(Page aPage, string href)
        {
            foreach (Control control in aPage.Header.Controls)
            {
                if (control is HtmlLink && (control as HtmlLink).Href == href)
                    return true;
            }
            return false;
        }

        public static void LoadRequires(this Page page)
        {
            ScriptManager sm = ScriptManager.GetCurrent(page);

            if (sm == null)
            {
                sm = new ScriptManager();
                page.Form.Controls.Add(sm);
            }

            const string name = "JUtil.Web.JavaScript.jutil-all.js";
            const string assembly = "JUtil.Web";
            bool found = false;

            ScriptReferenceCollection scripts = sm.Scripts;
            for (int i = 0; i < scripts.Count; i++)
            {
                ScriptReference sr = scripts[i];

                if (sr.Name == name && sr.Assembly == assembly)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                scripts.Add(new ScriptReference(name, assembly));

            found = false;
            foreach (Control ctl in page.Form.Controls)
            {
                if (ctl is WebStyle)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                page.Form.Controls.Add(new WebStyle());

            Environment.HasPrepared = true;
        }

    } // end of ExtPage
}
