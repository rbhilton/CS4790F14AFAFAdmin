$(document).ready(function () {
$(".deleteLink").click(function (event) {
        var link = $(this).attr("href");

        $("#dialog-confirm").dialog({
            resizable: false,
            height: 200,
            width: 400,
            modal: true,
            buttons: {
                "Delete Entry": function () {
                    __RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
                    $.ajax({
                        type: "post",
                        dataType: "html",
                        url: link,
                        data: { '__RequestVerificationToken': __RequestVerificationToken },
                        success: function (response) {
                            location.reload();
                        }
                    });
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

        return false;
    });
$('.eventSubmitButton').click(function () {
    $('.createEvent').click();

});

$('.eventFillData').click(function () {
    fillEventTable();
});

if ($("#userTable").find("tr").size() > 1) {
    $("#userTable").tablesorter();
}
if ($("#eventTable").find("tr").size() > 1) {
    $("#eventTable").tablesorter();
}
if ($("#donorTable").find("tr").size() > 1) {
    $("#donorTable").tablesorter();
}
if ($("#recipientTable").find("tr").size() > 1) {
    $("#recipientTable").tablesorter();
}
      
});

/*-----------------------------------------------------------------------------------*/
/*	TABS
/*-----------------------------------------------------------------------------------*/
$(document).ready(function() {
	//Default Action
	$(".tab_content").hide(); //Hide all content
	$("ul.tabs li:first").addClass("active").show(); //Activate first tab
	$(".tab_content:first").show(); //Show first tab content
	
	//On Click Event
	$("ul.tabs li").click(function() {
		$("ul.tabs li").removeClass("active"); //Remove any "active" class
		$(this).addClass("active"); //Add "active" class to selected tab
		$(".tab_content").hide(); //Hide all tab content
		var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
		$(activeTab).fadeIn(); //Fade in the active content
		return false;
	});

});

/*-----------------------------------------------------------------------------------*/
/*	MENU
/*-----------------------------------------------------------------------------------*/
ddsmoothmenu.init({
	mainmenuid: "menu", 
	orientation: 'v', 
	classname: 'menu-v', 
	contentsource: "markup" 
})

/*-----------------------------------------------------------------------------------*/
/*	IMAGE HOVER
/*-----------------------------------------------------------------------------------*/
$(function() {
$('.post a img, ul.works li a img, ul.popular-posts a img').css("opacity","1.0");	
$('.post a img, ul.works li a img, ul.popular-posts a img').hover(function () {										  
$(this).stop().animate({ opacity: 0.85 }, "fast"); },	
function () {			
$(this).stop().animate({ opacity: 1.0 }, "fast");
});
});

/*-----------------------------------------------------------------------------------*/
/*	SIDEBAR HEIGHT
/*-----------------------------------------------------------------------------------*/
jQuery(document).ready(function($){
	var h = $(document).height();
	$('#sidebar').css({height: h+'px'});
});


function fillEventTable() {
    $('#eventType').val("Charity");
    $('#eventStatus').val("active");
    $('#eventTitle').val("Event for Charity");
    $('#eventDate').val("11/30/13");
    $('#eventTime').val("8:00 AM");
    $('#eventRegCloseDate').val("12/30/13");
    $('#eventLocationName').val("Union Pacific Center");
    $('#eventAddress').val("24th Center Street");
    $('#eventCity').val("Ogden");
    $('#eventState').val("Utah");
    $('#eventZip').val("83212");
    $('#userName').val("userName");
    $('#price1').val("50");
    $('#price2').val("30");
    $('#eventDirector').val("Event Director");
    $('#eventDirectorEmail').val("Director@gmail.com");
    $('#eventDirectorPhone').val("1234567890");
    $('#eventMaxCapacity').val("500");
    $('#eventDescription').val('This event is for charity, This event is for charity, This event is for charity, This event is for charity, This event is for charity.');
    $('#eventDetails').val('Event Details, Event Details, Event Details, Event Details, Event Details, Event Details, Event Details, Event Details, Event Details, Event Details.');
}