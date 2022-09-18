using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipService.Data.Models
{
    public class Game_Mine
    {
        public int Id { get; set; }

        public int MineId { get; set; }

        public int GameId { get; set; }

        public int TileId { get; set; }

        public int PlayerId { get; set; }

    }
}
