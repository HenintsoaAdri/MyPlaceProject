using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Models
{
    public class Commande : BaseModele
    {
        public DateTime Date { get; set; }
        public List<DetailCommande> DetailCommande { get; set; }
    }
}