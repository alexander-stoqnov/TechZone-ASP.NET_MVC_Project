$(function() {
    var chat = $.connection.techChat;

    $.connection.hub.start()
        .done(function() {
            $('#send-message')
                .click(function() {
                    chat.server.receiveMessage($('#username').val(), $('#message').val());
                });
        });
})