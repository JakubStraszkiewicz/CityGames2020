using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("Games")]
    public class Game
    {
        #region Constructors

        public Game(User creator)
        {
            this.GroupGames = new List<GroupGame>();
            this.ControlPoints = new List<ControlPoint>();
            this.Presentations = new List<Presentation>();
            this.Creator = creator;
            this.CreatorId = creator.Id;
        }

        public Game()
        {
            this.GroupGames = new List<GroupGame>();
            this.ControlPoints = new List<ControlPoint>();
            this.Presentations = new List<Presentation>();
        }

        #endregion

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        List<ControlPoint> ControlPoints { get; set; }

        List<GroupGame> GroupGames { get; set; }

        List<Presentation> Presentations { get; set; }

        #endregion

        #region Foreign

        public User Creator { get; set; }

        [ForeignKey("Creator")] public virtual string CreatorId { get; set; }

        #endregion
    }
}
