$(function() {
    var chat = $.connection.techChat;

    chat.client.receiveMessage = function(name, message) {
        if (name === $('#username').html()) {
            $('#chat-window').append('<li id="mine">' + message + '</li>');
        } else {
            $('#chat-window').append('<li id="other">' + message + '</li>');
        }
    }
})