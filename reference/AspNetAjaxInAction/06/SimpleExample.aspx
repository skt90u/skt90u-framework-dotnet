<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SimpleExample.aspx.cs" Inherits="SimpleExample" %>
<%@ Register Src="~/UserPoll.ascx" TagPrefix="demo" TagName="UserPoll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Simple UpdatePanel Example</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div id="masthead">
            <a href="SimpleExample.aspx" ><img src="images/header.png" alt="Emily's Flowers" style="border: 0px;" /></a>            
        </div>
        
        <div id="menucontainer">            
            <asp:Menu ID="FlowerMenu" runat="server" Orientation="Horizontal" 
                CssClass="menubar" DisappearAfter="10"
                DynamicSelectedStyle-CssClass="menuItemSelected">                
                <LevelMenuItemStyles>
                    <asp:MenuItemStyle CssClass="menuheaders" />                     
                </LevelMenuItemStyles>                
                <Items>
                    <asp:MenuItem Text="Flowers" />
                    <asp:MenuItem Text="Plants" />                    
                    <asp:MenuItem Text="Valentine's Day" />
                    <asp:MenuItem Text="Specials" />
                    <asp:MenuItem Text="Occasions" />
                    <asp:MenuItem Text="Shopping Cart" />
                    <asp:MenuItem Text="Help" />
                </Items>
            </asp:Menu>
        </div>
        
        <div id="container">
            <div id="page_content">                
               <div class="banner">Save up to 50% on Delivery!</div>               
                <table>
                    <tr>
                        <td>                            
                            <img src="images/sunflower.jpg" alt="Flowers" />                            
                        </td>
                        <td align="left" valign="top">
                            <table cellspacing="8">
                                <tr>
                                    <td colspan="2">
                                        <span class="categoryheader">Flowers</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Daisies</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Orchids</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Tulips</a>
                                    </td>
                                    <td>
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Sunflowers</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Lilies</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Carnations</a>                        
                                    </td>
                                 
                                </tr>
                                
                                <tr>
                                    <td colspan="2">
                                        <span class="categoryheader">Occasions</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Valentine's Day</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Birthday</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Anniversary</a>
                                    </td>
                                    <td>
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Get Well</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Wedding</a><br />
                                        <img src="images/bullets.gif" />&nbsp;<a href="#">Thank You</a>                        
                                    </td>                             
                                </tr>                    
                            </table>                    
                        </td>
                    </tr>
               </table>                          
            </div>
            <div id="right_col">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <demo:UserPoll ID="FlowerPoll" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </div>
        </div>
        
        <div id="footer">
            
        </div>        



    </form>
</body>
</html>
