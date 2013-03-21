<%@ Page %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Declarative Databinding</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
        </Scripts>
        <Services>
            <asp:ServiceReference Path="ProductsService.asmx" />
        </Services>
    </asp:ScriptManager>
    
    <div id="myListView">
        <table id="listView_layoutTemplate" style="background-color:#efefef;border:solid 1px #808080;border-right:solid 2px #808080;border-bottom:solid 2px #808080">
            <thead>
                <tr style="background-color:#bcbcbc;border:solid 1px #808080">
                    <th colspan="2">Articles</th>
                </tr>
            </thead>
            <tbody id="listView_itemTemplateParent">
                <tr id="listView_itemTemplate">
                    <td><span id="nameLabel"></span></td>
                    <td><a id="detailsLink" href="#">View Details</a></td>
                </tr>
            </tbody>
        </table>
    </div>
    
    <script type="text/xml-script">
        <page xmlns="http://schemas.microsoft.com/xml-script/2005">
            <components>
                <dataSource id="theDataSource"
                            serviceURL="ProductsService.asmx"
                            loadMethod="select"
                            />

                <listView id="myListView"
                          itemTemplateParentElementId="listView_itemTemplateParent"
                          >
                    <bindings>
                        <binding dataContext="theDataSource"
                                 dataPath="data"
                                 property="data"
                                 />
                    </bindings>
                    <layoutTemplate>
                        <template layoutElement="listView_layoutTemplate" />
                    </layoutTemplate>
                    <itemTemplate>
                        <template layoutElement="listView_itemTemplate">
                            <label id="nameLabel">
                                <bindings>
                                    <binding dataPath="Name" property="text" />
                                </bindings>
                            </label>
                            <hyperLink id="detailsLink">
                                <bindings>
                                    <binding dataPath="ID" 
                                             property="navigateURL"
                                             transform="ToDetailsUrl"
                                             />
                                </bindings>
                            </hyperLink>
                        </template>
                    </itemTemplate>
                </listView>
                
                <!--
                <application>
                    <load>
                        <invokeMethodAction target="theDataSource" method="load" />
                    </load>
                </application>
                -->
            </components>
        </page>
    </script>
    
    <script type="text/javascript">
    <!--
        function pageLoad() {
            ProductsService.GetTopTenProducts(onGetComplete);
        }
        
        function onGetComplete(result) {
            $find('myListView').set_data(result);
        }
        
        function ToDetailsUrl(sender, e) {
            var productId = e.get_value();
            var formatUrl = "catalog.html?product_id={0}";
            
            e.set_value(String.format(formatUrl, productId));
        }
    //-->
    </script>

    </form>
</body>
</html>
