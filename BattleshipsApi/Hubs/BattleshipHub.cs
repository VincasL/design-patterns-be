using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Handlers;
using BattleshipsApi.Helpers;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace BattleshipsApi.Hubs;

[SignalRHub]
public class BattleshipHub : Hub
{
    private QueueHandler _queueHandler;
    private SessionsHandler _sessionsHandler;
    private IMapper _mapper;
    private GameLogicHandler _gameLogicHandler;

    public BattleshipHub(QueueHandler queueHandler, SessionsHandler sessionsHandler, IMapper mapper, GameLogicHandler gameLogicHandler)
    {
        _queueHandler = queueHandler;
        _sessionsHandler = sessionsHandler;
        _mapper = mapper;
        _gameLogicHandler = gameLogicHandler;
    }
    
    public async Task JoinQueue(string name)
    {
        var player = new Player(Context.ConnectionId, name);
        var moreThanTwoPlayersInTheQueue = _queueHandler.AddPlayerToQueue(player);

        if (!moreThanTwoPlayersInTheQueue)
        {
            return;
        }
        
        var players = _queueHandler.ReturnLastTwoPlayers();
        StartGame(players.Item1, players.Item2);
    }

    public async Task PlaceShips(List<Ship> ships)
    {
        if (ships.Count != 5 || ships.Select(x => x.Type).Distinct().Count() != ships.Count)
        {
            throw new Exception("Invalid battleship configuration");
        }
        //TODO: might want to handle ships overlapping
        
        var session = _sessionsHandler.GetSessionByConnectionId(Context.ConnectionId);
        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        
        
        //TODO: move to different file
        foreach (var ship in ships)
        {
            board.Cells[ship.Cell.X][ship.Cell.Y].Ship = ship;

            if (ship.IsHorizontal)
            {
                for (var x = ship.Cell.X; x < ship.Type.GetShipLength(); x++)
                {
                    board.Cells[x][ship.Cell.Y].Ship = ship;
                }
            }
            else
            {
                for (var y = ship.Cell.Y; y < ship.Type.GetShipLength(); y++)
                {
                    board.Cells[ship.Cell.X][y].Ship = ship;
                }
            }
        }
        
        session.GetPlayerByConnectionId(Context.ConnectionId).PlacedShips = true;
        
        if (session.AllPlayersPlacedShips)
        {
            session.AreShipsPlaced = true;
            session.NextPlayerTurnConnectionId = session.PlayerOne.ConnectionId;
        }
        
        SendGameData(session);
    }

    public async Task MakeMove(Move move)
    {
        //TODO:  Move validation
        var session = _sessionsHandler.GetSessionByConnectionId(Context.ConnectionId);

        if (session.NextPlayerTurnConnectionId != Context.ConnectionId)
        {
            Console.WriteLine("it's not your move bro");
            return;
        }

        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        var hitCell = board.Cells[move.X][ move.Y];

        if (hitCell.Type != CellType.NotShot)
        {
            //This should not happen, the cell has been already hit previously, so do nothing
            return;
        }

        //TODO: cell hit logic
        var shipExistsInCell = true;

        if (shipExistsInCell)
        {
            //TODO: cell type = DamagedShip or DestroyedShip
        }
        else
        {
            hitCell.Type = CellType.Empty;
            session.SetMoveToNextPlayer();
        }
        
        SendGameData(session);
    }

    public async Task RequestData()
    {
        var session = _sessionsHandler.GetSessionByConnectionId(Context.ConnectionId);
        SendGameData(session);
    }


    public async void StartGame(Player player1, Player player2)
    {
        var session = _sessionsHandler.CreateSession(player1, player2);
        await Clients.Clients(player1.ConnectionId, player2.ConnectionId).SendAsync("startGame");
        SendGameData(session);
    }
    
    public async void SendGameData(Session session)
    {
        var gameDataPlayerOne = _gameLogicHandler.MapSessionToGameDataDtoPlayerOne(session);
        var gameDataPlayerTwo = _gameLogicHandler.MapSessionToGameDataDtoPlayerTwo(session);
        
        await Clients.Client(session.PlayerOne.ConnectionId).SendAsync("gameData", gameDataPlayerOne);
        await Clients.Client(session.PlayerTwo.ConnectionId).SendAsync("gameData", gameDataPlayerTwo);
    }
}

