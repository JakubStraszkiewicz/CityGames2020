using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    public enum LanguageEnum
    {
        PL = 0,
        EN = 1
    }

    [Table("Users")]
    public class User : IdentityUser
    {
        #region Constructors

        public User()
        {
            this.Games = new List<Game>();
            this.UserGroups = new List<UserGroup>();
            this.Presentations = new List<Presentation>();
        }

        public User(IdentityUser user)
        {
            this.Games = new List<Game>();
            this.UserGroups = new List<UserGroup>();
            this.Presentations = new List<Presentation>();

            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.EmailConfirmed = user.EmailConfirmed;
            this.PasswordHash = user.PasswordHash;
            this.SecurityStamp = user.SecurityStamp;
        }

        #endregion

        #region Fields

        public LanguageEnum NativeLanguage { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public byte[] Photo { get; set; }

        public List<Game> Games { get; set; }

        public List<UserGroup> UserGroups { get; set; }

        public List<Presentation> Presentations { get; set; }

        #endregion
    }
}
