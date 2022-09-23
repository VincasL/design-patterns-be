using BattleshipsApi.Handlers;
using BattleshipsApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IHubContext<BattleshipHub> _hub;
    private readonly TimerManager _timer;
        
    public TestController(IHubContext<BattleshipHub> hub, TimerManager timer)
    {
        _hub = hub;
        _timer = timer;
    }
    [HttpGet]
    public IActionResult Get()
    {
        if (!_timer.IsTimerStarted)
        {
            _timer.PrepareTimer(() =>
            {
                Console.WriteLine("Message sent");
                _hub.Clients.All.SendAsync("SendMessage");
                
            });
        }
        return Ok(new { Message = "Request Completed" });
    }
    
}