using Snap.Models;

namespace Snap.ViewModels
{
    public class GameEndViewModel
    {
        public GameEndViewModel(Player player)
        {
            WinningPlayerName = player.Name;
        }
        public string WinningPlayerName { get; set; }
    }
}