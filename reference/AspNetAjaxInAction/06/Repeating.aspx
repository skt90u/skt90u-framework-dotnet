<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Repeating.aspx.cs" Inherits="Repeating" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Repeating UpdatePanels</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <h1>My Portfolio</h1>

    <asp:XmlDataSource ID="data" runat="server">			
    <Data>
    <stocks>
     <stock name="STOCK1" value="STOCK1" />
     <stock name="STOCK2" value="STOCK2" />
     <stock name="STOCK3" value="STOCK3" />
     <stock name="STOCK4" value="STOCK4" />
    </stocks>
    </Data>
    </asp:XmlDataSource>

    <div style="width: 200px; border: 1px solid gray; padding: 5px;">
    <asp:Repeater ID="Stocks" runat="server" DataSourceID="data" 
    DataMember="stock">
    <ItemTemplate>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">  
      <ContentTemplate>
        <asp:Label runat="server" ID="StockName" Text='<%# Eval("name") %>' 
          />
        <asp:Button runat="server" ID="UpdateStock" Text="Update" 
          OnClick="UpdateStock_Click" />      
        <asp:Label runat="server" ID="StockPrice" Text="" />  
      </ContentTemplate>
     </asp:UpdatePanel>
    </ItemTemplate>
    <SeparatorTemplate>
     <hr style="border: 1px dashed gray;" />
    </SeparatorTemplate>
    </asp:Repeater>        
    </div>

    </form>
</body>
</html>
