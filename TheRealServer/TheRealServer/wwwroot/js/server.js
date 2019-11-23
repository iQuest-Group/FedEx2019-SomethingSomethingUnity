"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/server").build();

connection.on("ReceiveSpawnPoints", function (playerPositions) {
    for (var i = 0; i < playerPositions.length; i++) {
        document.getElementById("messagesList").value += "Player " + playerPositions[i].name + " has spawned at: " + playerPositions[i].posX + ", " + playerPositions[i].posY + "\n";
    }
});

connection.on("ReceiveGameLeave", function (id) {
    document.getElementById("messagesList").value += "Player with id: " + playerPosition.id + " has left the game " + "\n";
});

connection.on("ReceiveSingleSpawnPoint", function (playerPosition) {
        document.getElementById("messagesList").value += "Player " + playerPosition.name + " has spawned at: " + playerPosition.posX + ", " + playerPosition.posY + "\n";
});

connection.on("ReceiveGameReset", function () {
    document.getElementById("messagesList").value += "Game was resetted" +  "\n";
});

connection.on("ReceiveMovement", function (playerPosition) {
    document.getElementById("messagesList").value += "Player " + playerPosition.name + " has moved at: " +  playerPosition.posX + ", " + playerPosition.posY + "\n";
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