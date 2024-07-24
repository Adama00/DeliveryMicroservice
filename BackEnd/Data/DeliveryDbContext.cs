using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class DeliveryDbContext: DbContext
    {
        public DbSet<Delivery> Deliveries { get; set; }
       

        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) 
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>()
                .Property(d => d.PickupLocation)
                .HasColumnType("geography (point)");

            modelBuilder.Entity<Delivery>()
                .Property(d => d.DeliveryLocation)
                .HasColumnType("geography (point)");
        }

    }
}

