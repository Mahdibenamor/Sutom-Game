using Sutom.Absrtractions;
using Sutom.Application.Models;
using Sutom.Core.Models;
using Sutom.Domain.Entites;
using System.Net.Http.Json;

namespace Sutom.Application.Implementations
{

    public class RemoteGameService : IGameService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string api = "http://localhost:5173/game";

        public RemoteGameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Game> StartNewGameAsync(int wordLength, int attempts)
        {
            var request = new StartGameRequest
            {
                WordLength = wordLength,
                MaxAttempts = attempts
            };

            var response = await _httpClient.PostAsJsonAsync($"{api}/start", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Game>();
        }

        public async Task<GuessResult> MakeGuessAsync(int gameId, string guess)
        {
            var request = new GuessRequest
            {
                guess = guess
            };

            var response = await _httpClient.PostAsJsonAsync($"{api}/{gameId}/guess", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GuessResult>();
        }

        public async Task<Game?> GetGameByIdAsync(int gameId)
        {
            var response = await _httpClient.GetAsync($"{api}/{gameId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Game>();
        }
    }
}
