using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceApp.Models
{
    public class Produit : BaseModele 
    {
        public string nom { get; set; }
        public float prix { get; set; }
        public string description { get; set; }
        public string photo { get; set; }
        public Categorie categorie { get; set; }
        
    }
}