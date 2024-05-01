using C3P1.Client.Components.Apps.Cat;
using System.ComponentModel;

namespace C3P1.Client.Services.Apps
{
    public interface ICatService
    {
        // Cats
        public Task<List<Cat>> GetCatsAsync(Guid userId);
        public Task<Cat?> GetCatFromIdAsync(Guid userId, Guid catId);
        public Task<bool> AddCatAsync(Guid userId, Cat cat);
        public Task<bool> DeleteCatAsync(Guid userId, Guid catId);
        public Task<bool> UpdateCatAsync(Cat cat);

        // Cat entries
        public Task<List<CatEntry>> GetEntriesAsync(Guid userId, Guid catId);
        public Task<bool> AddEntryAsync(Guid userId, CatEntry entry);
        public Task<bool> DeleteEntryAsync(Guid userId, Guid entryId);
        public Task<bool> UpdateEntryAsync(CatEntry entry);
        public Task<List<CatEntry>> GetWeightDataAsync(Guid userId, Guid catId);
    }
}
