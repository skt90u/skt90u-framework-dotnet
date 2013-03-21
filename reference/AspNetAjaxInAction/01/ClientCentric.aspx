<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientCentric.aspx.cs" Inherits="ClientCentric" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Client-Centric Example</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="HRService.asmx" />
        </Services>
    </asp:ScriptManager>
    
    <h2>Employee Lookup</h2>
    
    <div>              
        <select id="Departments" size="5">
            <option value="Engineering">Engineering</option>
            <option value="HR">Human Resources</option>
            <option value="Sales">Sales</option>
            <option value="Marketing">Marketing</option>
        </select>            
    </div>
    <br />
    <div>
        <span id="employeeResults"></span>
        <span id="loading" style="display:none;">
            <img src="images/indicator.gif" alt="" />
            &nbsp;&nbsp;Loading ...
        </span>            
    </div>
            
    <script type="text/javascript">
    <!--
        
        var departments = null;
    
        Sys.Application.add_load(page_load);
        Sys.Application.add_unload(page_unload);
        
        function page_load(sender, e){            
            departments = $get("Departments");
            $addHandler(departments, "change", departments_onchange);
        }
        
        function page_unload(sender, e){            
            $removeHandler(departments, "change", departments_onchange);
        }
    
        function departments_onchange(sender, e){
            $get("employeeResults").innerHTML = "";
            $get("loading").style.display = "block";
            
            var selectedValue = departments.value;
            HRService.GetEmployeeCount(selectedValue, onSuccess);                     
        }
        
        function onSuccess(result){
            $get("loading").style.display = "none";
            $get("employeeResults").innerHTML = "Employee count: " + result;
        }
        

    //-->
    </script>
            
    </form>
</body>
</html>
