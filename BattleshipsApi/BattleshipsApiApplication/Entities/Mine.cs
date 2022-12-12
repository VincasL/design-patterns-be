using BattleshipsApi.Enums;
using BattleshipsApi.Template;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Entities
{
    public abstract class Mine : Unit, IPlaceItem
    {
        public int ExplosionRadius { get; set; }
        public int Damage { get; set; }
        public int ArmourStrength { get; set; }
        public MineType Type { get; set; }
        public bool HasExploded { get; set; }


        protected Mine(int explosionRadious, int dammage, int armourStrength, MineType type, bool hasExploded)
        {
            ExplosionRadius = explosionRadious;
            Damage = dammage;
            ArmourStrength = armourStrength;
            Type = type;
            HasExploded = hasExploded;
        }

        public Mine(){ }

        public override Unit Clone()
        {
            throw new NotImplementedException();
        }

        public void PlaceObject(Unit unit, Board board, CellCoordinates coordinates)
        {
            var mine = (Mine)unit;
            var cellToPlaceMineAt = board.Cells[coordinates.X, coordinates.Y];

            if (cellToPlaceMineAt.Mine != null)
            {
                throw new Exception("Mine already placed here");
            }

            if (mine.Type is MineType.Small or MineType.RemoteControlled)
            {
                cellToPlaceMineAt.Mine = mine;
                return;
            }

            // if huge mine: 

            if (cellToPlaceMineAt.X + 1 == board.BoardSize || cellToPlaceMineAt.X + 1 == board.BoardSize)
            {
                throw new Exception("overflow");
            }

            var cellsToPlaceHugeMineAt = new List<Cell>
            {
                board.Cells[cellToPlaceMineAt.X, cellToPlaceMineAt.Y],
                board.Cells[cellToPlaceMineAt.X + 1, cellToPlaceMineAt.Y],
                board.Cells[cellToPlaceMineAt.X, cellToPlaceMineAt.Y + 1],
                board.Cells[cellToPlaceMineAt.X + 1, cellToPlaceMineAt.Y + 1]
            };

            foreach (var cell in cellsToPlaceHugeMineAt)
            {
                cell.Mine = mine;
            }
        }

        public void ObjectExists(Unit unit)
        {
            if (unit == null)
            {
                throw new Exception("Mine doesn't exist");
            }
        }
    }
}
