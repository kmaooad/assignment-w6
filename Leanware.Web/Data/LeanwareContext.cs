using KmaOoad18.Leanware.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace KmaOoad18.Leanware.Web.Data
{
    public class LeanwareContext : DbContext
    {
        // Add DbSets for your entities
        public DbSet<UserStory> UserStories { get; set; }

        public LeanwareContext(DbContextOptions<LeanwareContext> options) : base(options) { }

        public LeanwareContext() { }
    }
}
