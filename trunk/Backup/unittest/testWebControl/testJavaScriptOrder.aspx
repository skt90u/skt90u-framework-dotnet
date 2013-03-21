<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testJavaScriptOrder.aspx.vb" Inherits="testJavaScriptOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="BigJs.js"></script>
    <script type="text/javascript">
      alert('this is in head');
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Scripts>
          <%--<asp:ScriptReference Path="~/BigJs.js" />--%>
          <asp:ScriptReference Path="~/MiddleJs.js" />
        </Scripts>
      </asp:ScriptManager>
      
      <code>
      [測試JavaScript載入次序]
      <p>
      (1) 在head]/head]中設定外部連結的JavaScript(script type="text/javascript" src="..."]/script])
          彼此是沒有順序的, 這表示誰先載入是無法確定的
      </p>
      <p>
      (2) 在asp:ScriptManager]Scripts]asp:ScriptReference Path="..." /]/Scripts]/asp:ScriptManager]      
          定義的JavaScript是有順序性的
      </p>
      <p>
      (3) JavaScript執行的順序
          (a) [HEAD]中定義
          (b) [ScriptManager.Scripts]中定義
          (c) 在ScriptManager之後定義的script]/script]區塊
          (d) Sys.Application.add_init中註冊的function
          (e) Sys.Application.add_load中註冊的function
      </p>
      </code>
      
      <script type="text/javascript">
      <!--
          
          Sys.Application.add_load(page_load);
          Sys.Application.add_unload(page_unload);
          Sys.Application.add_init(page_init);

          function page_init(sender, e) {
            alert('init');
          }
          
          function page_load(sender, e) {
            funcBMiddleJs();
          }

          function page_unload(sender, e) {
          }

          funcAMiddleJs();
          alert('ccc');
      //-->
      </script>
    </div>
    </form>
</body>
</html>
