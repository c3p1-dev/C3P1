using C3P1.Client.Components.Apps.Cat;
using C3P1.Client.Components.Apps.Tasklist;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace C3P1.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<TodoItem> Tasklist => Set<TodoItem>();
        public DbSet<Cat> Catlist => Set<Cat>();
        public DbSet<CatEntry> CatEntries => Set<CatEntry>();
    }
}
