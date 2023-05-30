using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Domain;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            //--
        }

        public DbSet<Deal> Deal { get; set; }
        public DbSet<Assignee> Assignee { get; set; }
        public DbSet<DealAssigne> DealAssigne { get; set; }
        public DbSet<Headquarter> Headquarter { get; set; }
        public DbSet<Priority> Priority { get; set; }
    }
}