using NUnit.Framework;
using Snap.Models;
using Snap.Services;
using Moq;
using Snap.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SnapTests.Services
{
    [TestFixture]
    public class GameServiceTests
    {
        private IGameService gameService { get; set; }

        [OneTimeSetUp]
        public void Initialise()
        {
            gameService = new GameService();
        }

        [TestCase]
        public void Deck_Contains_52_Cards()
        {
            //Arrange
            gameService.SetupGame();

            //Assert
            var totalCards = gameService.Game.TableDeck.Cards.Count;
            foreach (var player in gameService.Game.Players)
            {
                totalCards += player.Hand.Cards.Count;
            }
            Assert.AreEqual(52, totalCards );
        }

        [TestCase]
        public void Played_Card_Moves_To_Deck()
        {
            //Arrange
            gameService.SetupGame();
            var player = gameService.GetPlayer(PlayerType.Player);
            var playersNextCard = player.Hand.Cards.FirstOrDefault();

            //Act
            gameService.PlayCard(player);

            //Assert
            var currentTableCard = gameService.Game.TableDeck.Cards.LastOrDefault();
            Assert.AreEqual(playersNextCard, gameService.Game.TableDeck.Cards.LastOrDefault());
        }
    }
}