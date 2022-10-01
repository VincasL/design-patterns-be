using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BattleshipsApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PlayerController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: PlayerController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
    {
        return await _context.Players.ToListAsync();
    }

    //GET: PlayerController/Player/5
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(int id)
    {
        if (!PlayerExists(id))
        {
            return NotFound();
        }
        return await _context.Players.FindAsync(id);
    }

    // POST: PlayerController/AddPlayer
    [HttpPost]
    public async Task<ActionResult<AddPlayerDto>> AddPlayer(AddPlayerDto player)
    {
        var playerToAdd = _mapper.Map<AddPlayerDto, Player>(player);
        await _context.Players.AddAsync(playerToAdd);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<Player, AddPlayerDto>(playerToAdd));
    }

    //POST: PlayerController/AddPlayer
    [HttpPut]
    public async Task<ActionResult> EditPlayer(Player player)
    {
        _context.Entry(player).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlayerExists(player.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }
    //DELETE: PlayerController/DeletePlayer
    [HttpDelete("{id}")]
    public async Task<ActionResult<Player>> DeletePlayer(int id)
    {
        var playerToDelete = await GetPlayer(id);

        if (playerToDelete == null || playerToDelete.Value == null)
        {
            return NotFound($"Player with Id = {id} not found");
        }

        var result = await _context.Players.FirstOrDefaultAsync(player => player.Id == id);

        if (result != null)
        {
            _context.Players.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }
    private bool PlayerExists(int id)
    {
        return _context.Players.Any(player => player.Id == id);
    }
}