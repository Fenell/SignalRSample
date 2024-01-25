using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalR.Service.Hubs;

public class UserHub:Hub
{
    public static int UserCounter { get; set; } = 0;

    public async Task NewLoadedWindow()
    {
	    UserCounter++;
	    await Clients.All.SendAsync("newWindowLoaded", UserCounter);
    }
}