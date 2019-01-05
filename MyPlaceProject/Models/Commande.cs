using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceApp.Models
{
    public class Commande : BaseModele
    {
        public DateTime date { get; set; }
        public List<DetailCommande> detailCommande { get; set; }
    }
}