using BattleshipsApi.Entities;

namespace BattleshipsApi.Template
{
    public interface IPlaceItem
    {
        public abstract void PlaceObject(Unit unit, Board board, CellCoordinates coordinates);
        public abstract void ObjectExists(Unit unit);
        // The 'Template Method' 
        public void Place(Unit unit, Board board, CellCoordinates coordinates)
        {
            ObjectExists(unit);
            PlaceObject(unit, board, coordinates);
        }
    }
}
