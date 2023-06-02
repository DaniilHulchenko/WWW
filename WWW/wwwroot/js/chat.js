"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/СhatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
// Send Button disabled before hub connected
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// get content from user and sent to hub 
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// recive message from hub and Add Content to page
connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    li.textContent = `${user} : ${message}`;
    document.getElementById("messagesList").appendChild(li);
});