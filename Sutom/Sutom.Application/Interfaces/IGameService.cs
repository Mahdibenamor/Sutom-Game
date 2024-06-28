using Sutom.Application.Models;
using Sutom.Domain.Entites;

namespace Sutom.Application.Interfaces
{
    public interface IGameService
    {
        Task<Game> StartNewGameAsync(int wordLenght, int attemps);
        Task<GuessResult> MakeGuessAsync(int gameId, string guess);
        Task<Game?> GetGameByIdAsync(int gameId);
    }
}
