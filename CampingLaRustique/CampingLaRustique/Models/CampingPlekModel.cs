using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace CampingLaRustique.Models
{
    public class CampingPlekModel
    {
        public List<Camping> campings { get; set; }
        public SelectList PlekID { get; set; }
        public int plek { get; set; }
        public string SearchString { get; set; }
    }
}
