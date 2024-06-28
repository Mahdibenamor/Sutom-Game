using Sutom.Application.Interfaces;
using Sutom.Application.Models;
using Sutom.Core;
using Sutom.Domain.Entites;

namespace Sutom.Application.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> StartNewGameAsync(int wordLenght, int maxAttemps)
        {
            var word = await _gameRepository.GetRandomWordAsync(wordLendth: wordLenght);
            var game = new Game
            {
                Id = Utils.GenerateRandomId(),
                Word = word,
                Date = DateTime.Now,
                Guesses = new List<Guess>(),
                MaxAttemps = maxAttemps,
                Difficulty = wordLenght
            };

            await _gameRepository.SaveGameAsync(game);
            return game;
        }

        public async Task<GuessResult> MakeGuessAsync(int gameId, string guess)
        {
            var game = await _gameRepository.GetGameByIdAsync(gameId);
            if (game == null)
            {
                throw new ArgumentException("Invalid game ID");
            }


            var result = new GuessResult();
            var word = game.Word.ToLower();
            guess = guess.ToLower();

            IEnumerable<string> wordsDict = await _gameRepository.GetRandomWordsForLenAsync(game.Difficulty);
            if (!wordsDict.Contains(guess))
            {
                result.ShowInfoMessage = true;
                result.Correct = false;
                return result;
            }


            if (guess == word)
            {
                result.Correct = true;
                foreach (var letter in guess)
                {
                    result.LetterResults.Add(new LetterResult { Letter = letter, Status = "correct" });
                }
            }
            else
            {
                result.LetterResults = BuildLetterResults(word:word, guess:guess);
            }

            game.Guesses.Add(new Guess
            {
                GameId = gameId,
                PlayerGuess = guess,
                GuessTime = DateTime.Now,
                GuessResult = result
            });

            await _gameRepository.SaveGameAsync(game);

            return result;
        }

        private List<LetterResult> BuildLetterResults(string word, string guess)
        {
            List<LetterResult> letterResults = new List<LetterResult>();
            var wordLetterCounts = word.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            var guessLetterCounts = guess.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == word[i])
                {
                    letterResults.Add(new LetterResult { Letter = guess[i], Status = "correct" });
                    wordLetterCounts[guess[i]]--;
                }
                else if (word.Contains(guess[i]) && wordLetterCounts[guess[i]] > 0)
                {
                    letterResults.Add(new LetterResult { Letter = guess[i], Status = "misplaced" });
                    wordLetterCounts[guess[i]]--;
                }
                else
                {
                    letterResults.Add(new LetterResult { Letter = guess[i], Status = "wrong" });
                }
            }
            return letterResults;
        }

        public async Task<Game?> GetGameByIdAsync(int gameId)
        {
            return await _gameRepository.GetGameByIdAsync(gameId);
        }


    }
}
