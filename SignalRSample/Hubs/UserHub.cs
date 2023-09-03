using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class UserHub:Hub
{
    private static int TotalViews { get; set; }  
    
    public async Task NewWindowLoaded()
    {
        TotalViews++;
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
    
}