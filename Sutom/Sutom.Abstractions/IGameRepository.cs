using Sutom.Domain.Entites;
namespace Sutom.Absrtractions
{
    public interface IGameRepository
    {
        Task<Game?> GetGameByIdAsync(int id);
        Task SaveGameAsync(Game game);
        Task<string> GetRandomWordAsync(int wordLendth);
        Task<IEnumerable<string>> GetRandomWordsForLenAsync(int wordLength);

    }
}
