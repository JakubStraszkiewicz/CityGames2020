using CityGames2019.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace CityGames2019.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            
            if (!context.Users.Any())
            {
                //Haslo: Admin123!

                User admin = new User()
                {
                    Id = "18cf8089-6c0f-4248-ae89-e105ffd9cd39",
                    UserName = "admin@wp.pl",
                    NormalizedUserName = "ADMIN@WP.PL",
                    Email = "admin@wp.pl",
                    PasswordHash = "AQAAAAEAACcQAAAAEIK9iv1GaeIHTMbqDpK7pj0kMzj99SRDC+voxKAOK4cVXCJFLFBJ8biv3vbVNXngwA==",
                    SecurityStamp = "CPHYEWHEWJ4SGNCLAWG4VRL2IW2N4CM4",
                    ConcurrencyStamp = "e8f26cad-ce28-4777-b8e2-159281b1e3a9",
                    Name = "Admin",
                    Surname = "Admin",
                };
                context.Users.Add(admin);

                //Haslo: User123!
                User user1 = new User()
                {
                    Id = "d1ed65bf-1c16-4a81-b35c-018737d0890b",
                    UserName = "user1@wp.pl",
                    NormalizedUserName = "USER1@WP.PL",
                    Email = "user1@wp.pl",
                    PasswordHash = "AQAAAAEAACcQAAAAEN01vqFsDPJz2BA96uFer4jUWaQBGxWLk/zz0rTi2dXmbhJ9U4Wz2Huah5EvrIO7kQ==",
                    SecurityStamp = "B6ZQG7A6LU45SFYLWVRINYSMSJ7IXWUI",
                    ConcurrencyStamp = "dd6bb1fb-09b6-4ffd-ad6e-16a02dd6fd4d",
                    Name = "UserName",
                    Surname = "UserSurname"
                };

                context.Users.Add(user1);

                context.Games.Add(new Game()
                {
                    Name="Wycieczka Gdańsk",
                    Photo= File.ReadAllBytes(".\\GdanskPhoto.png"),
                    Creator = admin,
                    CreatorId = admin.Id
                });

                context.Games.Add(new Game()
                {
                    Name = "Wycieczka Bydgoszcz",
                    Creator = admin,
                    CreatorId = admin.Id
                });

                context.Games.Add(new Game()
                {
                    Name = "Wycieczka Jarocin",
                    Creator = admin,
                    CreatorId = admin.Id
                });

                context.Games.Add(new Game()
                {
                    Name = "Wycieczka Sopot",
                    Creator = admin,
                    CreatorId = admin.Id
                });

                context.Games.Add(new Game()
                {
                    Name = "Wycieczka Gdynia",
                    Creator = admin,
                    CreatorId = admin.Id
                });
                //context.Tenants.Add(new Tenant() { Name = "Sample", Host = "sample", Style = "blue.css" });
                context.SaveChanges();
            }
        }
    }
}
