using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Reflection;
using users_directory.DbModels;
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
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<PhoneNumbersType> PhoneNumbersType { get; set; }
        public DbSet<GendersType> GendersType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRelationship>()
                .HasOne(p => p.User)
                .WithMany(p => p.Relationships)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PersonRelationship>()
               .HasOne(p => p.RelatedType)
               .WithMany()
               .HasForeignKey(p => p.RelatedTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhoneNumber>()
                .HasOne(p => p.User)
                .WithMany(p => p.PhoneNumbers)
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhoneNumber>()
               .HasOne(p => p.NumberType)
               .WithMany()
               .HasForeignKey(p => p.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
               .HasOne(p => p.City)
               .WithMany()
               .HasForeignKey(p => p.CityId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
               .HasOne(p => p.Gender)
               .WithMany()
               .HasForeignKey(p => p.GenderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
               .HasIndex(u => u.PersonalNumber)
               .IsUnique();

            modelBuilder.Entity<GendersType>().HasData(
     new GendersType { Id = 1, Gender = "კაცი" },
     new GendersType { Id = 2, Gender = "ქალი" },
     new GendersType { Id = 3, Gender = "სხვა" }
 );



            modelBuilder.Entity<PhoneNumbersType>().HasData(
        new PhoneNumbersType { Id = 1, Type = "მობილური" },
        new PhoneNumbersType { Id = 2, Type = "სახლის" },
        new PhoneNumbersType { Id = 3, Type = "ოფისის" }
    );
            modelBuilder.Entity<RelationshipType>().HasData(
                new RelationshipType { Id = 1, Type = "ნათესავი" },
                new RelationshipType { Id = 2, Type = "კოლეგა" },
                new RelationshipType { Id = 3, Type = "ნაცნობი" },
                new RelationshipType { Id = 4, Type = "სხვა" }
            );
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, CityName = "თბილისი" },
                new City { Id = 2, CityName= "რუსთავი" },
                new City { Id = 3, CityName = "ქუთაისი" },
                new City { Id = 4, CityName = "თელავი" },
                new City { Id = 5, CityName = "ბათუმი" },
                new City { Id = 6, CityName = "მარნეული" },
                new City { Id = 7, CityName = "ზუგდიდი" },
                new City { Id = 8, CityName = "ოზურგეთი" },
                new City { Id = 9, CityName = "ქობულეთი" },
                new City { Id = 10, CityName = "ყვარელი" }
            );
        }
    }
}
