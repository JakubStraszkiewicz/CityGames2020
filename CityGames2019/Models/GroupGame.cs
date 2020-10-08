using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("GroupGames")]
    public class GroupGame
    {
        #region Constructors

        public GroupGame(Group group, Game game)
        {
            this.Game = game;
            this.GameId = game.Id;
            this.Group = group;
            this.GroupId = group.Id;
        }

        public GroupGame() { }

        #endregion

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        #endregion

        #region Foreign

        public Group Group { get; set; }

        [ForeignKey("Group")] public virtual long GroupId { get; set; }

        public Game Game { get; set; }

        [ForeignKey("Game")] public virtual long GameId { get; set; }

        #endregion
    }
}
