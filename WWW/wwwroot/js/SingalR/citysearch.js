"use strict";

 
var connection = new signalR.HubConnectionBuilder().withUrl("/CityHub").build();

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

var CitySearchInput = document.getElementById("CitySearchInput");
CitySearchInput.addEventListener("input", function (event) {
    var city = CitySearchInput.value;
    connection.invoke("GetCitys", city).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});



// recive message from hub and Add Content to page
connection.on("ReceiveCitys", function (citys) {
    document.getElementById("Citylist").innerHTML = null;
    for (var i = 0; i < citys.length; i++) {
        var div = document.createElement("div");
        div.className = "col";

        var a = document.createElement("a");
        a.className = "dropdown-item";
        a.textContent = citys[i];

        div.appendChild(a);
        document.getElementById("Citylist").appendChild(div);
    }
});

