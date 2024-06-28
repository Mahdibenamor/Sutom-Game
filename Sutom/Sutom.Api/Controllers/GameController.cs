using Microsoft.AspNetCore.Mvc;
using Sutom.Application.Interfaces;
using Sutom.Core.Models;
using Sutom.Domain.Entites;

namespace Sutom.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> StartGame([FromBody] StartGameRequest request)
        {
            if (request == null || request.MaxAttempts <= 0 || request.WordLength <= 0)
            {
                return BadRequest("Invalid game parameters.");
            }
            Game game = await _gameService.StartNewGameAsync(wordLenght: request.WordLength, attemps: request.MaxAttempts);
            //game.Word = string.Empty;
            return Ok(game);
        }

        [HttpPost("{gameId}/guess")]
        public async Task<IActionResult> MakeGuess(int gameId, [FromBody] GuessRequest guess)
        {
            var result = await _gameService.MakeGuessAsync(gameId, guess.guess);
            return Ok(result);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameById(int gameId)
        {
            var game = await _gameService.GetGameByIdAsync(gameId);
            if(game == null)
            {
                return NotFound();
            }
            //game.Word = string.Empty;
            return Ok(game);
        }
    }
}
