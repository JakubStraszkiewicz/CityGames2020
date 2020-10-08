using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("Presentations")]
    public class Presentation
    {
        #region Constructors

        public Presentation(User creator, Game game)
        {
            this.Creator = creator;
            this.CreatorId = creator.Id;
            this.Game = game;
            this.GameId = game.Id;
            this.Photos = new List<Photo>();
        }

        public Presentation()
        {
            this.Photos = new List<Photo>();
        }

        #endregion

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Photo> Photos { get; set; }

        #endregion

        #region Foreign

        public Game Game { get; set; }

        [ForeignKey("Game")] public virtual long GameId { get; set; }

        public User Creator { get; set; }

        [ForeignKey("Creator")] public virtual string CreatorId { get; set; }

        #endregion
    }
}
