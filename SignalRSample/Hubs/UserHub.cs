using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class UserHub:Hub
{
    private static int TotalViews { get; set; }  
    
    public async Task<string> NewWindowLoaded(string name)
    {
        TotalViews++;
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
        return $"Total views from {name}: {TotalViews}";
    }
    
}