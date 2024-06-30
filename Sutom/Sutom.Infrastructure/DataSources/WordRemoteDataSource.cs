using Sutom.Absrtractions;
using System.Text.Json;

namespace Sutom.Infrastructure.DataSources
{
    public class WordRemoteDataSource : IWordRemoteDataSource
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<IEnumerable<string>> GetRandomWordsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://random-word-api.herokuapp.com/all");
            string[] words = JsonSerializer.Deserialize<string[]>(response);
            if (words != null && words.Length != 0)
            {
                return words;
            }
            return [];
        }
    }
}
