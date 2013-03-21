<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PhotoGallery Example</title>
    <link type="text/css" href="Style/PhotoGallery.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="TheScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="ScriptLibrary/PhotoGallery.js" />
        </Scripts>
    </asp:ScriptManager>
       
    <div id="photoGallery">
        <div class="ctrl_panel">
            <input type="button" id="gal_prevButton" value="Prev" />
            <img id="gal_progress" src="Images/indicator.gif" alt="" style="visibility:hidden" />
            <input type="button" id="gal_nextButton" value="Next" />
        </div>
        <div>
            <img src="Images/placeholder.png" id="gal_image" alt="" />
        </div>
    </div>
    
    <script type="text/javascript">
    <!--
        Sys.Application.add_init(pageInit);

        function pageInit() {
            // Create a new instance of the PhotoGallery control.
            $create(Samples.PhotoGallery,
              {
               'imageElement': $get('gal_image'), 
               'prevElement': $get('gal_prevButton'), 
               'nextElement':$get('gal_nextButton'),
               'progressElement':$get('gal_progress'),
               'images': ['Images/photo1.jpg', 'Images/photo2.jpg', 'Images/photo3.jpg', 'Images/photo4.jpg']
              },
              null,
              null,
              $get('photoGallery'));
        }
    //-->
    </script>
    </form>
</body>
</html>
