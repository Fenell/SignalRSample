using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSignalR.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCountController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CountUser()
        {
            
        }
    }
}
