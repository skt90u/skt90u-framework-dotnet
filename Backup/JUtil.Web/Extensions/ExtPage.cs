using System.Web.UI;
using System.Web.UI.HtmlControls;

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
                {
                    return true;
                }
            }
            return false;
        }


    } // end of ExtPage
}
