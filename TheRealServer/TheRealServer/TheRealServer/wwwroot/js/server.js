"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/server").build();

connection.on("ReceiveMovement", function (playerPosition) {
    console.log(playerPosition);
    document.getElementById("messagesList").value = playerPosition.posX + " " + playerPosition.posY;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var playerPosition = { "id": 1, "posX": 5, "posY": 20 };
    console.log(playerPosition);
    connection.invoke("SendMovement", playerPosition).catch(function (err) {
        return console.error(err);
    });
    event.preventDefault();
});