using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CampingLaRustique.Models
{
    public class ReserveringViewModel
    {
        public List<Klanten> klanten { get; set; }
        public List<Camping> campings { get; set; }
        public List<Reservering> reserverings { get; set; }

        public int IsSelected { get; set; }
    }
}
