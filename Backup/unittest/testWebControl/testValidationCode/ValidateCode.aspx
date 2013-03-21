<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ValidateCode.aspx.vb" Inherits="testValidationCode_ValidateCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register 
  Assembly="AjaxControlToolkit" 
  Namespace="AjaxControlToolkit" 
  TagPrefix="cc1" %>
  
<%@ Register 
  Assembly="JFramework.WebControl" 
  Namespace="JFramework.WebControl" 
  TagPrefix="jw" %>
  
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <jw:ValidateCodeImage 
        ID="ValidateCodeImage1" 
        runat="server" 
        ValiateCodeWidthModulus="2"
        ValidateCodeBackColor="#fffff0" 
        ValidateCodeBorderColor="#000000" 
        ValidateCodeBorderColorMode="static"
        ValidateCodeBorderWidth="1" 
        ValidateCodeDisturbLevel="custom" 
        ValidateCodeDisturbNum="200"
        ValidateCodeDistrubLength="1" 
        ValidateCodeFontColor="#000000" 
        ValidateCodeFontColorMode="randomall"
        ValidateCodeFontSize="20" 
        ValidateCodeMaxLength="6" 
        ValidateCodeMinLength="4"
        ValidateCodeLengthMode="static" />
    </form>
</body>
</html>
