using Sutom.Application.Interfaces;
using Sutom.Core;
using Sutom.Domain.Entites;
using System.Collections.Concurrent;

namespace Sutom.Infrastructure.DataSources
{
    public class GameDataSource : IGameDataSource
    {
        private static readonly ConcurrentDictionary<int, Game?> _games = new ConcurrentDictionary<int, Game?>();
        Task<Game?> IGameDataSource.GetGameByIdAsync(int id)
        {

            _games.TryGetValue(id, out Game? game);
            return Task.FromResult(Utils.DeepCopy(game));
        }
        public Task SaveGameAsync(Game game)
        {
            _games[game.Id] = Utils.DeepCopy(game);
            return Task.CompletedTask;
        }
    }
}

