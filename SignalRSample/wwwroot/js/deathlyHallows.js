//Create connection
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

//Connect to methods that hub invokes aka receive notification from hub
 connection.on("updateTotalViews", (value)=>{
     var newCountSpan = document.getElementById("totalViewsCounter");
     newCountSpan.innerText = value.toString();
 });

//invoke hub methods aka send notification to hub
//Send thì server không trả về Response -- invoke thì có
function newWindowLoadedOnClient(){
     connection.invoke("NewWindowLoaded", "Fenell").then(value => console.log(value));
}

// start connection
function successful(){
     console.log("Connect to User hub successful");
     newWindowLoadedOnClient();
}
function rejected(){
     
}
connection.start().then(successful, rejected);