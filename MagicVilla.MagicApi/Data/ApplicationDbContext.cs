using MagicVilla.MagicApi.Model;
using MagicVilla.MagicApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.MagicApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
    new Villa()
    {
        Name = "Villa 1",
        Id =1,
        Details = "This is a beautiful villa with stunning views.",
        Rate = 1000.00,
        Sqft = 2000,
        Occupancy = 4,
        CreatedDate = DateTime.Now,
        ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
        Amenities = "Swimming pool, jacuzzi, sauna, gym"
    },
    new Villa()
    {
        Name = "Villa 2",
        Id = 2,
        Details = "This is a cozy villa with a private garden.",
        Rate = 800.00,
        Sqft = 1500,
        Occupancy = 2,
        ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
        Amenities = "Garden, barbecue, fireplace",
        CreatedDate = DateTime.Now
    },
    new Villa()
    {
        Name = "Villa 3",
        Id = 3,
        Details = "This is a modern villa with a rooftop terrace.",
        Rate = 1200.00,
        Sqft = 2500,
        Occupancy = 6,
        ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
        CreatedDate = DateTime.Now,
        Amenities = "Rooftop terrace, city views, outdoor kitchen"
    },
    new Villa()
    {
        Name = "Villa 4",
        Id = 4,
        Details = "This is a luxurious villa with a private pool.",
        Rate = 1500.00,
        Sqft = 3000,
        Occupancy = 8,
        CreatedDate = DateTime.Now,
        ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
        Amenities = "Private pool, ocean views, butler service"
    },
    new Villa()
    {
        Name = "Villa 5",
        Id = 5,
        Details = "This is a charming villa with a rustic feel.",
        Rate = 700.00,
        Sqft = 1200,
        Occupancy = 3,
        CreatedDate = DateTime.Now,
        ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
        Amenities = "Fireplace, wood-burning oven, hammock"
    }    
);
            base.OnModelCreating(modelBuilder);
        }
       public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<LocalUser> LocalUsers{ get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    }
}
