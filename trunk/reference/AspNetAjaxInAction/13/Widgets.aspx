<%@ Page %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Widgets UI Example</title>
    <link rel="stylesheet" type="text/css" href="~/Style/Widgets.css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="scriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewScript.js" />
            <asp:ScriptReference Assembly="Microsoft.Web.Preview" Name="PreviewDragDrop.js" />
        </Scripts>
    </asp:ScriptManager>

    <div class="widgets">
        
        <%-- Left List --%>
        <div id="leftArea" class="left_col">
        
            <%--  Widget 1 --%>
            <div id="widget1" class="widget">
                <div id="widget1_Handle" class="widget_handle">Widget 1</div>
                <div class="widget_content">
                    <asp:Login ID="myLogin" runat="server" 
                               CssClass="centered"></asp:Login>
                </div>
            </div>
            
            <%-- Widget 2 --%>
            <div id="widget2" class="widget">
                <div id="widget2_Handle" class="widget_handle">Widget 2</div>
                <div class="widget_content">
                    <span>Enter some text:</span>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </div>
            </div>
            
        </div>
        
        <%-- Right List --%>
        <div id="rightArea" class="right_col">
        
            <%-- Widget 3 --%>
            <div id="widget3" class="widget">
                <div id="widget3_Handle" class="widget_handle">Widget 3</div>
                <div class="widget_content">
                    <asp:Calendar ID="Calendar1" runat="server"
                                  CssClass="centered"></asp:Calendar>
                </div>
            </div>
            
        </div>
        
        <%-- Templates --%>
        <div class="templates">
            <%-- Drop Cue template --%>
            <div id="dropCueTemplate" class="drop_cue"></div>
            <%-- Empty template --%>
            <div id="emptyTemplate" class="emptyList">Drop widgets here.</div>
        </div>

    </div>
       
    </form>

    <script type="text/xml-script">
        <page>
            <components>
                
                <!-- Left Area -->
                <control id="leftArea">
                    <behaviors>
                        <dragDropList dragDataType="HTML"
                                      acceptedDataTypes="'HTML'"
                                      dragMode="Move"
                                      direction="Vertical">
                            <dropCueTemplate>
                                <template layoutElement="dropCueTemplate" />
                            </dropCueTemplate>
                            <emptyTemplate>
                                <template layoutElement="emptyTemplate" />
                            </emptyTemplate>
                        </dragDropList>
                    </behaviors>
                </control>
                
                <!-- Right Area -->
                <control id="rightArea">
                    <behaviors>
                        <dragDropList dragDataType="HTML"
                                      acceptedDataTypes="'HTML'"
                                      dragMode="Move"
                                      direction="Vertical">
                            <dropCueTemplate>
                                <template layoutElement="dropCueTemplate" />
                            </dropCueTemplate>
                            <emptyTemplate>
                                <template layoutElement="emptyTemplate" />
                            </emptyTemplate>
                        </dragDropList>
                    </behaviors>
                </control>
                
                <!-- Draggable items -->
                <control id="widget1">
                    <behaviors>
                        <draggableListItem handle="widget1_Handle" />
                    </behaviors>
                </control>
                <control id="widget2">
                    <behaviors>
                        <draggableListItem handle="widget2_Handle" />
                    </behaviors>
                </control>
                <control id="widget3">
                    <behaviors>
                        <draggableListItem handle="widget3_Handle" />
                    </behaviors>
                </control>
                
            </components>
        </page>
    </script>
</body>
</html>

