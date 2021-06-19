using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var games = new List<Product>()
            {
                new Product()
                {
                    AgeRating = AgeRatings.PEGI16,
                    Category = Categories.PS5,
                    DateOfProduction = new DateTime(2021, 1, 1),
                    Genre = Genres.Action,
                    Name = "Resident Evil Village",
                    Price = 1748,
                    Rating = 8.6f,
                    Id = 1

                },
                new Product()
                {
                    AgeRating = AgeRatings.PEGI16,
                    Category = Categories.PC,
                    DateOfProduction = new DateTime(2020, 7, 7),
                    Genre = Genres.Adventure,
                    Name = "Death stranding",
                    Price = 1389,
                    Rating = 9.2f,
                    Id = 2

                },
                new Product()
                {
                    AgeRating = AgeRatings.PEGI7,
                    Category = Categories.Android,
                    DateOfProduction = new DateTime(2011, 12, 28),
                    Genre = Genres.Indie,
                    Name = "Terraria",
                    Price = 199,
                    Rating = 8.8f,
                    Id = 3

                },
                new Product()
                {
                    AgeRating = AgeRatings.PEGI16,
                    Category = Categories.PC,
                    DateOfProduction = new DateTime(2018, 12, 3),
                    Genre = Genres.Shooter,
                    Name = "Battlefield 5",
                    Price = 789,
                    Rating = 7.9f,
                    Id = 4

                },
                new Product()
                {
                    AgeRating = AgeRatings.PEGI12,
                    Category = Categories.PC,
                    DateOfProduction = new DateTime(2014, 4, 1),
                    Genre = Genres.Strategy,
                    Name = "Hearts of Iron",
                    Price = 259,
                    Rating = 8.5f,
                    Id = 5

                },
                new Product()
                {
                    AgeRating = AgeRatings.PEGI16,
                    Category = Categories.PC,
                    DateOfProduction = new DateTime(2018, 8, 21),
                    Genre = Genres.RPG,
                    Name = "Fallout 76",
                    Price = 399,
                    Rating = 5.6f,
                    Id = 6

                },
                new Product()
                {
                    AgeRating = AgeRatings.PEGI16,
                    Category = Categories.PS5,
                    DateOfProduction = new DateTime(2021, 4, 10),
                    Genre = Genres.Action,
                    Name = "Days Gone",
                    Price = 2599,
                    Rating = 8.2f,
                    Id = 7

                },
                new Product()
                {
                    AgeRating = AgeRatings.PEGI7,
                    Category = Categories.XBOX360,
                    DateOfProduction = new DateTime(2020, 5, 22),
                    Genre = Genres.Action,
                    Name = "FIFA21",
                    Price = 2100,
                    Rating = 7.5f,
                    Id = 8

                },
            };
            builder.Entity<Product>().HasData(games);
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
