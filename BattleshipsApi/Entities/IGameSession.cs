namespace BattleshipsApi.Entities;

public class IGameSession
{
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }
    public string NextPlayerTurnConnectionId { get; set; }
    public bool IsGameOver { get; set; } = false;
    public Settings Settings { get; set; }
    
    public bool AllPlayersPlacedShips =>
        PlayerOne.AreAllShipsPlaced && PlayerTwo.AreAllShipsPlaced;
}