using BattleshipsApi.Entities;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record StartGameCommand(Player Player1, Player Player2) : ICommand;


public class StartGameHandler : BaseHandler<StartGameCommand>
{
    public override async Task Handle(StartGameCommand command)
    {
        var session = BattleshipsFacade.CreateSession(command.Player1, command.Player2);
        await BattleshipsFacade.StartGame(session);
        await Mediator.Send(new SendGameDataCommand(session));
    }

    public StartGameHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}

