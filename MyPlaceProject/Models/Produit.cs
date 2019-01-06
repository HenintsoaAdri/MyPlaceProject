using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Models
{
    public class Produit : BaseModele 
    {
        public string Nom { get; set; }
        public float Prix { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public Categorie Categorie { get; set; }
        
    }
}