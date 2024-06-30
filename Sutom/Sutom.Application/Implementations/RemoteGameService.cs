using Sutom.Absrtractions;
using Sutom.Application.Models;
using Sutom.Core.Models;
using Sutom.Domain.Entites;
using System.Net.Http.Json;

namespace Sutom.Application.Implementations
{

    public class RemoteGameService : IGameService
    {
        private Game game { get; set; }
        private List<GuessResult> guessResults { get; set; } = [];
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string api = "http://10.0.2.2:5173/game";

        public RemoteGameService()
        {
            buildGame();
            buidGussResults();
        }

        public async Task<Game> StartNewGameAsync(int wordLength, int attempts)
        {
            //return await DoStartNewGameAsync(wordLength, attempts);
            game = buildGame();
            guessResults = buidGussResults();
            return game;
        }

        public async Task<GuessResult> MakeGuessAsync(int gameId, string guess)
        {
            //return await DoMakeGuessAsync(gameId, guess);
            int currnetGuessIndex = game.Guesses.Count;
            GuessResult guessResult = guessResults.ElementAt(currnetGuessIndex);
            game.Guesses.Add(new Guess
            {
                GameId = gameId,
                PlayerGuess = guess,
                GuessTime = DateTime.Now,
                GuessResult = guessResult
            });

            return guessResult;

        }

        public async Task<Game?> GetGameByIdAsync(int gameId)
        {
            //return await DoGetGameByIdAsync(gameId);
            return game;
        }

        public async Task<Game> DoStartNewGameAsync(int wordLength, int attempts)
        {
            var request = new StartGameRequest
            {
                WordLength = wordLength,
                MaxAttempts = attempts
            };

            var response = await _httpClient.PostAsJsonAsync($"{api}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Game>();

        }
        public async Task<GuessResult> DoMakeGuessAsync(int gameId, string guess)
        {
            var request = new GuessRequest
            {
                guess = guess
            };

            var response = await _httpClient.PostAsJsonAsync($"{api}/{gameId}/guess", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GuessResult>();
        }

        public async Task<Game?> DoGetGameByIdAsync(int gameId)
        {
            var response = await _httpClient.GetAsync($"{api}/{gameId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Game>();
        }

        private Game buildGame()
        {
            Game game = new Game();
            game.Id = 496810;
            game.Word = "caps";
            game.Date = DateTime.Now;
            game.Guesses = [];
            game.MaxAttemps = 4;
            game.Difficulty = 4;
            return game;
        }

        private List<GuessResult> buidGussResults()
        {
            List<GuessResult> guessResults = new List<GuessResult>
        {
            new GuessResult
            {
                Correct = false,
                LetterResults = new List<LetterResult>
                {
                    new LetterResult { Letter = 'c', Status = "correct" },
                    new LetterResult { Letter = 'b', Status = "wrong" },
                    new LetterResult { Letter = 'd', Status = "wrong" },
                    new LetterResult { Letter = 'r', Status = "wrong" }
                }
            },
            //new GuessResult
            //{
            //    Correct = false,
            //    LetterResults = new List<LetterResult>
            //    {
            //        new LetterResult { Letter = 't', Status = "wrong" },
            //        new LetterResult { Letter = 'p', Status = "misplaced" },
            //        new LetterResult { Letter = 'a', Status = "misplaced" },
            //        new LetterResult { Letter = 's', Status = "correct" }
            //    }
            //},
            new GuessResult
            {
                Correct = false,
                LetterResults = new List<LetterResult>
                {
                    new LetterResult { Letter = 't', Status = "wrong" },
                    new LetterResult { Letter = 'p', Status = "misplaced" },
                    new LetterResult { Letter = 'a', Status = "misplaced" },
                    new LetterResult { Letter = 's', Status = "correct" }
                }
            },
            new GuessResult
            {
                Correct = false,
                ShowInfoMessage = true,
                LetterResults = new List<LetterResult>
                {
                    new LetterResult { Letter = 'a', Status = "wrong" },
                    new LetterResult { Letter = 'a', Status = "correct" },
                    new LetterResult { Letter = 'a', Status = "wrong" },
                    new LetterResult { Letter = 'a', Status = "wrong" }
                }
            },
            new GuessResult
            {
                Correct = false,
                LetterResults = new List<LetterResult>
                {
                    new LetterResult { Letter = 'c', Status = "correct" },
                    new LetterResult { Letter = 'a', Status = "correct" },
                    new LetterResult { Letter = 's', Status = "misplaced" },
                    new LetterResult { Letter = 'p', Status = "misplaced" }
                }
            },
            new GuessResult
            {
                Correct = true,
                LetterResults = new List<LetterResult>
                {
                    new LetterResult { Letter = 'c', Status = "correct" },
                    new LetterResult { Letter = 'a', Status = "correct" },
                    new LetterResult { Letter = 'p', Status = "correct" },
                    new LetterResult { Letter = 's', Status = "correct" }
                }
            }
        };
            return guessResults;
        }

    }
}
