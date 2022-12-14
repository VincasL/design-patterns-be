using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record AssignNewConnectionIdCommand(string ConnectionId, string ContextConnectionId) : ICommand;


public class AssignNewConnectionIdHandler: BaseHandler<AssignNewConnectionIdCommand>
{
    public override async Task Handle(AssignNewConnectionIdCommand command)
    {
        var session = BattleshipsFacade.GetSessionByConnectionId(command.ConnectionId);
        BattleshipsFacade.BindNewConnectionIdToPlayer(command.ConnectionId, command.ContextConnectionId, session);
        await Mediator.Send(new SendGameDataCommand(session));
    }

    public AssignNewConnectionIdHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}