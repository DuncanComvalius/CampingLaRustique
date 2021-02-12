using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CampingLaRustique.Models
{
    public class PlekViewModel
    {
        public List<Camping> campings { get; set; }

        public int IsSelected { get; set; }
    }
}
