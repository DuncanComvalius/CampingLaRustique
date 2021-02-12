using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingLaRustique.Models
{
    public class Klanten
    {
        [Key]
        public int KlantID { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Woonplaats { get; set; }
        public string Postcode { get; set; }
        public string Telefoon { get; set; }

    }
}