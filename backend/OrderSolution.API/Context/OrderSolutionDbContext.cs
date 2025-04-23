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
        public required DbSet<ServiceClientProducts> ServiceClientProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Impede a deleção em cascata

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

            modelBuilder.Entity<ServiceClientProducts>()
                .HasOne(sc => sc.ServiceClient)
                .WithMany()
                .HasForeignKey(sc => sc.ServiceClientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServiceClientProducts>()
                .HasOne(sc => sc.Service)
                .WithMany()
                .HasForeignKey(sc => sc.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServiceClientProducts>()
                .HasOne(sc => sc.User)
                .WithMany()
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}