using Sutom.Application.Interfaces;
using System.Collections.Concurrent;
namespace Sutom.Infrastructure.DataSources
{
    public class WordLocalDataSource : IWordLocalDataSource
    {
        private ConcurrentDictionary<int, List<string>> _wordsDict = new ConcurrentDictionary<int, List<string>>();
        private List<string> _words = [];
        private readonly Random _random = new Random();

        public Task<IEnumerable<string>> GetRandomWordsAsync()
        {
            return Task.FromResult<IEnumerable<string>>(_words);
        }

        public Task SaveWords(IEnumerable<string> words)
        {
            ConcurrentDictionary<int, List<string>> localDict = new ConcurrentDictionary<int, List<string>>();

            foreach (var word in words)
            {
                if (localDict.ContainsKey(word.Length))
                {
                    List<string> newWords = localDict[word.Length];
                    newWords.Add(word);
                    localDict[word.Length] = newWords;
                }
                else
                {
                    localDict[word.Length] = new List<string> { word };
                }
            }

            _words = words.ToList();
            _wordsDict = localDict;
            return Task.FromResult(_wordsDict);

        }

        public Task<IEnumerable<string>> GetRandomWordsForLength(int length)
        {
            List<string> result = new List<string> { };

            if (_wordsDict.ContainsKey(length))
            {
                result = _wordsDict[length];
            }
            return Task.FromResult<IEnumerable<string>>(result);
        }

        public async Task<string> GetRandomWordForLength(int length)
        {

            var words = await GetRandomWordsForLength(length);

            if (words.Any())
            {
                var random = new Random();
                return words.ElementAt(random.Next(words.Count()));
            }
            return string.Empty;
        }
    }
}
