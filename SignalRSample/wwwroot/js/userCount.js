//Create connection
var connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/userCount").build();

//Connect to methods that hub invokes aka receive notification from hub
connection.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

//invoke hub methods aka send notification to hub
//Send thì server không trả về Response -- invoke thì có
function newWindowLoadedOnClient() {
    connection.invoke("NewWindowLoaded", "Fenell").then(value => console.log(value));
}

// start connection
function successful() {
    console.log("Connect to User hub successful");
    newWindowLoadedOnClient();
}

function rejected() {

}

connection.onclose((error) => {
    document.body.style.background = "red";
});

connection.onreconnecting((error)=>{
    document.body.style.background = "orange";
});

connection.onreconnected((connectionId)=>{
    document.body.style.background = "green";
})

connection.start().then(successful, rejected).catch((error) => {
    console.error(error.message);
});