using Microsoft.EntityFrameworkCore;
using System;
using users_directory.Models;

namespace users_directory.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> People { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PersonRelationship> PersonRelationships { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRelationship>()
                .HasOne(p => p.User)
                .WithMany(p => p.Relationships)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonRelationship>()
                .HasOne(p => p.RelatedPerson)
                .WithMany()
                .HasForeignKey(p => p.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhoneNumber>()
                .HasOne(p => p.User)
                .WithMany(p => p.PhoneNumbers)
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
               .HasOne(p => p.City)
               .WithMany()
               .HasForeignKey(p => p.CityId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
