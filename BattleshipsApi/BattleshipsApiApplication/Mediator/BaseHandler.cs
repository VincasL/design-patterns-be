using BattleshipsApi.Facades;
using BattleshipsApi.Hubs.Handlers;

namespace BattleshipsApi.Mediator;

public abstract class BaseHandler<T> where T : ICommand
{
    protected IMediator Mediator;
    protected readonly BattleshipsFacade BattleshipsFacade;

    protected BaseHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null)
    {
        BattleshipsFacade = battleshipsFacade;
        Mediator = mediator;
    }

    public void SetMediator(IMediator mediator)
    {
        Mediator = mediator;
    }

    public abstract Task Handle(T command);
}