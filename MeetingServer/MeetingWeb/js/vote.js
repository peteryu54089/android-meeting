var e = 0, sum = 0;
var returnResult=new Array();
var titleRow=2;
$(document).ready(function()
{
    //dialogInit();
	if (typeof (voteObj) != "undefined") {
		$("#vote").load("./web/" + voteObj.getRelativePath(), function () {
        // $("#vote").load("./web/63524077377991924932.html", function () { //
            evnetInit();
   	        dialogInit();
			tableInit();
		});
	}
});
function filterSpace() {
    return ($(this).find("td:eq(0)").html() != "&nbsp;")  
}
function tableInit()
{ 
        var length = $("tr:gt("+titleRow+")").filter(filterSpace).length ;
		$("table").css("margin","auto");
		$("tr:gt("+titleRow+"):lt(" + length  + "):even td").css("background-color","#ccc");
		var index=0
		
		$("tr:gt("+titleRow+"):lt(" + length  + ")").find("td:gt(1):lt("+titleRow+")").each(function()
		{
			var row_index = $(this).parent().index('tr');
			var col_index = $(this).index('tr:eq('+row_index+') td');
			$(this).html('<input type="checkbox" id="check'+index+'" /><label for="check'+(index) +'"></label>' +$('tr:eq('+titleRow+')').find('td:eq('+col_index+')').html());			 
			index++;
		}); 
		
		$("tr:gt("+titleRow+"):lt(" + length  + ")").each(function()
		{
		   if($(this).find("input[type=checkbox]:checked").length==0)
		   {
			 $(this).find('td:eq(4)').html('<font color="red">未投票</font>');
		   }
		   
		});
		$('input[type=checkbox]').click(function(){
		  var row=$(this).parent().parent();
		  var check = $(this).prop("checked");
		  //row.find('td:eq(2)').find('input[type=checkbox]').prop("checked",false);
		  row.find('input[type=checkbox]').prop("checked",false);
		  $(this).prop("checked",check);
		  if(row.find('input[type=checkbox]:checked').length>1)
			  row.find('td:eq(4)').html('<font color="red">此為廢票</font>');
		  else if(row.find('input[type=checkbox]:checked').length==0)
			 row.find('td:eq(4)').html('<font color="red">未投票</font>');
		  else
			 row.find('td:eq(4)').html('');
		});

}
function evnetInit()
{
	$('#sendButton').click(function()
	{
		$('#dialog').dialog('open');
 
		var length = $("tr:gt("+titleRow+")").filter(filterSpace).length;
		var isError=0;
		var str="";
		var resultItem;
		var i=0
		$("tr:gt("+titleRow+"):lt(" + length  + ")").each(function()
		{
		   resultItem=new Object();
		   resultItem.name=$(this).find("td:eq(0)").text()+":"+$(this).find("td:eq(1)").text();
		   resultItem.agreeChoose=false;
		   resultItem.rejectChoose=false;
		   resultItem.invalidateChoose=false;
		   str +=resultItem.name;
		   if($(this).find("input[type=checkbox]:checked").length!=1)
		    { 
				str+=" <font color=\"red\">不同意</font><br/>";
				resultItem.rejectChoose=true;
				//resultItem.invalidateChoose=true;
			}else
			{
				if($(this).find("input[type=checkbox]:checked").parent().text()=="同意") {
					str +=" <font color=\"blue\">"+$(this).find("input[type=checkbox]:checked").parent().text()+"</font><br />";
					resultItem.agreeChoose=true;
				}
				if($(this).find("input[type=checkbox]:checked").parent().text()=="不同意"){
					str +=" <font color=\"red\">"+$(this).find("input[type=checkbox]:checked").parent().text()+"</font><br />";
					resultItem.rejectChoose=true;
				}
			} 
			returnResult[i++]=resultItem;
		});
        $("#voteResult").html(str);
        dialogInit();
	});
}

function dialogInit() {

	$('#dialog').dialog({
		autoOpen: false,
		modal: true,
		resizable: false,
		width: '900px',
		position: 'center',
		open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
		buttons: {
			"確認": function () {
				if (typeof (voteObj) != "undefined") {
				    voteObj.sendJsonVote(JSON.stringify(returnResult));
				}
			},
			"取消": function () {
				$(this).dialog("close");
			}
		}
	}).css('font-size', '32px');
}
