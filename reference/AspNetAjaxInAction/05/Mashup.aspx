<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mashup.aspx.cs" Inherits="Mashup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Simple Mashup</title>
</head>
<body>
    <form id="form1" runat="server">    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx" />  
            </Scripts>
            <Services>
                <asp:ServiceReference Path="~/GeocodeService.asmx" />
            </Services>
        </asp:ScriptManager>    
    
    <div>    
        <div id="theMap" style="position:relative; width:400px; height:400px;"></div>        
    </div>        
    </form>
    
<script type="text/javascript" language="javascript">
<!--

    function pageLoad() {
        var theMap = new VEMap('theMap');            
        theMap.LoadMap();    
        
        var city = 'Paris';
        var country = 'France';
        AspNetAjaxInAction.GeocodeService.GetLocationData('', '', city, country, '', onLocationReceived);     
        
        function onLocationReceived(result) {
            var latLong = new VELatLong(result.Latitude, result.Longitude);
            var pinText = String.format('{0} ({1}, {2})', city, result.Latitude, result.Longitude);
            var pin = new VEPushpin(1, latLong, null, pinText);           
            
            theMap.AddPushpin(pin);    
            theMap.SetCenter(latLong);       
        }
    }  

//-->
</script>    
    
</body>
</html>
