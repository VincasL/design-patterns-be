namespace BattleshipApiTests.Entities;
using AutoFixture.NUnit3;
using BattleshipsApi.Entities.Mines;
using BattleshipsApi.Entities.Missiles;
using NUnit.Framework;

public class BoardTests
{
    Cell[,] cells = new Cell[3, 3];
    [SetUp]
    public void Setup()
    {
        Cell cell1 = new Cell(0, 0);
        Cell cell2 = new Cell(0, 1);
        Cell cell3 = new Cell(0, 2);
        Cell cell4 = new Cell(1, 0);
        Cell cell5 = new Cell(1, 1);
        Cell cell6 = new Cell(1, 2);
        Cell cell7 = new Cell(2, 0);
        Cell cell8 = new Cell(2, 1);
        Cell cell9 = new Cell(2, 2);

        cells[0, 0] = cell1;
        cells[0, 1] = cell2;
        cells[0, 2] = cell3;
        cells[1, 0] = cell4;
        cells[1, 1] = cell5;
        cells[1, 2] = cell6;
        cells[2, 0] = cell7;
        cells[2, 1] = cell8;
        cells[2, 2] = cell9;
    }

    [Test,AutoData]
    public void GetEnumerator_AddedShips_ShouldIterateCorrectAmmount()
    {
        List<Unit> list = new List<Unit>();
        Board board = new Board(cells, 3, 0);

        var ship1 = new Destroyer();
        var ship2 = new Battleship();
        var mine1= new SmallMine();

        cells[0, 0].Add(ship1);
        cells[0, 1].Add(ship2);
        cells[0, 2].Add(mine1);

        var iterator=board.GetShipIterator();
        var enumerator=iterator.GetEnumerator();
        
        foreach (var a in enumerator)
        {
            list.Add((Ship)a);
        }

        Assert.That(list.Count, Is.EqualTo(2));
    }

    [Test, AutoData]
    public void GetEnumerator_AddedMines_ShouldIterateCorrectAmmount()
    {
        List<Unit> list = new List<Unit>();
        Board board = new Board(cells, 3, 0);

        var unit1 = new HugeMine();
        var unit2 = new SmallMine();
        var unit3 = new Battleship();

        cells[0, 0].Add(unit1);
        cells[0, 1].Add(unit2);
        cells[0, 2].Add(unit3);

        var iterator = board.GetMineIterator();
        var enumerator = iterator.GetEnumerator();

        foreach (var a in enumerator)
        {
            list.Add((Mine)a);
        }

        Assert.That(list.Count, Is.EqualTo(2));
    }

    [Test, AutoData]
    public void GetEnumerator_AddedMissiles_ShouldIterateCorrectAmmount()
    {
        List<Unit> list = new List<Unit>();
        Board board = new Board(cells, 3, 0);

        var unit1 = new DefaultMissile();
        var unit2 = new DefaultMissile();
        var unit3 = new Battleship();

        cells[0, 0].Add(unit1);
        cells[0, 1].Add(unit2);
        cells[0, 2].Add(unit3);

        var iterator = board.GetMissileIterator();
        var enumerator = iterator.GetEnumerator();

        foreach (var a in enumerator)
        {
            list.Add((Missile)a);
        }

        Assert.That(list.Count, Is.EqualTo(2));
    }


    [Test, AutoData]
    public void GetEnumerator_NoShips_ShouldNotBreak()
    {
        List<Unit> list = new List<Unit>();
        Board board = new Board(cells, 3, 0);

        var unit1 = new HugeMine();
        var unit2 = new SmallMine();
        var mine1 = new SmallMine();

        cells[0, 0].Add(unit1);
        cells[0, 1].Add(unit2);
        cells[0, 2].Add(mine1);

        var iterator = board.GetShipIterator();
        var enumerator = iterator.GetEnumerator();

        foreach (var a in enumerator)
        {
            list.Add((Ship)a);
        }

        Assert.That(list.Count, Is.EqualTo(0));
    }

    [Test, AutoData]
    public void GetEnumerator_NoMines_ShouldNotBreak()
    {
        List<Unit> list = new List<Unit>();
        Board board = new Board(cells, 3, 0);

        var unit1 = new Destroyer();
        var unit2 = new Battleship();
        var unit3 = new Battleship();

        cells[0, 0].Add(unit1);
        cells[0, 1].Add(unit2);
        cells[0, 2].Add(unit3);

        var iterator = board.GetMineIterator();
        var enumerator = iterator.GetEnumerator();

        foreach (var a in enumerator)
        {
            list.Add((Mine)a);
        }

        Assert.That(list.Count, Is.EqualTo(0));
    }

    [Test, AutoData]
    public void GetEnumerator_NoMissiles_ShouldNotBreak()
    {
        List<Unit> list = new List<Unit>();
        Board board = new Board(cells, 3, 0);

        var unit1 = new Destroyer();
        var unit2 = new Battleship();
        var unit3 = new Battleship();

        cells[0, 0].Add(unit1);
        cells[0, 1].Add(unit2);
        cells[0, 2].Add(unit3);

        var iterator = board.GetMissileIterator();
        var enumerator = iterator.GetEnumerator();

        foreach (var a in enumerator)
        {
            list.Add((Missile)a);
        }

        Assert.That(list.Count, Is.EqualTo(0));
    }
}