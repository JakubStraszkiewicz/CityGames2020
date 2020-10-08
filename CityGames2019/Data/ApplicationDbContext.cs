using System;
using System.Collections.Generic;
using System.Text;
using CityGames2019.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CityGames2019.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<ControlPoint> ControlPoints { get; set; }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<GroupGame> GroupGames { get; set; }

        public virtual DbSet<User> ApplicationUsers { get; set; }

        public virtual DbSet<UserGroup> UserGroups { get; set; }

        public virtual DbSet<Presentation> Presentations { get; set; }

        public virtual DbSet<Photo> Photos { get; set; }



    }
}
