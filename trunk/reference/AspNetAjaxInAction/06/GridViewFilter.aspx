<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridViewFilter.aspx.cs" Inherits="GridViewFilter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Live GridView Filter</title>
	<style type="text/css">
		.highlight
		{
			background-color: yellow;
		}    
		
		.updateProgressPanel
        {
	        position: absolute;
	        width: 200px;
	        left: 30%;
	        top: 200px;
	        background-color: #fff;
	        border: solid 1px #00008B;
	        text-align: left;
	        vertical-align: middle;		
	        padding-top: 16px;
	        padding: 5px;
        }
          
	</style>      
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
       <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
        <div>
            Filter selected column: 
            <asp:TextBox ID="FilterText" runat="server" OnTextChanged="FilterText_TextChanged" />                    
        </div>
    
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div class="updateProgressPanel">
                    <img src="images/gears_an.gif" alt="Loading"  />&nbsp;<b>Loading ..</b>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    
        <p>
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                        AllowPaging="True" AllowSorting="True"
                        EmptyDataText="There are no data records to display." 
                        CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" 
                        OnPageIndexChanged="GridView1_PageIndexChanged" OnSorted="GridView1_Sorted">
                        <Columns>
                            <asp:BoundField DataField="ContactID" HeaderText="ContactID" SortExpression="ContactID" />                    
                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                            <asp:BoundField DataField="MiddleName" HeaderText="MiddleName" SortExpression="MiddleName" />
                            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />                    
                            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />                    
                            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#E3EAEB" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>                

                
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:AdventureWorks_DataConnectionString1 %>"                
                        SelectCommand="SELECT [ContactID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [EmailAddress], [Phone] FROM [Person].[Contact]">
                    </asp:SqlDataSource>     
                    
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="FilterText" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>

 
        </p>
    
    </div>
    </form>
    
    <script type="text/javascript">
        
        Sys.Application.add_load(page_load);
        Sys.Application.add_unload(page_unload);
        
        function page_load(){        
            $addHandler($get('FilterText'), 'keydown', onFilterTextChanged);                    
        }
        
        function page_unload(){         
            $removeHandler($get('FilterText'), 'keydown', onFilterTextChanged);   
        }
        
        var timeoutID = 0;
        
        function onFilterTextChanged(e){        
            
            // Clear any delays
            if (timeoutID){
                window.clearTimeout(timeoutID);
            }
            
            // Executes a code snippet or a function after specified delay
            timeoutID = window.setTimeout(updateFilterText, 1000);            
        }
        
        function updateFilterText(){                    
            __doPostBack('FilterText', '');
        }
        
        
    </script>
        
</body>
</html>