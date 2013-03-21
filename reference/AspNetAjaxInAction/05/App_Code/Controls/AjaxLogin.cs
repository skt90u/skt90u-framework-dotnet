using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:AjaxLogin runat=server></{0}:AjaxLogin>")]
    public class AjaxLogin : Login, IScriptControl
    {
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string OnLogonSuccessHandler
        {
            get { return ViewState["OnLogonSuccessHandler"] as string; }
            set { ViewState["OnLogonSuccessHandler"] = value; }
        }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string OnLogonFailedHandler
        {
            get { return ViewState["OnLogonFailedHandler"] as string; }
            set { ViewState["OnLogonFailedHandler"] = value; }
        }

        private void AddControlIDToScript(ScriptComponentDescriptor descriptor, string id)
        {
            Control control = this.FindControl(id);

            if (control != null)
            {
                descriptor.AddElementProperty(id, control.ClientID);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Page.Request.Browser.SupportsXmlHttp)
            {

                //Make sure that scriptmanager has the Authentication service
                ScriptManager manager = ScriptManager.GetCurrent(this.Page);

                if (manager == null)
                    throw new InvalidOperationException(Resources.SR.ScriptManagerRequired);

                AuthenticationServiceManager authenticationService = manager.AuthenticationService;

                manager.RegisterScriptControl(this);
                manager.RegisterScriptDescriptors(this);
            }

            //Otherwise use the default rendering
        }

        #region IScriptControl Members

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("Samples.AjaxLogin", this.ClientID);
            AddControlIDToScript(descriptor, "UserName");
            AddControlIDToScript(descriptor, "Password");
            AddControlIDToScript(descriptor, "RememberMe");
            AddControlIDToScript(descriptor, "LoginButton");

            descriptor.AddProperty("ValidationGroup", this.UniqueID);

            if (this.OnLogonFailedHandler != null)
                descriptor.AddEvent("logonFailed", this.OnLogonFailedHandler);

            if (this.OnLogonSuccessHandler != null)
                descriptor.AddEvent("logonSuccess", this.OnLogonSuccessHandler);

            yield return descriptor;
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference("~/Scripts/TypeHelpers.js");
            yield return new ScriptReference("~/Scripts/AjaxLogin.js");
        }

        #endregion
    }
}