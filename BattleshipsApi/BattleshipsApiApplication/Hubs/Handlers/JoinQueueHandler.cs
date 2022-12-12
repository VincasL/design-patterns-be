using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record JoinQueueCommand(string Name, string Nation, string ConnectionId) : ICommand;

public class JoinQueueHandler : BaseHandler<JoinQueueCommand>
{
    public override async Task Handle(JoinQueueCommand command)
    {
        var name = command.Name;
        var nation = command.Nation;
        
        var player = new Player(command.ConnectionId, name);
        player.NationType = nation switch
        {
            "Russian" => NationType.Russian,
            "American" => NationType.American,
            "German" => NationType.German,
            _ => player.NationType
        };
        var moreThanTwoPlayersInTheQueue = BattleshipsFacade.AddPlayerToQueue(player);

        if (!moreThanTwoPlayersInTheQueue) return;
        
        var players = BattleshipsFacade.ReturnLastTwoPlayers();
        await Mediator.Send(new StartGameCommand(players.Item1, players.Item2));
    }

    public JoinQueueHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}