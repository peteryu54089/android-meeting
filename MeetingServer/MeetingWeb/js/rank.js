var error = 0, sum = 0;
var returnResult=new Array();
var  titleRow = 2;
$(document).ready(function()
{
  if (typeof (voteObj) != "undefined") {
		$("#vote").load("./web/" + voteObj.getRelativePath(), function () {
  //  $("#vote").load("./web/6352367059221727152.html", function () {
           
	        submitInit();
			tableInit();
			evnetInit(); 
            dialogInit();
		});
	}
});
function submitInit()
{
    $('#sendButton').click(function () {
        var str = "";
        error = 0;
        $('#dialog').dialog('open');
      
        var length = $("tr:gt("+titleRow+")").filter(filterSpace).length ;
        
        var resultItem;
        var i = 0;
        $('tr:gt('+titleRow+'):lt(' + length + ')').each(function () {
            if ($(this).find("td:eq(1)").html() != "&nbsp;") {
                resultItem = new Object();
                resultItem.name = $(this).find("td:eq(0)").text()+":"+$(this).find("td:eq(1)").text();
                resultItem.rank = ($(this).find("td:eq(2)").text() == "") ? $(this).find("td:eq(2)").find('input[type="number"]').val() : error++;
                str += resultItem.name + " ";
                str += ($(this).find("td:eq(3)").text() == "") ? "序位為 " + resultItem.rank + "<br />" : '<font color="red">' + $(this).find("td:eq(3)").text() + "</font><br/>";
                returnResult[i++] = resultItem;
            }
        });
        str += (error == 0) ? '<br /> <font color="blue">您確認是否要送出</font>' : '<br /> <font color="red">您的輸入中尚有錯誤，請更正後再送出</font>';
        $("#voteResult").html(str);
        dialogInit();
    });
}
function filterSpace() {
    return $(this).find("td:eq(0)").html() != "&nbsp;";
}
function tableInit()
{
    //var offset = 3 ;
    var length = $("tr:gt("+titleRow+")").filter(filterSpace).length;
    console.log("row " + length);
	$("tr:gt("+titleRow+"):lt(" + length  + "):even td").css("background-color","#ccc");
	var counter=0;
	var counterString="";
	$('tr:gt('+titleRow+'):lt('+length+')').each(function(){
		 $(this).find("td:eq(2)").html('<input type="number" step="1" required style="font-size:24px;width: 120px;"/>');
		 $(this).find("td:eq(3)").html('<font color="red">未輸入<br/></font>');
		 counter++;
		 counterString+="「"+counter+"」、";
	  
	});
	$('input[type="number"]').attr("max",counter);
	$('input[type="number"]').attr("min",1);
	$('tr:eq(1) td').html('<font color="blue">(請以序位'+counterString.substr(0,counterString.length-1)+'進行投票）</font>'); 

}
function evnetInit()
{
 
    var length = $("tr:gt("+titleRow+")").filter(filterSpace).length;
	$('input[type="number"]').keyup(function(){
		$(this).attr("value",$(this).val());
		
		$('input[type="number"]').each(function()
		{
			if($(this).val()=="")
			{
				$(this).parents('tr').find("td:eq(3)").html('<font color="red">未輸入<br/></font>');
				return;
			}
			var isValid = $(this)[0].validity.valid;
			
			
			var errorString=""; 
			if(!isValid)
				errorString+='<font color="red">您所輸入的序位不符合範圍<br/></font>';
			
			var another=$(this).parents('tr').siblings().find(':input[value="'+$(this).val()+'"]');
			var isDuplicated = another.length > 0;
			if(isDuplicated)
				errorString+='<font color="red">您所輸入的序位與其它序位重覆<br/></font>'; 
			$(this).parents('tr').find("td:eq(3)").html(errorString);	 
		});	
	
	});
}

function dialogInit() {

	$('#dialog').dialog({
		autoOpen: false,
		modal: true,
		resizable: false,
		width: 'auto',
        height: 'auto',
		position: 'center',
		open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
		buttons: {
			"確認": function () {
				if (typeof (voteObj) != "undefined" && error==0) {
				    voteObj.sendJsonVote(JSON.stringify(returnResult));
				}else
				{
					console.log("close");
					$(this).dialog("close");
				}
			},
			"取消": function () {
				$(this).dialog("close");
			}
		}
	}).css('font-size', '32px');
}
