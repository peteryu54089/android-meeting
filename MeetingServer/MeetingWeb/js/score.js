var scores = [];
var e = 0, sum = 0;
var defaultValue;

$(function () {
    dialogInit();
	if (typeof (scoreObj) != "undefined") {
		$("#vote").load("./web/" + scoreObj.getRelativePath(), function () {
	//		$("#vote").load("./web/63512862081272679380.html", function () {
			inputInit();
			checkboxInit();
			appendReason();
            eventInit();
			refresh();
			defaultValue = $('.cleardefault').val();
			$('.cleardefault').focus(function () {
				if (defaultValue == $(this).val())
					$(this).val("");
			});
		});
	}
});
function eventInit() {
	$("#sendButton").click(function () {
		$('#dialog').dialog('open');
		(e != 0) ? $('#error').show() : $('#error').hide();
		return false;
	});
}
function dialogInit() {
    $("#comformDialog").dialog({ autoOpen: false, modal: true,
        buttons: {
            "確認": function () {
                $(this).dialog("close");
            }
        }

    });
    $("#reason_ck").dialog({ autoOpen: false, modal: true, width: '300px', position: 'center',
	buttons: {
            "確認": function () {
		$(this).dialog("close");
	    }
        }
    });
    $("#reason_dialog").dialog({ autoOpen: false, modal: true, width: '1200px', position: 'center',
        buttons: {
            "確認": function () {
		if($("#check9").prop("checked"))
		{
			var value = $('.cleardefault').val();
		 	if(value == '___________________' || value == '')
			{
			    $("#reason_ck").dialog('open');
			    return;
			}
		}
                var reasons = [];
                $('input[type=checkbox]:checked').each(function () {
                    var other = $(this).parent().find('input[type=text]');
                    var msg = $(this).parent().text().replace(/\s/g, '');
                    if (other.length != 0)
                        msg += $(other).val();
                    reasons.push(msg);
                    console.log(reasons);
                })
                if (typeof (scoreObj) != "undefined" && reasons.length!=0) {
                    scoreObj.sendJsonScore(JSON.stringify(scores), JSON.stringify(reasons));
                }else
                    $("#comformDialog").dialog('open');
            },
			"取消": function () {
				$(this).dialog("close");
			}
        }
    });
	$('#dialog').dialog({
		autoOpen: false,
		modal: true,
		resizable: false,
		width: '650px',
		position: 'center',
		open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
		buttons: {
			"確認": function () {
				if (sum < 75 || e != 0)
					$("#reason_dialog").dialog('open');
				else if (typeof (scoreObj) != "undefined") {
					scoreObj.sendJsonScore(JSON.stringify(scores), JSON.stringify([]));
				}
			},
			"取消": function () {
				$(this).dialog("close");
			}
		}
	}).css('font-size', '32px');
}
function inputInit() {
	index = 0;
	$("#vote").find("table td").each(function () {
		$(this).css("border-width", "1px").css("padding", "7px").css("font-weight", "bold");
		if ($(this).css('border-top-color') == "rgb(255, 0, 0)") {
			$(this).css("border-width", "2px").css("font-weight", "normal");
			input_id = 'arg' + (index++);
			range = $(this).html();
			min = range.split('~')[0];
			max = range.split('~')[1];
			input = '<input autocomplete="off" type="number" name="' + input_id + '" id="' + input_id + '" title="' + range + '" min="' + min + '" max="' + max + '" step="0.01" value="" required />';
			$(this).css("border-color", "red").html(input);
		}
	});
	$('input[type="number"]').keyup(valueChanged);
	$('input[type="number"]').change(valueChanged);
}
function checkboxInit() {
	index = 0;
	$('td:contains("□")').each(function () {
		var text = $(this).html().replace('□', '');
		$(this).html('<input type="checkbox" id="check' + index + '"><label for="check' + (index++) + '"></label>' + text);
	});
	$('td:contains("___________________")').each(function () {
		var html = $(this).html().replace('___________________', '<input type="text" id="arg' + (index++) + '" tabindex="-1" value="___________________" class="cleardefault">');
		$(this).html(html);
	});
}

function appendReason() {
	var start = $("tr:contains('教師升等未過理')").index();
	var end = $("tr:contains('備註')").index();
	var size = end - start;
	for (i = 0; i < size; i++) {
		$($('tr')[start]).find('td').removeClass();
		$($('tr')[start]).appendTo('#reason_table');
	}
}
function refresh() {
	e = 0, sum = 0;
	scores = [];
	sum = parseFloat($(".X33").html());
	$('input[type="number"]').each(function () {
	    var id = $(this).attr("id");
	    var val = parseFloat($(this).val());
	    if (!isNaN(val)) {
	        sum += val;
	    }
	    scores.push(isNaN(val) ? -1 : val);
	    if (false == $(this)[0].validity.valid) {
	        e++;
	    }
	    $("#score").text(Math.round(sum * 100) / 100);
        var score=Math.round(sum * 100) / 100;
        var color =((score < 75)||(score >100)) ? "red" : "blue";
	    $(".X24").html('<b><font color="' + color + '">' + Math.round(sum * 100) / 100 + '</font></b>').css('font-size', '32px');
	});
}
function valueChanged() {
	refresh();
	if (false == $(this)[0].validity.valid) {
		msg = $('<div class="messagebox" id="' + $(this).attr("id").replace("arg", "msg") + '">' + "<b> 評分範圍為: " + $(this).attr("title") + ' 分 </b></div>').hide();
		$(this).bubbletip(msg, { deltaDirection: "left", bindShow: "blur", bindHide: "focus" });
	} else {
		$(this).removeBubbletip();
	}
}