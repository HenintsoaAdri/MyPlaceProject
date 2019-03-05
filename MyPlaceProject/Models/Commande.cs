using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Models
{
    public class Commande : BaseModele
    {
        public DateTime Date { get; set; }
        public virtual ICollection<DetailCommande> DetailCommande { get; set; }

        public float GetTotal()
        {
            float total = 0;
            foreach(DetailCommande item in DetailCommande)
            {
                total += item.GetTotal();
            }
            return total;
        }
    }
}