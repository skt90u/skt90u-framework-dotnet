using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using JUtil.Extensions;

namespace JUtil.Web.WebControls
{
#if false
    /// <summary>
    /// Extension of GridView
    /// </summary>
    public class (失敗作品)XGridView : GridView
    {
        /// <summary>
        /// ctor of XGridView
        /// </summary>
        public XGridView()
        {
        }

        /// <summary>
        /// DataObjectOperator Type
        /// </summary>
        public string DataObjectOperatorType { get; set; }

        /// <summary>
        /// DataObject Type
        /// </summary>
        public string DataObjectType { get; set; }

        /// <summary>
        /// SelectMethod Name
        /// </summary>
        public string SelectMethod { get; set; }

        /// <summary>
        /// InsertMethod Name
        /// </summary>
        public string InsertMethod { get; set; }

        /// <summary>
        /// UpdateMethod Name
        /// </summary>
        public string UpdateMethod { get; set; }

        /// <summary>
        /// DeleteMethod Name
        /// </summary>
        public string DeleteMethod { get; set; }

        /// <summary>
        /// SelectCountMethod Name
        /// </summary>
        public string SelectCountMethod { get; set; }

        /// <summary>
        /// just bind data
        /// </summary>
        public void DoDataBind()
        {
            DataBind();
        }

        /// <summary>
        /// reset SelectParameters, and bind data
        /// </summary>
        public void DoDataBind(OrderedDictionary SelectParameters)
        {
            // ((DatabaseDataObjectOperatorBase)dataObjectOperator).SelectParameters = SelectParameters;

            DoDataBind();
        }

        private object _dataObjectOperator;
        private object dataObjectOperator
        {
            get
            {
                if (_dataObjectOperator == null)
                {
                    Type typeBinder = ExtType.GetType(DataObjectOperatorType);
                    _dataObjectOperator = Activator.CreateInstance(typeBinder);
                }
                return _dataObjectOperator;
            }
        }

        private ObjectDataSource _ods;
        private ObjectDataSource ods
        {
            get
            {
                if (_ods == null)
                {
                    _ods = new ObjectDataSource();

                    initObjectDataSource(
                         _ods,
                         DataObjectOperatorType,
                         DataObjectType,
                         SelectMethod,
                         InsertMethod,
                         UpdateMethod,
                         DeleteMethod,
                         SelectCountMethod);
                }
                return _ods;
            }
        }

        private void initObjectDataSource(ObjectDataSource aODS,
                                          string DataObjectOperatorType,
                                          string DataObjectType,
                                          string SelectMethod,
                                          string InsertMethod,
                                          string UpdateMethod,
                                          string DeleteMethod,
                                          string SelectCountMethod)
        {
            aODS.ID = "ods";
            aODS.TypeName = DataObjectOperatorType;
            aODS.DataObjectTypeName = DataObjectType;

            aODS.SelectMethod = SelectMethod;
            aODS.InsertMethod = InsertMethod;
            aODS.UpdateMethod = UpdateMethod;
            aODS.DeleteMethod = DeleteMethod;
            aODS.SelectCountMethod = SelectCountMethod;

            aODS.OldValuesParameterFormatString = "original_{0}";
            aODS.EnablePaging = true;
            aODS.EnableViewState = false;

            aODS.SelectParameters.Add("startRowIndex", TypeCode.Int32, "0");
            aODS.SelectParameters.Add("maximumRows", TypeCode.Int32, "0");

            aODS.ObjectCreating += new ObjectDataSourceObjectEventHandler(DoObjectCreating);
        }

        private void DoObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = dataObjectOperator;
        }

        /// <summary>
        /// XGridView Initialization
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //
            // if we want to create an ObjectDataSource inside XGridView dynamically,
            // we can't set ObjectDataSource's ID as XGridView's DataSourceID
            //
            // because of any kind of DataSourceID (no matter container is GridView or not)
            // the ObjectDataSource which container's DataSourceID specified
            // MUST BE EXIST IN PAGE
            // which mean you need 
            //   - (1) declare the ObjectDataSource in aspx explicitly
            //   - (2) create ObjectDataSource in codebehide dynamically and use
            //         Page.Form.Controls.Add(TheObjectDataSource) implicitly
            //
            // DataSourceID = ods.ID; <--- THIS STATEMENT WILL NEVER WORK !!!
            DataSource = ods;

            addEventHandlers();
        }

        private void addEventHandlers()
        {
            if (AllowPaging)
            {
                //
                // in order to avoid following exception, we need to handle event 'PageIndexChanging'
                // exception message : 引發但尚未處理的事件 PageIndexChanging
                //
                this.PageIndexChanging += new GridViewPageEventHandler(DoPageIndexChanging);
            }

            //
            // http://social.msdn.microsoft.com/forums/zh-TW/233/thread/dd672b42-8f74-4137-9047-e7b4a9021333
            //
            this.RowEditing += new GridViewEditEventHandler(DoRowEditing);
            this.RowCancelingEdit += new GridViewCancelEditEventHandler(DoRowCancelingEdit);
            
            //this.RowUpdating += new GridViewUpdateEventHandler(DoRowUpdating);
        }

        private void DoRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //this.UpdateRow(e.RowIndex, false);
            
        }

        private void DoRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EditIndex = -1;
            DoDataBind();
        }

        private void DoRowEditing(object sender, GridViewEditEventArgs e)
        {
            EditIndex = e.NewEditIndex;
            DoDataBind();
        }

        private void DoPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            DoDataBind();
        }


    } // end of GridView
#endif
}
