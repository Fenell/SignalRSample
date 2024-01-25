document.getElementById("sendButton").disabled = true;
let btn_send_message = document.getElementById("sendButton");

//Create connection 
let notificationConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/notification").build();

notificationConnection.on("LoadNotification", function (message, notificationCounter) {
    document.getElementById("messageList").innerHTML = "";
    let counter = document.getElementById("notificationCounter");
    counter.innerHTML = `<span>(${notificationCounter})</span>`
    
    for (let i = message.length - 1; i >= 0; i--) {
        let li = document.createElement("li");
        li.textContent = "Notification - " + message[i];
        document.getElementById("messageList").appendChild(li);
    }
})

btn_send_message.addEventListener("click", (event) => {
    let message = document.getElementById("notificationInput");
    notificationConnection.send("SendMessage", message.value).then(() => {
        message.value = "";
    });
    event.preventDefault();
});


//Start connecion
notificationConnection.start().then(function () {
    console.log("Connect to notification hub successful");
    notificationConnection.send("LoadMessage");
    document.getElementById("sendButton").disabled = false;
});