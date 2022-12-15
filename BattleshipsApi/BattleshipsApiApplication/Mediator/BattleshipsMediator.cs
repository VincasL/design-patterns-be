using BattleshipsApi.Hubs.Handlers;

namespace BattleshipsApi.Mediator;

public class BattleshipsMediator : IMediator
{
    private readonly JoinQueueHandler _joinQueueHandler;
    private readonly StartGameHandler _startGameHandler;
    private readonly SendGameDataHandler _sendGameDataHandler;
    private readonly AssignNewConnectionIdHandler _assignNewConnectionIdHandler;
    private readonly MakeMoveHandler _makeMoveHandler;
    private readonly MoveUnitHandler _moveUnitHandler;
    private readonly PlaceShipHandler _placeShipHandler;
    private readonly PlaceMineHandler _placeMineHandler;
    private readonly PlaceShipsHandler _placeShipsHandler;
    private readonly RotateShipHandler _rotateShipHandler;
    private readonly UndoPlaceShipHandler _undoPlaceShipHandler;
    private readonly AddComponentToShipHandler _addComponentToShipHandler;


    public BattleshipsMediator(JoinQueueHandler joinQueueHandler, StartGameHandler startGameHandler,
        SendGameDataHandler sendGameDataHandler, UndoPlaceShipHandler undoPlaceShipHandler,
        AssignNewConnectionIdHandler assignNewConnectionIdHandler, MakeMoveHandler makeMoveHandler,
        MoveUnitHandler moveUnitHandler, PlaceShipHandler placeShipHandler, PlaceShipsHandler placeShipsHandler,
        RotateShipHandler rotateShipHandler, PlaceMineHandler placeMineHandler, AddComponentToShipHandler addComponentToShipHandler)
    {
        _joinQueueHandler = joinQueueHandler;
        _startGameHandler = startGameHandler;
        _sendGameDataHandler = sendGameDataHandler;
        _undoPlaceShipHandler = undoPlaceShipHandler;
        _assignNewConnectionIdHandler = assignNewConnectionIdHandler;
        _makeMoveHandler = makeMoveHandler;
        _moveUnitHandler = moveUnitHandler;
        _placeShipHandler = placeShipHandler;
        _placeShipsHandler = placeShipsHandler;
        _rotateShipHandler = rotateShipHandler;
        _placeMineHandler = placeMineHandler;
        _addComponentToShipHandler = addComponentToShipHandler;
        _joinQueueHandler.SetMediator(this);
        _startGameHandler.SetMediator(this);
        _sendGameDataHandler.SetMediator(this);
        _undoPlaceShipHandler.SetMediator(this);
        _assignNewConnectionIdHandler.SetMediator(this);
        _makeMoveHandler.SetMediator(this);
        _moveUnitHandler.SetMediator(this);
        _placeShipHandler.SetMediator(this);
        _placeShipsHandler.SetMediator(this);
        _rotateShipHandler.SetMediator(this);
        _placeMineHandler.SetMediator(this);
        _addComponentToShipHandler.SetMediator(this);

    } 

    public async Task Send(ICommand command)
    {
        switch (command)
        {
            case AssignNewConnectionIdCommand cmd:
                await _assignNewConnectionIdHandler.Handle(cmd);
                break;
            case JoinQueueCommand cmd:
                await _joinQueueHandler.Handle(cmd);
                break;
            case MakeMoveCommand cmd:
                await _makeMoveHandler.Handle(cmd);
                break;
            case MoveUnitCommand cmd:
                await _moveUnitHandler.Handle(cmd);
                break;
            case PlaceMineCommand cmd:
                await _placeMineHandler.Handle(cmd);
                break;
            case PlaceShipCommand cmd:
                await _placeShipHandler.Handle(cmd);
                break;
            case PlaceShipsCommand cmd:
                await _placeShipsHandler.Handle(cmd);
                break;
            case RotateShipCommand cmd:
                await _rotateShipHandler.Handle(cmd);
                break;
            case StartGameCommand cmd:
                await _startGameHandler.Handle(cmd);
                break;
            case UndoPlaceShipCommand cmd:
                await _undoPlaceShipHandler.Handle(cmd);
                break;
            case SendGameDataCommand cmd:
                await _sendGameDataHandler.Handle(cmd);
                break;
            case AddComponentToShipCommand cmd:
                await _addComponentToShipHandler.Handle(cmd);
                break;
            default:
                throw new Exception("No handler assigned to command!");
        }
    }
}