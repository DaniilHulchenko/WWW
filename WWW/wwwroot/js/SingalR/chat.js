"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/СhatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
// Send Button disabled before hub connected
connection.start().then(function () {
    // join to event group 
    connection.invoke("JoinGroup", document.getElementById("EventIdInput").value);
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// get content from user and sent to hub 
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var useravatar = document.getElementById("userAvatarInput").value;
    var eventId = document.getElementById("EventIdInput").value;
    


    connection.invoke("SendMessage", user, message, useravatar, eventId).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messageInput").value = "";
    event.preventDefault();
});

// recive message from hub and Add Content to page
connection.on("ReceiveMessage", function (user, message, useravatar, datetime) {
    var container = document.createElement("div");
    container.className = "container";

    var avatarname = document.createElement("div");
    avatarname.className = "position-relative";

    var img = document.createElement("img");
    img.src = useravatar;
    img.className = "ChatAvatar";
    avatarname.appendChild(img);

    var name = document.createElement("span");
    name.className = "text-color-black";
    name.textContent = `${user}:`;
    avatarname.appendChild(name);

    var date = document.createElement("span");
    date.className = "text-color-black";
    date.textContent = ` ${datetime}`;
    avatarname.appendChild(date);

    var usersmessage = document.createElement("p");
    usersmessage.textContent = `${message}`;

    container.appendChild(avatarname);
    container.appendChild(usersmessage);




    var messagesList = document.getElementById("messagesList");
    var firstChild = messagesList.firstChild; 
    messagesList.insertBefore(container, firstChild); 


    //document.getElementById("messagesList").appendChild(container);

});
