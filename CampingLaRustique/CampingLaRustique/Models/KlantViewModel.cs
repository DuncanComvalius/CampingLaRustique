using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CampingLaRustique.Models
{
    public class KlantViewModel
    {
        public List<Klanten> klanten { get; set; }

        public int IsSelected { get; set; }
    }
}
