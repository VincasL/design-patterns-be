namespace BattleshipApiTests.Common;

public static class Mocks
{
    public static GameSession GameSession
    {
        get
        {
            var playerOne = new Player("connectionOne", "Stepas");
            var playerTwo = new Player("connectionOne", "Marinis");

            var session = new GameSession(playerOne, playerTwo);
            return session;
        }
    }
}