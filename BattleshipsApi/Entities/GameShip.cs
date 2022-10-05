using BattleshipsApi.Enums;
using BattleshipsApi.Models;

namespace BattleshipsApi.Entities;

public class GameShip
{
    public ShipType Type { get; set; }
    public Tile Tile { get; set; }
    public bool IsHorizontal { get; set; }
}