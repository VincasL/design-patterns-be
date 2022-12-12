using BattleshipsApi.Hubs.Handlers;

namespace BattleshipsApi.Mediator;

public interface IMediator
{
    Task Send(ICommand command);
}