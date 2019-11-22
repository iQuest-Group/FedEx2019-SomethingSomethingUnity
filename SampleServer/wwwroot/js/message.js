"use strict";

var connection = new signalR.HubConnectionBuilder()
  .withUrl("/messages")
  .build();

connection.on("ReceiveMessage", function(message) {
  var msg = message
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;");
  var div = document.createElement("div");
  div.innerHTML = msg + "<hr/>";
  document.getElementById("messages").appendChild(div);
});

connection.on("Move", function(dx, dy) {
  var div = document.createElement("div");
  div.innerHTML = "x: " + dx + ", y: " + dy + "<hr/>";
  document.getElementById("messages").appendChild(div);
});

connection.start().catch(function(err) {
  return console.error(err.toString());
});

document
  .getElementById("sendButton")
  .addEventListener("click", function(event) {
    var message = document.getElementById("message").value;
    connection.invoke("SendMessageToAll", message).catch(function(err) {
      return console.error(err.toString());
    });
    event.preventDefault();
  });

document
  .getElementById("clearButton")
  .addEventListener("click", function(event) {
    document.getElementById("messages").innerHTML = "";
  });

document.getElementById("btnLeft").addEventListener("click", function(event) {
  connection.invoke("MoveObject", -0.2, 0).catch(function(err) {
    return console.error(err.toString());
  });
  event.preventDefault();
});

document.getElementById("btnRight").addEventListener("click", function(event) {
  connection.invoke("MoveObject", 0.2, 0).catch(function(err) {
    return console.error(err.toString());
  });
  event.preventDefault();
});
