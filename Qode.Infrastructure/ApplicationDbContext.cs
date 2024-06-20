using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Qode.Infrastructure.Models;
using Qode.Quantum;

namespace Qode.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<UserCircuit> UserCircuits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCircuit>()
                .OwnsMany(e => e.CircuitOperations, o =>
                {
                    o.ToJson();
                })
                .HasMany(e => e.UserFavorites)
                .WithMany()
                .UsingEntity("UserFavoriteCircuits");

            base.OnModelCreating(modelBuilder);
        }
    }
}
