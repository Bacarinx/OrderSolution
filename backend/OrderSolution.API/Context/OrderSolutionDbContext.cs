using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using OrderSolution.API.Entities;

namespace OrderSolution.API.Context
{
    public class OrderSolutionDbContext : DbContext
    {
#pragma warning disable IDE0290
        public OrderSolutionDbContext(DbContextOptions<OrderSolutionDbContext> opt) : base(opt) { }

        public required DbSet<User> Users { get; set; }
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Product> Products { get; set; }
        public required DbSet<Client> Clients { get; set; }
        public required DbSet<Service> Services { get; set; }
        public required DbSet<ServiceClient> ServiceClients { get; set; }
        public required DbSet<Tab> Tab { get; set; }
        public required DbSet<TabProducts> TabProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<ServiceClient>()
                .HasOne(sc => sc.Service)
                .WithMany()
                .HasForeignKey(sc => sc.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServiceClient>()
                .HasOne(sc => sc.User)
                .WithMany()
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Tab>()
                .HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Tab>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TabProducts>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TabProducts>()
                .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TabProducts>()
                .HasOne(p => p.Service)
                .WithMany()
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TabProducts>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}