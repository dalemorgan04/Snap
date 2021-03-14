using Snap.Models;
using Snap.Models.Enums;

namespace Snap.Services
{
    public interface IGameService
    {
        public Game Game { get;}

        public Player GetPlayer(PlayerType playerType);

        public void SetupGame();

        public Card PlayCard(Player player);

        public void CallSnap(Player player);
    }
}