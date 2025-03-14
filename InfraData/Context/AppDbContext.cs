using System.Runtime.InteropServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace InfraData.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users {get; set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients {get; set;}
        public DbSet<State> States {get; set;}
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .HasMany(e => e.Cities)
                .WithOne(cd => cd.State)
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                 .HasOne(c => c.State)
                 .WithMany(s => s.Clients)
                 .HasForeignKey(c => c.StateId)
                 .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Client>()
                .HasOne(c => c.City)
                .WithMany(ci => ci.Clients)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Comportamento de exclusão

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configura o provedor do MySQL
                optionsBuilder.UseMySql("Server=localhost;Database=EcomApiDb;User=root;Password=samuka.201232;", 
                    ServerVersion.AutoDetect("Server=localhost;Database=EcomApiDb;User=root;Password=samuka.201232;"));
            }
        }
    }
}