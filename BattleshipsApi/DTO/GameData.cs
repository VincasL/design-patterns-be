using BattleshipsApi.Entities;

namespace BattleshipsApi.DTO;

public class GameData
{
    public PlayerDto PlayerOne { get; set; }
    public PlayerDto PlayerTwo { get; set; }
    public bool AreShipsPlaced { get; set; }
    public bool IsYourMove { get; set; }
}