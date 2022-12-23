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

document.getElementById("createGame").addEventListener("click", function (event) {
    var name = document.getElementById("nameInput").value;
    connection.invoke("CreateNewGame", name).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("getGame").addEventListener("click", function (event) {
    var gameID = document.getElementById("gameIdInput").value;
    connection.invoke("GetGame", gameID).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("joinGame").addEventListener("click", function (event) {
    var name = document.getElementById("nameInput").value;
    var gameID = document.getElementById("gameIdInput").value;
    connection.invoke("JoinGame", name, gameID).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("startGame").addEventListener("click", function (event) {
    var gameID = document.getElementById("gameIdInput").value;
    connection.invoke("StartGame", gameID).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("sendAnswer").addEventListener("click", function (event) {
    var gameID = document.getElementById("gameIdInput").value;
    var answer = document.getElementById("answerInput").value;
    connection.invoke("SendPlayerAnswer", answer, gameID).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


connection.start().then(function () {
    document.getElementById("createGame").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});