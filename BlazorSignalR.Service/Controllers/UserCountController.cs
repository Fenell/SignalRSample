using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSignalR.Service.Hubs;
using BlazorSignalR.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalR.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCountController : ControllerBase
    {
        private readonly IHubContext<UserHub> _userHub;

        public UserCountController(IHubContext<UserHub> userHub)
        {
            _userHub = userHub;
        }
        
        
        [HttpGet]
        [Route("CountUser")]
        public async Task<IActionResult> CountUser()
        {
            Counter.UserCount++;
            await _userHub.Clients.All.SendAsync("newWindowLoaded", Counter.UserCount);
            return Ok("Done");
        }
    }
}
