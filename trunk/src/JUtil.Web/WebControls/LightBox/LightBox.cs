using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

// --------------------------------------------------------------------------------
// LightBox
// --------------------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.LightBox.images.lightbox-blank.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.LightBox.images.lightbox-btn-close.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.LightBox.images.lightbox-btn-next.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.LightBox.images.lightbox-btn-prev.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.LightBox.images.lightbox-ico-loading.gif", "image/gif")]

[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.LightBox.css.jquery.lightbox-0.5.css", "text/css")]
[assembly: System.Web.UI.WebResource("JUtil.Web.WebControls.LightBox.js.jquery.lightbox-0.5.js", "text/javascript", PerformSubstitution = true)]

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// Image
    /// </summary>
    public class LightBox : CompositeControl
    {
        private Image image = new Image();

        #region OnPreRender
        /// <summary>
        /// OnPreRender
        /// </summary>
        protected override void OnPreRender(System.EventArgs e)
        {
            Type type = GetType();
            ClientScriptManager.RegisterEmbeddedCSS(type, "JUtil.Web.WebControls.LightBox.css.jquery.lightbox-0.5.css");
            ClientScriptManager.RegisterEmbeddedJs(type, "JUtil.Web.WebControls.jQuery.jquery.1.4.4.js");

            List<string> lstWebResource = new List<string>();
            lstWebResource.Add("JUtil.Web.WebControls.LightBox.js.jquery.lightbox-0.5.js");
            ClientScriptManager.RegisterCompositeScript(lstWebResource);

            string scripts = GenApplicationLoadScript();
            ClientScriptManager.RegisterClientApplicationLoadScript(this, scripts);
        }
        #endregion
        #region GenApplicationLoadScript
        private string GenApplicationLoadScript()
        {
            StringBuilder scripts = new StringBuilder();
            
            /*
            $(function() {
                $('a').lightBox();
            });
             */
            
            //string script = string.Format("$('a#{0}').lightBox();", ClientID);

            string script = string.Format("$('a#{0}').lightBox();", ClientID);

            scripts.Append(script);

            return scripts.ToString();
        }
        #endregion
        #region TagKey
        /// <summary>
        /// TagKey
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.A;
            }
        }
        #endregion
        #region CreateChildControls
        /// <summary>
        /// CreateChildControls
        /// </summary>
        protected override void CreateChildControls()
        {
            /*
             * <a href="photos/image1.jpg" title="Utilize a flexibilidade dos seletores da jQuery e crie um grupo de imagens como desejar. $('#gallery').lightBox();">
             *  <img src="photos/thumb_image1.jpg" width="72" height="72" alt="" />
             * </a>
             */

            if (!string.IsNullOrEmpty(ImageUrl))
                Attributes.Add("href", ImageUrl);

            if (!string.IsNullOrEmpty(Title))
                Attributes.Add("title", Title);
            
            Controls.Add(image);
        }
        #endregion

        #region ImageWidth
        /// <summary>
        /// ImageWidth
        /// </summary>
        public Unit ImageWidth
        {
            get
            {
                return image.Width;
            }
            set
            {
                image.Width = value;
            }
        }
        #endregion
        #region ImageHeight
        /// <summary>
        /// ImageHeight
        /// </summary>
        public Unit ImageHeight
        {
            get
            {
                return image.Height;
            }
            set
            {
                image.Height = value;
            }
        }
        #endregion
        #region ThumbUrl
        /// <summary>
        /// ThumbUrl
        /// </summary>
        public string ThumbUrl
        {
            get
            {
                return image.ImageUrl;
            }
            set
            {
                image.ImageUrl = value;
            }
        }
        #endregion
        #region ImageUrl
        /// <summary>
        /// ImageUrl
        /// </summary>
        public string ImageUrl
        {
            get
            {
                object o = ViewState["ImageUrl"];

                if (o == null)
                {
                    o = string.Empty;
                }

                return Convert.ToString(o);
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }
        #endregion
        #region Title
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get
            {
                object o = ViewState["Title"];

                if (o == null)
                {
                    o = string.Empty;
                }

                return Convert.ToString(o);
            }
            set
            {
                ViewState["Title"] = value;
            }
        }
        #endregion


    } // end of LightBox
}
