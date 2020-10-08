using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("UserGroups")]
    public class UserGroup
    {
        #region Contructors

        public UserGroup(Group group, User user)
        {
            this.User = user;
            this.UserId = user.Id;
            this.Group = group;
            this.GroupId = group.Id;
        }

        public UserGroup() { }

        #endregion

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        #endregion

        #region Foreign

        public Group Group { get; set; }

        [ForeignKey("Group")] public virtual long GroupId { get; set; }

        public User User { get; set; }

        [ForeignKey("User")] public virtual string UserId { get; set; }

        #endregion
    }
}
