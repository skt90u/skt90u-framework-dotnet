<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bridges</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="flickr.asbx" />
            </Services>
        </asp:ScriptManager>
        
        <div>        
            <img src="http://l.yimg.com/www.flickr.com/images/flickr_logo_gamma.gif.v1.5.7" width="98" height="26" />        
            <input id="flickrSearch" type="text" />
            <input id="search" type="button" value="Search" onclick="doSearch();" />
            <span id="searching" style="display: none;">
                &nbsp;<img src="images/indicator.gif" />&nbsp;Searching...
            </span>

            <div id="summary"></div>
            <hr />
            <span id="photoList"></span>                                    
        </div>
    </form>

<script type="text/javascript" language="javascript">

    function pageLoad(){
        $get("flickrSearch").focus();
    }

    function doSearch(){
        var keywords = $get("flickrSearch").value;
        $get("searching").style.display = "inline";        
        AspNetAjaxInAction.FlickrSearch.Search( {tags:keywords}, onSearchComplete, onSearchFailed);
    }
    
    function onSearchComplete(results){        
        $get("searching").style.display = "none";                        
        $get("summary").innerHTML = formatSummary(results, $get("flickrSearch").value);
                                
        var photos = new Sys.StringBuilder();        
        photos.append("<table>");               
        for (var i = 0; i < results.length; i++){        
            var photo = results[i];
            photos.append("<tr>");
            photos.append(formatImage(photo));
            photos.append(formatDetails(photo));
            photos.append("<tr>");               
        }
        photos.append("</table>");                 
        $get("photoList").innerHTML = photos.toString();                
    }        
    
    function onSearchFailed(error){
        $get("searching").style.display = "none";
        alert(error.get_message());
    }
    
    function formatSummary(photos, tags){        
        var summary = new Sys.StringBuilder();
        summary.append(photos.length);
        summary.append(" results found for photos tagged with ");
        summary.append("<b>" + tags + "</b>" + ".");
        return summary.toString();
    }
    
    function formatDetails(photo){    
        var details = new Sys.StringBuilder();
        details.append("<td>");
        details.append("<div>");
        details.append(photo.Title);        
        details.append("</div>");
        details.append("<div>");
        details.append("Tags: " + photo.Tags);
        details.append("</div>");
        details.append("</td>");
        return details.toString();        
    }
    
    function formatImage(photo){        
        // http://farm{farm-id}.static.flickr.com/{server-id}/{id}_{secret}_m.jpg        
        var link = new Sys.StringBuilder();
        link.append("<td>");
        link.append("<img src='http://farm");
        link.append(photo.FarmID);
        link.append(".static.flickr.com/");
        link.append(photo.ServerID);
        link.append("/" + photo.ID + "_");
        link.append(photo.Secret);
        link.append("_s.jpg'");        
        link.append(" />");
        link.append("</td>");                        
        return link.toString();
    }
    

</script>        
</body>
</html>
