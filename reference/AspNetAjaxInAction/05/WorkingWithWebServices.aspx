<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkingWithWebServices.aspx.cs" Inherits="WorkingWithWebServices" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <title>Working With Web Services</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
   <Services>
     <asp:ServiceReference Path="StarbucksService.asmx" InlineScript="true" />
   </Services>
  </asp:ScriptManager>

  <div>
    <input id="Location" type="text" />
    <input id="GetNumLocations" type="button" value="Get Count" onclick="getLocations()" /> (examples: 92618, 90623, 90017)
    <div id="NumLocations"></div>    
  </div>   
   
   <hr />
  <div>
    <input id="GetDeals" type="button" value="Get Deals" onclick="getDeals()" /> Retrieve complex types
    <div id="Deals"></div>   
  </div>
  
    <hr />
    <div>
    <input id="CreateEmployee" type="button" value="Instantiate Employee" onclick="createEmployee()" /> Instantiate complex type
    </div>
   
     
</form>    
<script type="text/javascript" language="javascript">
<!--

    function createEmployee(){                   
        var emp1 = new AspNetAjaxInAction.Employee();
        emp1.First = "Frank";
        emp1.Last = "Rizzo";        
        emp1.Title = "Principal";
        
        PageMethods.HelloEmployee(emp1, onHelloEmployeeSuccess);        
    }

    function onHelloEmployeeSuccess(result, context, methodName){
        alert(result);
    }
        

  function getDeals(){
    AspNetAjaxInAction.StarbucksService.GetDeals(onGetDealsSuccess, onGetDealsFailure, "context", 1000);
  }
  
  function onGetDealsSuccess(result, context, methodName){
    
    Sys.Debug.traceDump(result);
    
    var sb = new Sys.StringBuilder();    
    for (var i = 0; i < result.length; i++){        
        var bev = result[i];
        sb.append(bev.Name + " - ");
        sb.append(bev.Description + " - ");
        sb.append(bev.Cost + "<br />");        
    }    
    
    $get("Deals").innerHTML = sb.toString();
  }
  
  function onGetDealsFailure(error, context, methodName){
    $get("Deals").innerHTML = error.get_message();
  }

  function getLocations(){    
    var zip = $get("Location").value;
    AspNetAjaxInAction.StarbucksService.GetLocationCount(zip, onGetLocationSuccess, onGetLocationFailure, "<%= DateTime.Now %>");
  }

  function onGetLocationSuccess(result, context, methodName){
    $get("NumLocations").innerHTML = result + " location(s) found.";
  }

  function onGetLocationFailure(error, context, methodName){
    var errorMessage = error.get_message();
    $get("NumLocations").innerHTML = errorMessage;
  }
    

//-->
</script>
        
</body>
</html>
