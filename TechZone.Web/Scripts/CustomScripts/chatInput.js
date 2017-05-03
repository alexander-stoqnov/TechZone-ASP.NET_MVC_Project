$(function() {
    var chat = $.connection.techChat;

    chat.client.receiveMessage = function(name, message) {
        if (name === $('#username').val()) {
            $('#chat-window').append('<li id="mine">' + name + ' : ' + message + '</li>');
        } else {
            $('#chat-window').append('<li id="other">' + name + ' : ' + message + '</li>');
        }
        $('#message').val('');
    }
})