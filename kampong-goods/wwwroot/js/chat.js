
var connection = new signalR.HubConnectionBuilder().withUrl("/Chathub").build();

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + ": " + msg;
    var list = document.createElement("datalist");

    list.textContent = encodedMsg;
    var li = document.createElement("li");
    li.style.backgroundColor = "lightblue";
    li.style.borderRadius = "90px";
    li.style.padding = "10px";
    li.textContent = encodedMsg;
    $("#receiveMessagesList").append(li);
});

connection.start().then(function () {
    $("#sendMessage").prop('disabled', false);
}).catch(function (err) {
    return console.error(err.toString());
});


$("#sendMessage").click(function () {
    var sender = $("#sender").val();
    var receiver = $("#receiver").val();
    var message = $("#message").val();

    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = sender + ": " + msg;
    var list = document.createElement("datalist");

    list.textContent = encodedMsg;
    var li = document.createElement("li");
    li.style.textAlign = "right";
    li.style.backgroundColor = "lightgray";
    li.style.borderRadius = "90px";
    li.style.padding = "10px";
    li.textContent = encodedMsg;
    $("#receiveMessagesList").append(li);

    if (receiver != "") {
        //send to a user
        connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        //send to all
        connection.invoke("SendMessage", sender, message).catch(function (err) {
            return console.error(err.toString());
        });
    }


    event.preventDefault();

});
