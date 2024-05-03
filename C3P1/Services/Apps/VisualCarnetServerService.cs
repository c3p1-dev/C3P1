using C3P1.Client.Components.Apps.VisualCarnet;
using C3P1.Client.Services.Apps;
using C3P1.Data;
using Microsoft.EntityFrameworkCore;

namespace C3P1.Services.Apps
{
    public class VisualCarnetServerService : IVisualCarnetService
    {
        private readonly VisualCarnetContext _context;
        public VisualCarnetServerService(VisualCarnetContext context)
        {
            _context = context;
        }

        public async Task<List<FicCpt>> GetAccountListAsync(Guid userId)
        {
            // get accounts
            var result = await _context.FicCpts
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> AddAccountAsync(Guid userId, FicCpt account)
        {
            // fill data
            account.Id = Guid.NewGuid();
            account.UserId = userId;

            // add account
            _context.Add(account);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<int> RemoveAllAccountsAsync(Guid userId)
        {
            // delete all records from FicCpt table
            foreach (var account in _context.FicCpts)
            {
                _context.Remove(account);
            }

            int recorded = await _context.SaveChangesAsync();
            return recorded;
        }

        public async Task<List<FicFam>> GetFamilyListAsync(Guid userId)
        {
            // get accounts
            var result = await _context.FicFams
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> AddFamilyAsync(Guid userId, FicFam family)
        {
            // fill data
            family.Id = Guid.NewGuid();
            family.UserId = userId;

            // add account
            _context.Add(family);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<int> RemoveAllFamiliesAsync(Guid userId)
        {
            // delete all records from FicCpt table
            foreach (var family in _context.FicFams)
            {
                _context.Remove(family);
            }

            int recorded = await _context.SaveChangesAsync();
            return recorded;
        }

        public async Task<List<FicSfa>> GetSubFamilyListAsync(Guid userId)
        {
            // get accounts
            var result = await _context.FicSfas
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> AddSubFamilyAsync(Guid userId, FicSfa subfamily)
        {
            // fill data
            subfamily.Id = Guid.NewGuid();
            subfamily.UserId = userId;

            // add account
            _context.Add(subfamily);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<int> RemoveAllSubFamiliesAsync(Guid userId)
        {
            // delete all records from FicCpt table
            foreach (var subfamily in _context.FicSfas)
            {
                _context.Remove(subfamily);
            }

            int recorded = await _context.SaveChangesAsync();
            return recorded;
        }

        public async Task<List<WrkEcrLig>> GetRecordListAsync(Guid userId)
        {
            // get accounts
            var result = await _context.WrkEcrLigs
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> AddRecordAsync(Guid userId, WrkEcrLig record)
        {
            // fill data
            record.Id = Guid.NewGuid();
            record.UserId = userId;

            // add account
            _context.Add(record);
            int recorded = await _context.SaveChangesAsync();

            return (recorded == 1);
        }

        public async Task<int> RemoveAllRecordsAsync(Guid userId)
        {
            // delete all records from FicCpt table
            foreach (var record in _context.WrkEcrLigs)
            {
                _context.Remove(record);
            }

            int recorded = await _context.SaveChangesAsync();
            return recorded;
        }
    }
}
