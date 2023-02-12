
var connection = new signalR.HubConnectionBuilder().withUrl("/Chathub").build();

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + ": says " + msg;
    var list = document.createElement("datalist");

    if (user == "nat") {
        list.textContent = encodedMsg;
        console.log("list" + encodedMsg);
        var li = document.createElement("li");
        li.style.textAlign = "right";
        li.textContent = encodedMsg;
        $("#messagesList").append(li);
    }

    else if (user == "test") {
        list.textContent = encodedMsg;
        console.log("list" + encodedMsg);
        var li = document.createElement("li");
        li.style.textAlign = "left";
        li.textContent = encodedMsg;
        $("#messagesList").append(li);
    }
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
