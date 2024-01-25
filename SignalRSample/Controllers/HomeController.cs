using Microsoft.AspNetCore.Mvc;
using SignalRSample.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;
using SignalRSample.Hubs;

namespace SignalRSample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHubContext<DeathlyHallowsHub> _deadthlyHub;
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<OrderHub> _orderHub;

    public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowsHub> deadthlyDeadthlyHub, ApplicationDbContext context, 
        IHubContext<OrderHub> orderHub)
    {
        _logger = logger;
        _deadthlyHub = deadthlyDeadthlyHub;
        _context = context;
        _orderHub = orderHub;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BasicChatApp()
    {
        return View();
    }

    public IActionResult HarryPotterHouses()
    {
        return View();
    }

    public IActionResult DeadthlyHallowRace()
    {
        return View();
    }

    public IActionResult DeathlyHallows(string type)
    {
        if (SD.DealthyHallowRace.ContainsKey(type))
        {
            SD.DealthyHallowRace[type]++;
        }

        _deadthlyHub.Clients.All.SendAsync("updateDeadthlyHallows",
            SD.DealthyHallowRace[SD.Cloak],
            SD.DealthyHallowRace[SD.Stone],
            SD.DealthyHallowRace[SD.Wand]);

        return Accepted();
    }

    public IActionResult Notification()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [ActionName("Order")]
    public async Task<IActionResult> Order()
    {
        string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
        string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

        Random rand = new Random();
        // Generate a random index less than the size of the array.  
        int index = rand.Next(name.Length);

        Order order = new Order()
        {
            Name = name[index],
            ItemName = itemName[index],
            Count = index
        };

        return View(order);
    }

    [ActionName("Order")]
    [HttpPost]
    public async Task<IActionResult> OrderPost(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        await _orderHub.Clients.All.SendAsync("newOrder");
        return RedirectToAction(nameof(Order));
    }

    [ActionName("OrderList")]
    public async Task<IActionResult> OrderList()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetAllOrder()
    {
        var productList = _context.Orders.ToList();
        return Json(new { data = productList });
    }
}