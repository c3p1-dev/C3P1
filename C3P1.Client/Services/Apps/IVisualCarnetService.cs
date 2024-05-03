using C3P1.Client.Components.Apps.VisualCarnet;
using Microsoft.AspNetCore.Components.Web;

namespace C3P1.Client.Services.Apps
{
    public interface IVisualCarnetService
    {
        public Task<List<FicCpt>> GetAccountListAsync(Guid userId);
        public Task<bool> AddAccountAsync(Guid userId, FicCpt account);
        public Task<int> RemoveAllAccountsAsync(Guid userId);

        public Task<List<FicFam>> GetFamilyListAsync(Guid userId);
        public Task<bool> AddFamilyAsync(Guid userId, FicFam family);
        public Task<int> RemoveAllFamiliesAsync(Guid userId);

        public Task<List<FicSfa>> GetSubFamilyListAsync(Guid userId);
        public Task<bool> AddSubFamilyAsync(Guid userId, FicSfa subfamily);
        public Task<int> RemoveAllSubFamiliesAsync(Guid userId);

        public Task<List<WrkEcrLig>> GetRecordListAsync(Guid userId);
        public Task<bool> AddRecordAsync(Guid userId, WrkEcrLig record);
        public Task<int> RemoveAllRecordsAsync(Guid userId);
    }
}
