using C3P1.Client.Components.Apps.VisualCarnet;
using System.Net.Http.Json;

namespace C3P1.Client.Services.Apps
{
    public class VisualCarnetClientService : IVisualCarnetService
    {
        private readonly HttpClient _httpClient;
        public VisualCarnetClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<FicCpt>> GetAccountListAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<FicCpt>>("api/apps/visualcarnet/list/accounts");

            return result!;
        }

        public async Task<bool> AddAccountAsync(Guid userId, FicCpt account)
        {
            var result = await _httpClient.PostAsJsonAsync<FicCpt>("api/apps/visualcarnet/add/account", account);

            return result.IsSuccessStatusCode;
        }

        public async Task<int> RemoveAllAccountsAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<int>("api/apps/visualcarnet/remove/accounts");

            return result;
        }

        public async Task<List<FicFam>> GetFamilyListAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<FicFam>>("api/apps/visualcarnet/list/families");

            return result!;
        }

        public async Task<bool> AddFamilyAsync(Guid userId, FicFam family)
        {
            var result = await _httpClient.PostAsJsonAsync<FicFam>("api/apps/visualcarnet/add/family", family);

            return result.IsSuccessStatusCode;
        }

        public async Task<int> RemoveAllFamiliesAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<int>("api/apps/visualcarnet/remove/families");

            return result;
        }

        public async Task<List<FicSfa>> GetSubFamilyListAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<FicSfa>>("api/apps/visualcarnet/list/subfamilies");

            return result!;
        }

        public async Task<bool> AddSubFamilyAsync(Guid userId, FicSfa subfamily)
        {
            var result = await _httpClient.PostAsJsonAsync<FicSfa>("api/apps/visualcarnet/add/subfamily", subfamily);

            return result.IsSuccessStatusCode;
        }

        public async Task<int> RemoveAllSubFamiliesAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<int>("api/apps/visualcarnet/remove/subfamilies");

            return result;
        }

        public async Task<List<WrkEcrLig>> GetRecordListAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<WrkEcrLig>>("api/apps/visualcarnet/list/records");

            return result!;
        }

        public async Task<bool> AddRecordAsync(Guid userId, WrkEcrLig record)
        {
            var result = await _httpClient.PostAsJsonAsync<WrkEcrLig>("api/apps/visualcarnet/add/subfamily", record);

            return result.IsSuccessStatusCode;
        }

        public async Task<int> RemoveAllRecordsAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<int>("api/apps/visualcarnet/remove/records");

            return result;
        }
    }
}
