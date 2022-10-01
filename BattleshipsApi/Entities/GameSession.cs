namespace BattleshipsApi.Entities;

public class GameSession
{
    public GamePlayer PlayerOne { get; set; }
    public GamePlayer PlayerTwo { get; set; }

    public GameSession(GamePlayer playerOne, GamePlayer playerTwo)
    {
        this.PlayerOne = playerOne;
        this.PlayerTwo = playerTwo;
    }
}