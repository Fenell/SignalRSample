using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class ChatHub : Hub
{
    private readonly UserManager<IdentityUser> _userManager;

    public ChatHub(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("MessageReceived", user, message);
    }

    [Authorize]
    public async Task SendMessageToReceiver(string sender, string receiver, string message)
    {
        var user = await _userManager.FindByEmailAsync(receiver).ConfigureAwait(false);
        if (user != null)
        {
            await Clients.User(user.Id).SendAsync("MessageReceived", sender, receiver, message);
        }
    }
} 