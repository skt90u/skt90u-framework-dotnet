Sys.Application.add_load(function() {
	var AutoSavedMessage = new Sys.Services.ProfileGroup();
	AutoSavedMessage.Text = $get(__TextID).value;
	AutoSavedMessage.Subject = $get(__SubjectID).value;
	Sys.Services.ProfileService.properties["AutoSavedMessage"] = AutoSavedMessage;
	
	window.setInterval(tryAutoSave, 5000);
});
		
var _autoSaveCallInProgress = false;
		
function tryAutoSave() {
    if (_autoSaveCallInProgress)
        return;
    
     var AutoSavedMessage = Sys.Services.ProfileService.properties["AutoSavedMessage"];;
    
    var subject = $get(__SubjectID).value;
    var text = $get(__TextID).value;
    
    var propertyNames = [];
    
    if (AutoSavedMessage.Text != text) {
        propertyNames.push("AutoSavedMessage.Text");
        AutoSavedMessage.Text = text;
    }
    
    if (AutoSavedMessage.Subject != subject) {
        propertyNames.push("AutoSavedMessage.Subject");
        AutoSavedMessage.Subject = subject;
    }
    
    if (propertyNames.length == 0)
        return;
    
    Sys.Services.ProfileService.save(propertyNames, 
        function() {  
            _autoSaveCallInProgress = false;
        },
        
        function() {
            _autoSaveCallInProgress = false;
        }
    );			

    _autoSaveCallInProgress = true;
}

