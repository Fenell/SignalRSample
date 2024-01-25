using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class HouseGroupHub : Hub
{
    public static List<string> GroupJoined = new List<string>();

    public async Task JoinHouse(string houseName)
    {
        if (!GroupJoined.Contains(Context.ConnectionId + ":" + houseName))
        {
            GroupJoined.Add(Context.ConnectionId + ":" + houseName);
            string houseList = "";
            foreach (string item in GroupJoined)
            {
                if (item.Contains(Context.ConnectionId))
                {
                    houseList += item.Split(':')[1] + " ";
                }
            }
            await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), true);
            await Clients.Others.SendAsync("newMemberAddHouse", houseName);
            await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
        }
    }

    public async Task LeaveHouse(string houseName)
    {
        if (GroupJoined.Contains(Context.ConnectionId + ":" + houseName))
        {
            GroupJoined.Remove(Context.ConnectionId + ":" + houseName);
            string houseList = "";
            foreach (string item in GroupJoined)
            {
                if (item.Contains(Context.ConnectionId))
                {
                    houseList += item.Split(':')[1] + " ";
                }
            }
            await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), false);
            await Clients.Others.SendAsync("newMemberRemoveHouse", houseName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
        }
    }

    public async Task TriggerNotification(string houseName)
    {
        await Clients.Group(houseName).SendAsync("triggerHouseNotification", houseName);
    }
}