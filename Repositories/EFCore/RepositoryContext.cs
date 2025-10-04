using Entites.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<UserBase>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new UserConfig());
            // modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
