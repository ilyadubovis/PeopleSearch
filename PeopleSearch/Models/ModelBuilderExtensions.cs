using Microsoft.EntityFrameworkCore;
using System;

namespace PeopleSearch.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {     
            modelBuilder.Entity<Person>(p =>
            {
                p.HasData(
                    new
                    {
                        Id = 1,
                        FirstName = "Quentin",
                        LastName = "Tarantino",
                        BirthDate = new DateTime(1963, 3, 27),
                        StreetAddress = "234 Main Steet",
                        City = "Los Angeles",
                        State = "CA",
                        ZipCode = "90005",
                        PhotoFile = "assets/images/photos/Quentin.png"
                    }); 
                p.OwnsMany<Interest>(e => e.Interests).HasData(
                    new
                    {
                        Id = 1,
                        Name = "Directing",
                        PersonID = 1
                    },
                    new
                    {
                        Id = 2,
                        Name = "Writing",
                        PersonID = 1
                    });
            });
            
            modelBuilder.Entity<Person>(p =>
            {
                p.HasData(
                    new
                    {
                        Id = 2,
                        FirstName = "Leonardo",
                        LastName = "DiCaprio",
                        BirthDate = new DateTime(1974, 11, 1),
                        StreetAddress = "24 Church Steet",
                        City = "Beverly Hills",
                        State = "CA",
                        ZipCode = "90012",
                        PhotoFile = "assets/images/photos/Leonardo.png"
                    });
                p.OwnsMany<Interest>(e => e.Interests).HasData(
                    new
                    {
                        Id = 3,
                        Name = "Sailing",
                        PersonID = 2
                    },
                    new
                    {
                        Id = 4,
                        Name = "Playing",
                        PersonID = 2
                    },
                    new
                    {
                        Id = 5,
                        Name = "Hunting",
                        PersonID = 2
                    });
            });

            modelBuilder.Entity<Person>(p =>
            {
                p.HasData(
                    new
                    {
                        Id = 3,
                        FirstName = "Margot",
                        LastName = "Robbie",
                        BirthDate = new DateTime(1990, 7, 2),
                        StreetAddress = "2346 3rd Ave, Apt. 78A",
                        City = "New York",
                        State = "NY",
                        ZipCode = "10025",
                        PhotoFile = "assets/images/photos/Margot.png"
                    });
                p.OwnsMany<Interest>(e => e.Interests).HasData(
                    new
                    {
                        Id = 6,
                        Name = "Fashion",
                        PersonID = 3
                    });
            });

            modelBuilder.Entity<Person>(p =>
            {
                p.HasData(
                    new
                    {
                        Id = 4,
                        FirstName = "Brad",
                        LastName = "Pitt",
                        BirthDate = new DateTime(1964, 12, 18),
                        StreetAddress = "11 S. Sunny Rd.",
                        City = "Long Beach",
                        State = "CA",
                        ZipCode = "90032",
                        PhotoFile = "assets/images/photos/Brad.png"
                    });
                p.OwnsMany<Interest>(e => e.Interests).HasData(
                    new
                    {
                        Id = 7,
                        Name = "Biking",
                        PersonID = 4
                    },
                    new
                    {
                        Id = 8,
                        Name = "Martial Arts",
                        PersonID = 4
                    });           
            });

            modelBuilder.Entity<Person>(p =>
            {
                p.HasData(
                    new
                    {
                        Id = 5,
                        FirstName = "Al",
                        LastName = "Pachino",
                        BirthDate = new DateTime(1940, 4, 12),
                        StreetAddress = "45 Broadway St, Ste 300",
                        City = "New York",
                        State = "NY",
                        ZipCode = "10046",
                        PhotoFile = "assets/images/photos/Al.png"
                    });
                p.OwnsMany<Interest>(e => e.Interests).HasData(
                    new
                    {
                        Id = 9,
                        Name = "Gambling",
                        PersonID = 5
                    });
            });
       }
    }
}
