function castUpVote() {
    var currentUpVoteCount = $("#useful").text();
    $("#useful").text(Number(currentUpVoteCount) + 1);
}
function castDownVote() {
    var currentDownVoteCount = $("#useless").text();
    $("#useless").text(Number(currentDownVoteCount) + 1);
}

function userAlreadyVoted() {
    $('#error-message-here').toggle("slow");
    $('#error-message-here').text("You have already voted for this review!");
}