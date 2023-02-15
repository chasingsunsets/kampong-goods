
var connection = new signalR.HubConnectionBuilder().withUrl("/Chathub").build();

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = msg;
    var date = new Date();
    var current_time = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    var spanString = user + " " + current_time;
    var list = document.createElement("datalist");

    list.textContent = encodedMsg;
    var li = document.createElement("li");
    li.className = "clearfix"

    var div = document.createElement("div");
    div.className = "message-data";
    li.appendChild(div);

    var span = document.createElement("span");
    div.appendChild(span);
    span.className = "message-data-time";
    span.textContent = spanString;

    var div = document.createElement("div");
    div.className = "message my-message";
    li.appendChild(div);

    div.textContent = encodedMsg;
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
    var encodedMsg = msg;
    var date = new Date();
    var current_time = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    var spanString = sender + " " + current_time;
    var list = document.createElement("datalist");

    list.textContent = encodedMsg;
    var li = document.createElement("li");
    li.className = "clearfix"

    var div = document.createElement("div");
    div.className = "message-data text-right";
    li.appendChild(div);

    var span = document.createElement("span");
    div.appendChild(span);
    span.className = "message-data-time";
    span.textContent = spanString;

    var image = document.createElement("img");
    div.appendChild(image);
    image.src = "/images/seventeen-wonwoo-1.jpg";
    image.style.width = "50px";
    image.style.height = "50px";

    var div = document.createElement("div");
    div.className = "message other-message float-right";
    li.appendChild(div);

    div.textContent = encodedMsg;
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
