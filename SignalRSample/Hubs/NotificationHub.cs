using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class NotificationHub : Hub
{
    private static int NotificationCount = 0;
    private static List<string> Messages = new();

    public async Task SendMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            NotificationCount++;
            Messages.Add(message);
            await LoadMessage();
        }
    }

    public async Task LoadMessage()
    {
        await Clients.All.SendAsync("LoadNotification", Messages, NotificationCount);
    }
}