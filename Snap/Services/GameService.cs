using Snap.Models;
using Snap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snap.Services
{
    public class GameService : IGameService
    {
        public Game Game { get; private set; }

        public Player GetPlayer(PlayerType playerType) => Game.Players.Where(p => p.PlayerType == playerType).FirstOrDefault();

        public Player GetOpponent(Player player) => Game.Players.Where(p => p.PlayerType != player.PlayerType).FirstOrDefault();

        public void SetupGame()
        {
            Game = new Game();
            Game.TableDeck = BuildDeck();
            ShuffleDeck(Game.TableDeck);

            var players = new List<Player>();
            players.Add(
            new Player()
            {
                PlayerType = PlayerType.Player,
                Name = "Human",
                Hand = new Hand() { Cards = new List<Card>() }
            }
            );
            players.Add(
                new Player()
                {
                    PlayerType = PlayerType.Computer,
                    Name = "Computer",
                    Hand = new Hand() { Cards = new List<Card>() }
                }
            );
            DealHands(players);

            Game.Players = players;
            Game.HasEnded = false;
            Game.PlayerTurn = GetPlayer(PlayerType.Player);

            //Computer plays first card
            PlayCard(GetPlayer(PlayerType.Computer));
        }

        private Deck BuildDeck()
        {
            Deck deck = new Deck() { Cards = new List<Card>() };
            var cardValues = Enum.GetValues(typeof(CardValue)).Cast<CardValue>();
            var cardSuits = Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>();
            foreach (var value in cardValues)
            {
                foreach (var suit in cardSuits)
                {
                    deck.Cards.Add(new Card() { Suit = suit, Value = value });
                }
            }
            return deck;
        }

        private void ShuffleDeck(Deck deck)
        {
            var random = new Random();
            deck.Cards = deck.Cards.OrderBy(x => random.Next()).ToList();
        }

        private void DealHands(List<Player> players)
        {
            do
            {
                foreach (var player in players)
                {
                    var drawnCard = Game.TableDeck.Cards.FirstOrDefault();
                    player.Hand.Cards.Add(drawnCard);
                    Game.TableDeck.Cards.Remove(drawnCard);
                    if (Game.TableDeck.Cards.Count == 0) break;
                }
            } while (Game.TableDeck.Cards.Count > 0);
        }

        public void CheckEndGame()
        {
            foreach (var player in Game.Players)
            {
                if (player.Hand.Cards.Count == 0)
                {
                    Game.WinningPlayer = GetOpponent(player);
                    Game.HasEnded = true;
                }
            }
        }

        public Card PlayCard(Player player)
        {
            var drawnCard = player.Hand.Cards.FirstOrDefault();
            Game.TableDeck.Cards.Add(drawnCard);
            player.Hand.Cards.Remove(drawnCard);
            Game.PlayerTurn = GetOpponent(player);
            if (player.PlayerType == PlayerType.Player)
            {
                Game.SnapEvent = null;
            }

            CheckEndGame();

            return drawnCard;
        }

        public void CallSnap(Player player)
        {
            var snapEvent = new SnapEvent()
            {
                EventTriggered = true,
                TriggerPlayer = player
            };

            if (Game.TableDeck.Cards.Count > 1 &&
                Game.TableDeck.Cards[Game.TableDeck.Cards.Count - 1].Value == Game.TableDeck.Cards[Game.TableDeck.Cards.Count - 2].Value)
            {
                player.Hand.Cards.AddRange(Game.TableDeck.Cards);
                Game.TableDeck.Cards.RemoveAll(c => true);
                snapEvent.IsSuccess = true;
                Game.PlayerTurn = GetOpponent(player);
            }

            Game.SnapEvent = snapEvent;
            CheckEndGame();
        }
    }
}