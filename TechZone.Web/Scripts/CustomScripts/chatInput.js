$(function() {
    var chat = $.connection.techChat;

    chat.client.receiveMessage = function (name, message) {
        if (message == '') {
            return;
        }
        if (name === $('#chat-username').val()) {
            var newMsgMine = $('<li id="mine" style="display: none;"><span id="normal-message">' + name + ' : ' + message + '</span></li>');
            $('#chat-window').append(newMsgMine);
            newMsgMine.toggle('fast');
        } else {
            var newMsgOther = $('<li id="other" style="display: none;"><span id="normal-message">' + name + ' : ' + message + '</span></li>');
            $('#chat-window').append(newMsgOther);
            newMsgOther.toggle('fast');
        }
        $('#message').val('');
    }

    //$('#message').keydown(function (e) {
    //    if (e.keyCode == 13) {
    //        $('#send-message').click();
    //    }
    //});
})