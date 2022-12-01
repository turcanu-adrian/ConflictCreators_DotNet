"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

//Disable the send button until connection is established.
document.getElementById("createGame").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${message}`;
});

document.getElementById("stopConnection").addEventListener("click", function (event) {
    connection.stop();
    event.PreventDefault();
});

document.getElementById("startConnection").addEventListener("click", function (event) {
    connection.start().then(function () {
        document.getElementById("createGame").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("createGame").addEventListener("click", function (event) {
    connection.invoke("CreateNewGame", "gusky").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("getGames").addEventListener("click", function (event) {
    var gameID = document.getElementById("userInput").value;
    connection.invoke("GetGame", gameID).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});