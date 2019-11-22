"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/server").build();

connection.on("ReceiveMovement", function (posX, posY) {
    document.getElementById("messagesList").value = posX + " "  + posY;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendMovement", 10, 1).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});