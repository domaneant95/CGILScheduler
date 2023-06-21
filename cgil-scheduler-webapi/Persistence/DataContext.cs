using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DataContext(DbContextOptions options) : base(options)
        {
            //--
        }

        public DbSet<Deal> Deal { get; set; }
        public DbSet<Assignee> Assignee { get; set; }
        public DbSet<Headquarter> Headquarter { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<FileItem> FileItems { get; set; }
    }
}