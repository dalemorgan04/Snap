using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Snap.Models;
using Snap.Models.Enums;
using Snap.Services;
using Snap.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Snap.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;

        public GameController(ILogger<GameController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index",new GameViewModel());
        }

        [HttpGet]
        public IActionResult GetNewGame()
        {
            _gameService.SetupGame();
            var viewmodel = new GameViewModel(_gameService.Game);
            return View("Index", viewmodel);
        }

        [HttpGet]
        public IActionResult PlayCard(PlayerType playerType)
        {
            var player = _gameService.GetPlayer(playerType);
            _gameService.PlayCard(player);
            var viewmodel = new GameViewModel(_gameService.Game);
            return View("Index", viewmodel);
        }

        [HttpGet]
        public IActionResult CallSnap(PlayerType playerType)
        {
            var player = _gameService.GetPlayer(playerType);
            _gameService.CallSnap(player);
            var viewmodel = new GameViewModel(_gameService.Game);
            return View("Index", viewmodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
