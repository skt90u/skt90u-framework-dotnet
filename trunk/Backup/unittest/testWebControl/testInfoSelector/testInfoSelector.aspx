<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testInfoSelector.aspx.vb" Inherits="testInfoSelector_testInfoSelector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register
  Assembly="JFramework.WebControl"
  Namespace="JFramework.WebControl"
  TagPrefix="jw" %>

<%@ Register
  Assembly="AjaxControlToolkit"
  Namespace="AjaxControlToolkit"
  TagPrefix="cc1" %>
  
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
  <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      <CompositeScript>
        <Scripts>
          <asp:ScriptReference Path="~/testInfoSelector/jquery-1.4.4.js" />
          <asp:ScriptReference Path="~/testInfoSelector/JScript.js" />
        </Scripts>
      </CompositeScript>
    </cc1:ToolkitScriptManager>
    
      <div>
        <asp:Button ID="btn01" Text="InfoSelector.DataBind" runat="server" />
        <br />
        <asp:Label ID="title01" Text="銷售通路:" runat="server"></asp:Label>
        <asp:Label ID="lb01" runat="server" />
                
        <jw:InfoSelector
          ID="InfoSelector01"
          FiltingField="F1"
          FiltingTitle="銷售通路"
          DataKeyNames="F1"
          OptionButton="CheckBox"
          PageSize="5"
          Width="800px"
          runat="server">
          <Fields>
            <jw:InfoGridViewField colName="F1" Type="Label" Title="銷售通路" />
            <jw:InfoGridViewField colName="F2" Type="NumEdit" Title="銷售限量" />
            <jw:InfoGridViewField colName="F3" Type="DateEdit" Title="銷售限期" />
          </Fields>
        </jw:InfoSelector>
      </div>
      
      <%--<div>
        <asp:Button ID="btn02" Text="InfoSelector.DataBind" runat="server" />
        <br />
        <asp:Label ID="title02" Text="銷售通路:" runat="server"></asp:Label>
        <asp:Label ID="lb02" runat="server" />
                
        <jw:InfoSelector
          ID="InfoSelector02"
          FiltingField="F1"
          FiltingTitle="銷售通路"
          DataKeyNames="F1"
          OptionButton="CheckBox"
          PageSize="5"
          runat="server">
          <Fields>
            <jw:InfoGridViewField colName="F1" Type="Label" Title="銷售通路" />
            <jw:InfoGridViewField colName="F2" Type="NumEdit" Title="銷售限量" />
            <jw:InfoGridViewField colName="F3" Type="DateEdit" Title="銷售限期" />
          </Fields>
        </jw:InfoSelector>
      </div>--%>
            
    </form>
</body>
</html>
