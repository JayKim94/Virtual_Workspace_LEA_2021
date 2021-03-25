using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;

namespace VirtualWorkspace_Mirzaie_Kim.EntityFramework
{
    public class VirtualWorkspaceDbContext : DbContext
    {
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<WorkspaceItem> WorkspaceItems { get; set; }
        public DbSet<ResourceDirectory> ResourceDirectories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=workspaceDB.db");
            optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceDirectory>().ToTable("ResouceDirectories");

            base.OnModelCreating(modelBuilder);
        }
    }
}
