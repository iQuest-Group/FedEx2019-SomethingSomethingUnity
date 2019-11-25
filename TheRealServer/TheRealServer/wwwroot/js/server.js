"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/server").build();

connection.on("ReceiveSpawnPoints", function (playerPositions) {
    for (var i = 0; i < playerPositions.length; i++) {
        LogMessage("Player " + playerPositions[i].name + " has spawned at: " + playerPositions[i].posX + ", " + playerPositions[i].posY);
    }
});

connection.on("ReceiveGameLeave", function (id) {
    LogMessage("Player with id: " + playerPosition.id + " has left the game ");
});

connection.on("ReceiveSingleSpawnPoint", function (playerPosition) {
    LogMessage("Player " + playerPosition.name + " has spawned at: " + playerPosition.posX + ", " + playerPosition.posY);
});

connection.on("ReceiveGameReset", function () {
    LogMessage("Game was reset");
});

connection.on("ReceiveMovement", function (playerPosition) {
    LogMessage("Player " + playerPosition.name + " has moved at: " +  playerPosition.posX + ", " + playerPosition.posY);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var playerPosition = { "id": 1, "name": "ALEX1", "posX": 5, "posY": 20 };
    console.log(playerPosition);
    connection.invoke("SendMovement", playerPosition).catch(function (err) {
        return console.error(err);
    });
    event.preventDefault();
});

function LogMessage(msg) {
    var messageList = document.getElementById("messagesList");
    messageList.value = msg + '\n' + messageList.value;
}