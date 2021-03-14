using Snap.Models;
using Snap.Models.Enums;
using System.Linq;

namespace Snap.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            GameHasStarted = false;
        }
        public GameViewModel(Game game)
        {
            GameHasStarted = true;
            var currentCard = game.TableDeck.Cards.LastOrDefault();
            CurrentCardDescription = $"{currentCard?.Value.ToString() ?? ""} {currentCard?.Suit.ToString() ??  "Empty table"}";

            IsPlayersTurn = game.PlayerTurn == game.Players.Where(p => p.PlayerType == PlayerType.Player).FirstOrDefault();

            PlayerCardCount = game.Players.Where(p => p.PlayerType == PlayerType.Player).FirstOrDefault().Hand?.Cards?.Count() ?? 0;
            ComputerCardCount = game.Players.Where(p => p.PlayerType == PlayerType.Computer).FirstOrDefault().Hand?.Cards?.Count() ?? 0;
            if (game.TableDeck.Cards.Count() > 1 )
            {
                ComputerCallSnap = game.TableDeck.Cards.LastOrDefault().Value == game.TableDeck.Cards[game.TableDeck.Cards.Count() - 2].Value;
            }

            SnapWasCalled = game.SnapEvent?.EventTriggered ?? false;
            if (SnapWasCalled)
            {
                SnapMessage = game.SnapEvent.IsSuccess ? $"Snap called and won by {game.SnapEvent.TriggerPlayer.Name}" : $"Snap called by {game.SnapEvent.TriggerPlayer.Name} but wasn't valid";
            }

            GameHasEnded = game.HasEnded;
            WinningPlayer = game.WinningPlayer?.Name ?? "";
        }

        public bool GameHasStarted { get; set; }
        public string CurrentCardDescription { get; set; }

        public int PlayerCardCount { get; set; }
        public bool IsPlayersTurn { get; set; }

        public int ComputerCardCount { get; set; }
        public bool ComputerCallSnap { get; set; } = false;

        public bool SnapWasCalled { get; set; }
        public string SnapMessage { get; set; }

        public bool GameHasEnded { get; set; }
        public string WinningPlayer { get; set; }
    }
}