namespace BattleshipsApi.Models
{
    public class Move
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int TileId { get;set; }

        public int PlayerId { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

    }
}
