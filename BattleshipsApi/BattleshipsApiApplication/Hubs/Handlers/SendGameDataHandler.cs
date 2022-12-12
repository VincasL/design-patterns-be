
using BattleshipsApi.Entities;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record SendGameDataCommand(GameSession Session) : ICommand;

public class SendGameDataHandler : BaseHandler<SendGameDataCommand>
{
    public override async Task Handle(SendGameDataCommand command)
    {
        var gameSession = command.Session;
        var playerOneSessionData = ((GameSession)gameSession.Clone()).ShowPlayerOneShips().ShowPlayerTwoMines();
        var playerTwoSessionData = ((GameSession)gameSession.Clone()).SwapPlayers().ShowPlayerOneShips().ShowPlayerTwoMines();

        // var playerOneSessionDataShallowClone = ((GameSession)gameSession.ShallowClone()).ShowPlayerOneShips();


        Console.WriteLine($"main\n{gameSession}\n");
        Console.WriteLine($"deep\n{playerOneSessionData}\n");
        // Console.WriteLine($"shallow\n{playerOneSessionDataShallowClone}\n");

        await BattleshipsFacade.SendGameData(playerOneSessionData);
        await BattleshipsFacade.SendGameData(playerTwoSessionData);
    }

    public SendGameDataHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}