using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingLaRustique.Models
{
    public class Camping
    {
        [Key]
        public int PlekID { get; set; }

        public string Type { get; set; }

        public string Oppervlakte { get; set; }

        public bool GratisDouche { get; set; }

        public bool Huisdieren { get; set; }

        public bool Elektriciteit { get; set; }

        public string Ligging { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Prijs { get; set; }
    }
}
