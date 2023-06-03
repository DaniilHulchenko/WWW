"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/DataHub").build();

//Disable the send button until connection is established.
document.getElementById("getButton").disabled = true;
// Send Button disabled before hub connected
connection.start().then(function () {
    document.getElementById("getButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("getButton").addEventListener("click", function (event) {
    connection.invoke("GetTitle").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});



// recive message from hub and Add Content to page
connection.on("ReceiveTitle", function (title) {
    var li = document.createElement("li");
    li.textContent = title;
    document.getElementById("DataList").appendChild(li);
});

