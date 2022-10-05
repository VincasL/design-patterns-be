namespace BattleshipsApi.Enums;

public enum ShipType
{
    Carrier, // 5 tiles
    Battleship, // 4 tiles
    Cruiser, // 3 tiles
    Submarine, // 3 tiles
    Destroyer // 2 tiles
}

public enum PlayerTurn
{
    FirstPlayer,
    SecondPlayer
}

public enum CellType{
    NotShot,
    Empty,
    Ship,
    DamagedShip,
    DestroyedShip
}