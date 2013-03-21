<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicalNavigation.aspx.cs" Inherits="LogicalNavigation" %>
<%@ Register Assembly="RssToolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=02e47a85b237026a"
    Namespace="RssToolkit" TagPrefix="rssToolkit" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>History</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <asp:History ID="History1" runat="server" OnNavigate="NavigateHistory" />
    
    <div>

        <rssToolkit:RssDataSource ID="RssDataSource1" runat="Server" MaxItems="7">
        </rssToolkit:RssDataSource>   

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>                     
                
                <asp:DropDownList ID="Blogs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Blogs_Changed" >
                    <asp:ListItem Text="ASP.NET Weblogs" Value="http://weblogs.asp.net/MainFeed.aspx" />
                    <asp:ListItem Text="MSDN Blogs" Value="http://blogs.msdn.com/MainFeed.aspx" />
                    <asp:ListItem Text="DotNetSlackers Community" Value="http://dotnetslackers.com/community/blogs/MainFeed.aspx" />           
                </asp:DropDownList>                  
                <hr />                
                            
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div>
                            <img src="images/indicator.gif" alt="" />
                            Loading...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>                            
                                
                <asp:DataList ID="Posts" runat="server" DataSourceID="RssDataSource1">
                    <ItemTemplate>
                        <asp:HyperLink ID="TitleLink" runat="server" Text='<%# Eval("title") %>'
                            NavigateUrl='<%# Eval("link") %>' Target="_blank" >
                        </asp:HyperLink>          
                    </ItemTemplate>
                </asp:DataList>                                        
                                                           
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
        
    </form>
</body>
</html>
