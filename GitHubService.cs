using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Text.Json;

namespace csharp_valuetask
{

    public class GitHubService
    {
        private readonly IMemoryCache _reposCache = new MemoryCache(new MemoryCacheOptions());
        private readonly HttpClient _httpClient = new();

        private string _accessToken = "put here your GitHub APi Access Token";

        public GitHubService()
        {
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Alliw", "1.0"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _accessToken);
        }

        public async Task<List<Repo>> GetRepoAsyncTask(string username)
        {
            string cacheKey = username;
            List<Repo> repos = _reposCache.Get<List<Repo>>(cacheKey);

            if (repos is null)
            {
                var result = await _httpClient.GetStringAsync($"https://api.github.com/users/{username}/repos");
                repos = JsonSerializer.Deserialize<List<Repo>>(result);
                _reposCache.Set(cacheKey, repos, TimeSpan.FromHours(1));
            }

            return repos;
        }

        public async ValueTask<List<Repo>> GetRepoAsyncValueTask(string username)
        {
            string cacheKey = username;
            List<Repo> repos = _reposCache.Get<List<Repo>>(cacheKey);

            if (repos is null)
            {
                var result = await _httpClient.GetStringAsync($"https://api.github.com/users/{username}/repos");
                repos = JsonSerializer.Deserialize<List<Repo>>(result);
                _reposCache.Set(cacheKey, repos, TimeSpan.FromHours(1));
            }

            return repos;
        }
    }
}
