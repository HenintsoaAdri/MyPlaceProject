using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Models
{
    public class Commande : BaseModele
    {
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public virtual ICollection<DetailCommande> DetailCommande { get; set; }

        public string ApplicationUserID { get; set; }

        public float GetTotal()
        {
            float total = 0;
            foreach(DetailCommande item in DetailCommande)
            {
                total += item.GetTotal();
            }
            return total;
        }
        public Commande() { }
        public Commande(int id) : base(id) { }

    }
}