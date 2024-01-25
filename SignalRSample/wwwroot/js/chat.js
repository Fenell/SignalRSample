document.getElementById("sendMessage").disabled = false;
let chatConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/chat").build();

chatConnection.on("MessageReceived", function (user, message) {
    let li = document.createElement("li");
    li.textContent = `${user} - ${message}`;
    
    document.getElementById("messagesList").style.listStyleType = "none";
    document.getElementById("messagesList").appendChild(li);
});
document.getElementById("sendMessage").addEventListener("click", function (event) {
    event.preventDefault();
    
    let senderEmail = document.getElementById("senderEmail").value;
    let receiverEmail = document.getElementById("receiverEmail").value;
    let message = document.getElementById("chatMessage").value;

    if (receiverEmail.length > 0) {
        chatConnection.send("SendMessageToReceiver", senderEmail, receiverEmail, message);
    } else {
        //Send message to all of the users
        chatConnection.send("SendMessageToAll", senderEmail, message).then(() => {
            document.getElementById("chatMessage").value = "";
        });
    }
});

//Start connection
chatConnection.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
});