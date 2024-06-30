using Sutom.Absrtractions;
using Sutom.Domain.Entites;

namespace Sutom.Application.Implementations
{
    public class GameRepository : IGameRepository
    {
        private readonly IWordLocalDataSource _localWordDataSourceStrategy;
        private readonly IWordRemoteDataSource _remoteWordDataSourceStrategy;
        private readonly IGameDataSource _gameDataSource;
        public GameRepository(
            IWordRemoteDataSource remoteWordDataSourceStrategy,
            IWordLocalDataSource localWordDataSourceStrategy,
            IGameDataSource gameDataSource)
        {
            _remoteWordDataSourceStrategy = remoteWordDataSourceStrategy;
            _localWordDataSourceStrategy = localWordDataSourceStrategy;
            _gameDataSource = gameDataSource;
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _gameDataSource.GetGameByIdAsync(id);
        }

        public async Task SaveGameAsync(Game game)
        {
             await _gameDataSource.SaveGameAsync(game);
        }
        public async Task<string> GetRandomWordAsync(int wordLendth)
        {
            IEnumerable<string> localWords = await _localWordDataSourceStrategy.GetRandomWordsAsync();
            if (!localWords.Any())
            {
                IEnumerable<string> remoteWords = await _remoteWordDataSourceStrategy.GetRandomWordsAsync();
                await _localWordDataSourceStrategy.SaveWords(remoteWords);
            }
            return await _localWordDataSourceStrategy.GetRandomWordForLength(wordLendth);
        }

       public async Task<IEnumerable<string>> GetRandomWordsForLenAsync(int wordLendth) 
        {
            return await _localWordDataSourceStrategy.GetRandomWordsForLength(wordLendth);
        }

    }
}
