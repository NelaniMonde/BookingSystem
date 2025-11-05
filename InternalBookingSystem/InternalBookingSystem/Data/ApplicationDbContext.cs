using InternalBookingSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternalBookingSystem.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Resource> Resources {  get; set; }

        public DbSet<Booking>Bookings { get; set; } 
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Configuring a one to many relationship between Resources and Bookings
            builder.Entity<Resource>()
                   .HasMany(res => res.BookingsList)
                   .WithOne(books => books.Resource)
                   .HasForeignKey(books => books.ResourcedId)
                   .IsRequired();



        }

    }
}
