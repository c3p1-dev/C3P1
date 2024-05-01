using C3P1.Client.Components.Apps.Cat;
using C3P1.Client.Services.Apps;
using C3P1.Data;
using Microsoft.EntityFrameworkCore;

namespace C3P1.Services.Apps
{
    public class CatServerService : ICatService
    {
        private readonly AppDbContext _context;
        public CatServerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCatAsync(Guid userId, Cat cat)
        {
            // fill data
            cat.Id = Guid.NewGuid();
            cat.UserId = userId;

            // add cat
            _context.Add(cat);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<List<Cat>> GetCatsAsync(Guid userId)
        {
            var result = await _context.Catlist
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<Cat?> GetCatFromIdAsync(Guid userId, Guid catId)
        {
            var result = await _context.Catlist
                .Where(c => c.UserId == userId && c.Id == catId)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> DeleteCatAsync(Guid userId, Guid catId)
        {
            // try to get cat from catId
            var cat = await _context.Catlist
                .Where(c => c.UserId == userId && c.Id == catId)
                .FirstOrDefaultAsync();

            if (cat == null)
            {
                // not found
                return false;
            }

            // found, remove cat
            _context.Remove(cat);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<bool> UpdateCatAsync(Cat cat)
        {
            // update cat
            _context.Update(cat);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<List<CatEntry>> GetEntriesAsync(Guid userId, Guid catId)
        {
            var result = await _context.CatEntries
                .Where(e => e.UserId == userId && e.CatId == catId)
                .ToListAsync();

            return result;
        }
        public async Task<bool> AddEntryAsync(Guid userId, CatEntry entry)
        {
            // fill data
            entry.Id = Guid.NewGuid();
            entry.UserId = userId;

            // add entry
            _context.Add(entry);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<bool> DeleteEntryAsync(Guid userId, Guid entryId)
        {
            // try to get cat from catId
            var entry = await _context.CatEntries
                .Where(e => e.UserId == userId && e.Id == entryId)
                .FirstOrDefaultAsync();

            if (entry == null)
            {
                // not found
                return false;
            }

            // found, remove cat
            _context.Remove(entry);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<bool> UpdateEntryAsync(CatEntry entry)
        {
            // update entry
            _context.Update(entry);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<List<CatEntry>> GetWeightDataAsync(Guid userId, Guid catId)
        {
            var result = await _context.CatEntries
                .Where(e => e.UserId == userId && e.CatId == catId && e.Weight != null)
                .ToListAsync();

            return result;
        }
    }
}
