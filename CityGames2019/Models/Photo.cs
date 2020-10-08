using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("Photos")]
    public class Photo
    {
        #region Constructors

        public Photo(Presentation presentation)
        {
            this.Presentation = presentation;
            this.PresentationId = presentation.Id;
        }

        public Photo() { }

        #endregion

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public byte[] Data { get; set; }

        [StringLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        #endregion

        #region Foreign

        public Presentation Presentation { get; set; }

        [ForeignKey("Presentation")] public virtual long PresentationId { get; set; }

        #endregion
    }
}
