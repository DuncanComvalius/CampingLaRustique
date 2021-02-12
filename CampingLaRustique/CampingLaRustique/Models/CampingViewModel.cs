using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CampingLaRustique.Models
{
    public class CampingViewModel
    {
        public List<Camping> campings { get; set; }
        public SelectList Types { get; set; }
        public string Typess { get; set; }
        public string SearchString { get; set; }
    }
}
