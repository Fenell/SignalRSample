using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class DeathlyHallowsHub:Hub
{
    public Dictionary<string, int> GetDeathlyHallows()
    {
        return SD.DealthyHallowRace;
    }
}