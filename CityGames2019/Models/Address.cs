using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityGames2019.Models
{
    [Table("Addresses")]
    public class Address
    {
        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        //as string cause number like 3b etc.
        public string StreetNumber { get; set; }

        #endregion
    }
}
