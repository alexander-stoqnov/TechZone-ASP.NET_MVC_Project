$(function() {
    var chat = $.connection.techChat;

    chat.client.receiveMessage = function(name, message) {
        if (name === $('#chat-username').val()) {
            $('#chat-window').append('<li id="mine"><span id="normal-message">' + name + ' : ' + message + '</span></li>');
        } else {
            $('#chat-window').append('<li id="other"><span id="normal-message">' + name + ' : ' + message + '</span></li>');
        }
        $('#message').val('');
    }
})