using C3P1.Client.Components.Apps.Cat;
using System.Net.Http.Json;

namespace C3P1.Client.Services.Apps
{
    public class CatClientService : ICatService
    {
        private readonly HttpClient _httpClient;
        public CatClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Cat>> GetCatsAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Cat>>("api/apps/cat/list/cats");

            return result!;
        }
        public async Task<Cat?> GetCatFromIdAsync(Guid userId, Guid catId)
        {
            var result = await _httpClient.GetFromJsonAsync<Cat?>($"api/apps/cat/get/cat/{catId.ToString()}");

            return result;
        }
        public async Task<bool> AddCatAsync(Guid userId, Cat cat)
        {
            var result = await _httpClient.PostAsJsonAsync<Cat>("api/apps/cat/add", cat);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCatAsync(Guid userId, Guid catId)
        {
            var result = await _httpClient.DeleteAsync($"api/apps/cat/delete/cat/{catId.ToString()}");

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCatAsync(Cat cat)
        {
            var result = await _httpClient.PutAsJsonAsync<Cat>("api/apps/cat/update/cat", cat);

            return result.IsSuccessStatusCode;
        }

        public async Task<List<CatEntry>> GetEntriesAsync(Guid userId, Guid catId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<CatEntry>>($"api/apps/cat/get/entry/{catId.ToString()}");

            return result!;
        }

        public async Task<bool> AddEntryAsync(Guid userId, CatEntry entry)
        {
            var result = await _httpClient.PostAsJsonAsync<CatEntry>("api/apps/cat/add/entry", entry);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEntryAsync(Guid userId, Guid entryId)
        {
            var result = await _httpClient.DeleteAsync($"api/apps/cat/delete/entry/{entryId.ToString()}");

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEntryAsync(CatEntry entry)
        {
            var result = await _httpClient.PutAsJsonAsync<CatEntry>("api/apps/cat/update/entry", entry);

            return result.IsSuccessStatusCode;
        }

        public async Task<List<CatEntry>> GetWeightDataAsync(Guid userId, Guid catId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<CatEntry>>($"api/apps/cat/list/entries/weight/{catId.ToString()}");

            return result!;
        }
    }
}
