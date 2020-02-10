chrome.extension.onMessage.addListener(function(message, sender, sendResponse) {
    switch(message.type) {
        case "colors-div":
            var divs = document.querySelectorAll("a");
            if(divs.length === 0) {
                alert("There are no any anchors in the page.");
            } else {
                for(var i=0; i<divs.length; i++) {
					var element = divs[i];
					var id = element.id;
					if( id == null || id == "")
					{
					}else if(id== "HeaderCtl1_btnPunchInOut"){
						var value = element.innerText;
						if(value == "Punch Out"){
							element.click();
							return;
						}
					}
                }
            }
        break;
    }
});