<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" Title="Song Unsung Records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">        
        <Scripts>
            <asp:ScriptReference Path="~/scripts/DummyScript.js" />
        </Scripts>          
    </asp:ScriptManagerProxy>

    <div id="maincontent">
    
        <div id="leftcol">
	        <div class="columnheader">Recent Feedback</div>
        	        	        
	        <asp:UpdatePanel ID="CommentsPanel" runat="server" UpdateMode="Conditional">
	            <ContentTemplate>
                     <asp:GridView id="CommentsView" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="5" DataKeyNames="CommentID" Width="100%">
                        <PagerSettings PageButtonCount="5"></PagerSettings>
                        <Columns>
                            <asp:BoundField DataField="Comment" SortExpression="Comment" HeaderText="Comments">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="CommentID" InsertVisible="False" Visible="False" SortExpression="CommentID" HeaderText="CommentID"></asp:BoundField>
                        </Columns>
                    </asp:GridView> <%--                </ContentTemplate>
		        <Triggers>
		            <asp:AsyncPostBackTrigger ControlID="CommentDetails" EventName="ItemInserting" />
		        </Triggers>
	        </asp:UpdatePanel> --%><BR /><BR /><asp:DetailsView id="CommentDetails" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="CommentID" Width="320px" HeaderText="Post your comments and tell us what's on your mind." BorderWidth="1px" BorderStyle="Solid" BorderColor="Silver" CellPadding="4" GridLines="None" DefaultMode="Insert" AutoGenerateRows="False" OnItemInserting="CommentDetails_ItemInserting">
                <Fields>
                    <asp:BoundField DataField="CommentID" HeaderText="CommentID" InsertVisible="False"
                        ReadOnly="True" SortExpression="CommentID" />
                    <asp:BoundField DataField="Comment" HeaderText="Comment:" SortExpression="Comment" />
                    <asp:BoundField DataField="Name" HeaderText="Name:" SortExpression="Name" />
                    <asp:CommandField ShowInsertButton="True" CancelText="" ShowCancelButton="False" />
                </Fields>                    
	        </asp:DetailsView> 
                </ContentTemplate>
	       </asp:UpdatePanel>	
	       
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetComments"
                TypeName="CommentsTableAdapters.CommentTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_CommentID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Comment" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Original_CommentID" Type="Int32" />
                    <asp:Parameter Name="CommentID" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Comment" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>  
            	       	    			    			    
        </div>         
        
      
           
        <div id="rightcol">            
                                 
        <asp:UpdatePanel ID="GenrePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>                        
                <div class="columnheader">Music News:
                    <asp:DropDownList ID="Genres" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Genres_SelectedIndexChanged" >
                        <asp:ListItem Text="Rock" Value="~/App_Data/RockFeed.xml" Selected="true" />
                        <asp:ListItem Text="Jazz" Value="~/App_Data/JazzFeed.xml" />
                        <asp:ListItem Text="Blues" Value="~/App_Data/BluesFeed.xml" />
                    </asp:DropDownList>
                </div>                                                  

                <asp:UpdateProgress ID="UpdatingNews" runat="server" AssociatedUpdatePanelID="GenrePanel" >
                    <ProgressTemplate>
                        <img src="images/indicator.gif" alt="" />&nbsp;Loading ...
                    </ProgressTemplate>
                </asp:UpdateProgress> 
                            
                <asp:Repeater ID="GenreNews" runat="server" DataSourceID="GenreSource" >
                    <ItemTemplate>
                        <div class="newshead">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#XPath("link") %>' Text='<%#XPath("title") %>' />
                            &nbsp;
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#XPath("link") %>' Text="[read more]" />

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <hr />
                Last Updated: <%= DateTime.Now.ToLongTimeString() %>   
                                               
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="NewsTimer" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>                  
                          
        <asp:Timer ID="NewsTimer" runat="server" Interval="10000" OnTick="UpdateNews" /> 
 
        <asp:XmlDataSource runat="server" DataFile="~/App_Data/RockFeed.xml" ID="GenreSource" XPath="/rss/channel/item">
        </asp:XmlDataSource>   
                          
        </div>
        <div style="clear:both;"></div>
    </div>        

</asp:Content>

