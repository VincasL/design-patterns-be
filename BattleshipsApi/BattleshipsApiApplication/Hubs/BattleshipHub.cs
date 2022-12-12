using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Hubs.Handlers;
using BattleshipsApi.Mediator;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace BattleshipsApi.Hubs;

[SignalRHub]
public class BattleshipHub : Hub
{
    private readonly BattleshipsMediator _mediator;

    public BattleshipHub(BattleshipsMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinQueue(string name, string nation)
    {
        await _mediator.Send(new JoinQueueCommand(name, nation, Context.ConnectionId));
    }

    public async Task PlaceShips()
    {
        await _mediator.Send(new PlaceShipsCommand(Context.ConnectionId));
    }

    public async Task PlaceShip(CellCoordinates cellCoordinates, ShipType type)
    {
        await _mediator.Send(new PlaceShipCommand(cellCoordinates, type, Context.ConnectionId));
    }

    public async Task PlaceMine(CellCoordinates cellCoordinates, MineType type)
    {
        await _mediator.Send(new PlaceMineCommand(cellCoordinates, type, Context.ConnectionId));
    }

    public async Task UndoPlaceShip(CellCoordinates cellCoordinates)
    {
        await _mediator.Send(new UndoPlaceShipCommand(cellCoordinates, Context.ConnectionId));
    }

    public async Task RotateShip(CellCoordinates cellCoordinates)
    {
        await _mediator.Send(new RotateShipCommand(cellCoordinates, Context.ConnectionId));
    }

    public async Task MakeMove(CellCoordinates cellCoordinates)
    {
        await _mediator.Send(new MakeMoveCommand(cellCoordinates, Context.ConnectionId));
    }
    public async Task MoveUnit(CellCoordinates coordinates, MoveDirection direction, bool isEnemyBoard)
    {
        await _mediator.Send(new MoveUnitCommand(coordinates, direction, isEnemyBoard, Context.ConnectionId));
    }

    public async void StartGame(Player player1, Player player2)
    {
        await _mediator.Send(new StartGameCommand(player1, player2));
    }

    public async void SendGameData(GameSession gameSession)
    {
        await _mediator.Send(new SendGameDataCommand(gameSession));
    }

    public async Task AssignNewConnectionId(string connectionId)
    {
        await _mediator.Send(new AssignNewConnectionIdCommand(connectionId, Context.ConnectionId));
    }
}