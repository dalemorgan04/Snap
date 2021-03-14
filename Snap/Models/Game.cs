using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snap.Models
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public Deck TableDeck { get; set; }

        public Player PlayerTurn { get; set; }
        public SnapEvent SnapEvent { get; set; }

        public bool HasEnded { get; set; }
        public Player WinningPlayer { get; set; }
    }
}
