using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CampingLaRustique.Models
{
    public class Reservering
    {
        [Key]
        public int Reserveringsnummer { get; set; }

        public int KlantID { get; set; }

        public int PlekID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }



        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Prijs { get; set; }

        public bool Betaald { get; set; }

    }
}
