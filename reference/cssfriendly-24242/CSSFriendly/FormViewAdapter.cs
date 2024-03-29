using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CSSFriendly
{
	public class FormViewAdapter : CompositeDataBoundControlAdapter
	{
		protected override string HeaderText { get { return ControlAsFormView.HeaderText; } }
		protected override string FooterText { get { return ControlAsFormView.FooterText; } }
		protected override ITemplate HeaderTemplate { get { return ControlAsFormView.HeaderTemplate; } }
		protected override ITemplate FooterTemplate { get { return ControlAsFormView.FooterTemplate; } }
		protected override ITemplate PagerTemplate { get { return ControlAsFormView.PagerTemplate; } }
		protected override TableRow HeaderRow { get { return ControlAsFormView.HeaderRow; } }
		protected override TableRow FooterRow { get { return ControlAsFormView.FooterRow; } }
		protected override TableRow TopPagerRow { get { return ControlAsFormView.TopPagerRow; } }
		protected override TableRow BottomPagerRow { get { return ControlAsFormView.BottomPagerRow; } }
		protected override bool AllowPaging { get { return ControlAsFormView.AllowPaging; } }
		protected override int DataItemCount { get { return ControlAsFormView.DataItemCount; } }
		protected override int DataItemIndex { get { return ControlAsFormView.DataItemIndex; } }
		protected override int PageIndex { get { return ControlAsFormView.PageIndex; } }
		protected override PagerSettings PagerSettings { get { return ControlAsFormView.PagerSettings; } }

		protected string _classInsertData;

		public FormViewAdapter()
		{
			_classMain = "AspNet-FormView";
			_classHeader = "AspNet-FormView-Header";
			_classData = "AspNet-FormView-Data";
			_classFooter = "AspNet-FormView-Footer";
			_classPagination = "AspNet-FormView-Pagination";
			_classOtherPage = "AspNet-FormView-OtherPage";
			_classActivePage = "AspNet-FormView-ActivePage";
			_classEmptyData = "AspNet-FormView-EmptyData";

			_classFirstPrevPagination = "AspNet-FormView-FirstPrevPagination";
			_classNextLastPagination = "AspNet-FormView-NextLastPagination";
			_classFirstPage = "AspNet-FormView-FirstPage";
			_classNextPage = "AspNet-FormView-NextPage";
			_classPreviousPage = "AspNet-FormView-PreviousPage";
			_classLastPage = "AspNet-FormView-LastPage";
			_classPagingLinkDisabled = "AspNet-FormView-PagingDisabled";
			_classInsertData = "AspNet-FormView-InsertData";
		}

		protected override void BuildItem(HtmlTextWriter writer)
		{
			if ((ControlAsFormView.Row != null) &&
				(ControlAsFormView.Row.Cells.Count > 0))
			{
				writer.WriteLine();
				writer.WriteBeginTag("div");
				writer.WriteAttribute("class", _classData);
				writer.Write(HtmlTextWriter.TagRightChar);
				writer.Indent++;
				writer.WriteLine();

				switch (ControlAsFormView.CurrentMode)
				{
					case FormViewMode.Insert:
						writer.WriteBeginTag("div");
						writer.WriteAttribute("class", _classInsertData);
						writer.Write(HtmlTextWriter.TagRightChar);
						writer.Indent++;

						foreach (Control itemCtrl in ControlAsFormView.Row.Cells[0].Controls)
						{
							itemCtrl.RenderControl(writer);
						}

						writer.Indent--;
						writer.WriteLine();
						writer.WriteEndTag("div");
						break;

					default:
						if (DataItemCount == 0)
						{
							writer.WriteBeginTag("div");
							writer.WriteAttribute("class", _classEmptyData);
							writer.Write(HtmlTextWriter.TagRightChar);
							writer.Indent++;

							if (ControlAsFormView.EmptyDataTemplate != null)
							{
								PlaceHolder placeholder = new PlaceHolder();
								ControlAsFormView.Controls.Add(placeholder);
								ControlAsFormView.EmptyDataTemplate.InstantiateIn(placeholder);
								placeholder.RenderControl(writer);
                            }
							else
							{
								writer.Write(ControlAsFormView.EmptyDataText);
							}

							writer.Indent--;
							writer.WriteLine();
							writer.WriteEndTag("div");
						}
						else
						{
							foreach (Control itemCtrl in ControlAsFormView.Row.Cells[0].Controls)
							{
								itemCtrl.RenderControl(writer);
							}
						}

						break;
				}
				writer.Indent--;
				writer.WriteLine();
				writer.WriteEndTag("div");
			}
		}
	}
}
