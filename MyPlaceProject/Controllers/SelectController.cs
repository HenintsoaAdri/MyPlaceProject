using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace MyPlaceProject.Controllers
{
    public class SelectController : Controller
    {
        private MyPlaceContext db = new MyPlaceContext();

        // GET: SelectCategorie
        public PartialViewResult Categorie(int? id)
        {
            ViewBag.Selected = id;
            return PartialView(db.categorie.ToList());
        }
        // GET: SelectCategorie
        public PartialViewResult ModalCategorie()
        {
            return PartialView();
        }
        // GET: SelectProduit
        public PartialViewResult Produit(int? id)
        {
            ViewBag.Selected = id;
            return PartialView(db.produit.Include(p => p.Categorie).ToList());
        }
    }
}
