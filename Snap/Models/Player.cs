using Snap.Models.Enums;

namespace Snap.Models
{
    public class Player
    {
        public PlayerType PlayerType { get; set; }
        public string Name { get; set; }
        public Hand Hand { get; set; }
        public bool IsActive { get; set; }
    }
}