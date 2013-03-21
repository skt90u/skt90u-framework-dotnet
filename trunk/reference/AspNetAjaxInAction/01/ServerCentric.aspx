<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServerCentric.aspx.cs" Inherits="ServerCentric" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Employee Lookup</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <h2>Employee Lookup</h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>      
              <asp:ListBox AutoPostBack="true" runat="server" ID="Departments" 	
                OnSelectedIndexChanged="Departments_SelectedIndexChanged">   	                        
                 <asp:ListItem Text="Engineering" Value="Engineering" />
                 <asp:ListItem Text="Human Resources" Value="HR" />
                 <asp:ListItem Text="Sales" Value="Sales" />
                 <asp:ListItem Text="Marketing" Value="Marketing" />
              </asp:ListBox>                
            </div>
            <br />
            <div>
              <asp:Label ID="EmployeeResults" runat="server" />           		
            </div>      
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
     <ProgressTemplate>
         <img src="images/indicator.gif" alt="Loading" />&nbsp;&nbsp;Loading ...
     </ProgressTemplate>
    </asp:UpdateProgress>    
  
   
    </form>
</body>
</html>
