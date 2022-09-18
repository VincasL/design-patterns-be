using System.ComponentModel.DataAnnotations;

namespace BattleshipService.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public bool IsFinished { get; set; }

        public int PlayerOneId { get; set; }

        public int PlayerTwoId { get; set; }

        public int MapSize { get; set; }

    }
}
