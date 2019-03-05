using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        [Display(Name = "Catégorie")]
        public int? CategorieId { get; set; }

        public virtual Categorie Categorie { get; set; }
        public DateTime DatePlat { get; set; }
        
        [Display(Name="Quantité en Stock")]
        [Range(0, 20)]
        public int QuantiteStock { get; set; }

        public Produit()
        {
            DatePlat = DateTime.Now;
        }
        
    }
}