using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("Groups")]
    public class Group
    {
        #region Constructors

        public Group()
        {
            this.GroupGames = new List<GroupGame>();
            this.UserGroups = new List<UserGroup>();
        }

        #endregion

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public List<GroupGame> GroupGames { get; set; }

        public List<UserGroup> UserGroups { get; set; }

        #endregion
    }
}
