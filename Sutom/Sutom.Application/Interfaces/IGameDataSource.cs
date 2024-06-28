
using Sutom.Domain.Entites;

namespace Sutom.Application.Interfaces
{
    public interface IGameDataSource
    {
        Task<Game?> GetGameByIdAsync(int id);
        Task SaveGameAsync(Game game);
    }
}
