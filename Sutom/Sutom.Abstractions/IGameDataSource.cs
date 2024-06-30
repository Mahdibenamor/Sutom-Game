
using Sutom.Domain.Entites;

namespace Sutom.Absrtractions
{
    public interface IGameDataSource
    {
        Task<Game?> GetGameByIdAsync(int id);
        Task SaveGameAsync(Game game);
    }
}
