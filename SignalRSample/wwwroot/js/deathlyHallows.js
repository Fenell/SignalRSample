var countCloak = document.getElementById("cloakCounter");
var countStone = document.getElementById("stoneCounter");
var countWand = document.getElementById("wandCounter");
//Create connection
var connectionDeathlyHallows = new signalR.HubConnectionBuilder().withUrl("/hubs/deathlyhallows").build();

//Connect to methods that hub invokes aka receive notification from hub
connectionDeathlyHallows.on("updateDeadthlyHallows", (cloak, stone, wand) => {
    countCloak.innerText = cloak.toString();
    countStone.innerText  = stone.toString();
    countWand.innerText = wand.toString();
});

//invoke hub methods aka send notification to hub
//Send thì server không trả về Response -- invoke thì có


// start connection
function successful() {
    console.log("Connect to User hub successful");
    connectionDeathlyHallows.invoke("GetDeathlyHallows").then(value => {
        countCloak.innerText = value.cloak.toString();
        countStone.innerText  = value.stone.toString();
        countWand.innerText = value.wand.toString();
    });
}

function rejected() {

}

connectionDeathlyHallows.start().then(successful, rejected);