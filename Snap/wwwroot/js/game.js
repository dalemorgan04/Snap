$(function () {
    game.init();
});
var game = {
    init: function () {
        var $game = $("#game");
        if ($game.length) {

            var $computerCallSnap = $("#ComputerCallSnap");
            var $isPlayersTurn = $("#IsPlayersTurn");

            if ($computerCallSnap.length && $computerCallSnap.val() === "True") {
                game.computerWait(game.computerCallSnap);

            } else if($isPlayersTurn.length && $isPlayersTurn.val() === "False") {
                game.computerWait(game.computerPlayCard);
            }
        }
    },
    computerWait: function (func) {
        //Wait a random amount of time
        var min = 0.8,
            max = 2;
        var rand = Math.floor(Math.random() * (max - min + 1) + min);
        setTimeout(func, rand * 1000);
    },
    computerPlayCard: function () {
        var url = window.location.protocol + '//' + window.location.host + '/Game/PlayCard?PlayerType=Computer'
        window.location = url;
    },
    computerCallSnap: function () {
        var url = window.location.protocol + '//' + window.location.host + '/Game/CallSnap?PlayerType=Computer'
        window.location = url;
    }
}