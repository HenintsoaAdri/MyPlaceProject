using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MyPlaceProject.Services;

namespace MyPlaceProject.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class SelectController : Controller
    {
        private MyPlaceContext db = new MyPlaceContext();
        private UtilisateurServiceEF user = UtilisateurServiceEF.getInstance();

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
            return PartialView(db.produit.Include(p => p.Categorie).Where(p => p.QuantiteStock > 0).ToList());
        }
        public PartialViewResult Role(string role)
        {
            ViewBag.Selected = role;
            return PartialView(user.GetAllRole());
        }
        public PartialViewResult Utilisateur(string id)
        {
            ViewBag.Selected = id;
            return PartialView(user.GetAll());
        }
    }
}
