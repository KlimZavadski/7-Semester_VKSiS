var addresses = ["unicast", "broadcast", "multicast", "anycast"];
var host = "host" + Math.floor(Math.random() * 99999);

$(document).ready(function () {
	Initialize();
	localStorage.setItem('IsUsed', false);
});

function Initialize() {
	window.addEventListener('storage', receiveData,	false);

	clearSendTextArea();
	$('#sendButton').click(sendData);

	$('#sendTextArea').keyup(function () {
		if ($(this).val().length == 0) {
			$('#sendButton').attr('disabled', true);
		}
		else {
			$('#sendButton').attr('disabled', false);
		}
	});

	$('#clearButton').click(function (){
		clearSendTextArea();
		$('#receiveTextArea').val("");
		localStorage.clear();
		localStorage.setItem('IsUsed', false);
	});
}

function clearSendTextArea()
{
	$('#sendButton').attr('disabled', true);
	$('#sendTextArea').val("");
}

function insertReceiveData(data)
{
	var text = "from " + data.Source + ": " + data.Data + "\n";
	var text = data.Data + "\n";
	var oldText = $('#receiveTextArea').val();
	$('#receiveTextArea').val(oldText + text);
}

function getAddress(mode)
{
	var type = addresses.indexOf($('#' + mode + 'AddressingList option:selected').val()) - 0;
	var number = $('#' + mode + 'ChannelNumber').val() - 0;
	return type * 64 + number;
}

function sleep(millis)
{
	var date = new Date();
	var curDate;
	do
	{
		curDate = new Date();
	} while(curDate - date < millis);
}

function sendData()
{
	var package = {
		Id: Math.floor(Math.random() * 99999),
		Destination: getAddress("send"),
		Source: host,
		Data: $('#sendTextArea').val()
	};
	localStorage.setItem(package.Id, JSON.stringify(package));
	clearSendTextArea();
	insertReceiveData(package);
}

function receiveData (event) {
	var package = JSON.parse(event.newValue);
	if (package.Destination < 64) {
		// Unicast.
		if (package.Destination == getAddress("receive")) {
			insertReceiveData(package);
		}
	}
	else if (package.Destination < 128) {
		// Broadcast.
		insertReceiveData(package);
	}
	else if (package.Destination < 192) {
		// Multicast.
		if (package.Destination == getAddress("receive")) {
			insertReceiveData(package);
		}
	}
}