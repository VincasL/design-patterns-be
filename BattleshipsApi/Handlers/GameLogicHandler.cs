using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Entities;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Handlers;

public class GameLogicHandler
{
    private IMapper _mapper;

    public GameLogicHandler(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public GameData MapSessionToGameDataDtoPlayerOne(Session session)
    {
        var gameData = _mapper.Map<GameData>(session);
        for (var i = 0; i < session.PlayerOne.Board.Cells.Length; i++)
        {
            var row = session.PlayerOne.Board.Cells[i];
            for (var j = 0; j < row.Length; j++)
            {
                var cell = row[j];
                if (cell.Ship != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
                {
                    gameData.PlayerOne.Board.Cells[i][j].Type = CellType.Ship;
                }
            }
        }

        return gameData;
    }
    
    public GameData MapSessionToGameDataDtoPlayerTwo(Session session)
    {
        var gameData = _mapper.Map<GameData>(session);
        for (var i = 0; i < session.PlayerTwo.Board.Cells.Length; i++)
        {
            var row = session.PlayerTwo.Board.Cells[i];
            for (var j = 0; j < row.Length; j++)
            {
                var cell = row[j];
                if (cell.Ship != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
                {
                    gameData.PlayerTwo.Board.Cells[i][j].Type = CellType.Ship;
                }
            }
        }

        return gameData;
    }
}