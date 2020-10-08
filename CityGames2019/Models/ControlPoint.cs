using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("ControlPoints")]
    public class ControlPoint
    {
        #region Constructors

        public ControlPoint(Address address, Game game)
        {
            Address = address;
            AddressId = address.Id;
            Game = game;
            GameId = game.Id;
        }

        public ControlPoint() { }

        #endregion

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [StringLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        #endregion

        #region Foreign

        public Address Address { get; set; }

        [ForeignKey("Address")] public virtual long AddressId { get; set; }

        public Game Game { get; set; }

        [ForeignKey("Game")] public virtual long GameId { get; set; }

        #endregion
    }
}
