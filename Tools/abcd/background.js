chrome.extension.onMessage.addListener(function(request, sender, sendResponse) {
    switch(request.type) {
        case "color-divs":
            colorDivs();
        break;
    }
    return true;
});

var colorDivs = function() {
    chrome.tabs.getSelected(null, function(tab){
			var	tabId = tab.id;
			var min = prompt(" Name:", "30");
			var milliSeconds = parseInt(min) * 60 * 1000;
			if(milliSeconds > 300000){
				for(var myTime=1000; myTime< milliSeconds; myTime = myTime + 300000)
				{
					setTimeout(function(){goOk(tabId);}, myTime);
				}
			}
			setTimeout(function(){goOk(tabId);}, milliSeconds);
    });
	
}

function goOk(id){
	chrome.tabs.sendMessage(id, {type: "colors-div", color: "red"});
}