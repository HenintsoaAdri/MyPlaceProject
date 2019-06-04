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
        private UtilisateurServiceEF user = UtilisateurServiceEF.getInstance();
        private ProduitServiceEF produit = ProduitServiceEF.getInstance();

        // GET: SelectCategorie
        public PartialViewResult Categorie(int? id)
        {
            ViewBag.Selected = id;
            return PartialView(produit.GetCategories());
        }
        // GET: SelectCategorie
        public PartialViewResult ModalCategorie()
        {
            return PartialView();
        }
        // GET: SelectProduit
        public PartialViewResult Produit()
        {
            return PartialView(new List<string>() { "3", "18" });
        }
        // GET: SelectProduitResult
        public PartialViewResult ProduitResult(int categorie = 0, string query = "", int page = 1, int maxResult = 10)
        {
            ViewBag.Categorie = categorie;
            var liste = CategorieServiceEF.getInstance().GetAll();
            var tous = new Categorie();
            tous.Nom = "Tous";
            liste.Insert(0, tous);
            ViewBag.ListCategorie = liste;
            ViewBag.Query = query;
            return PartialView(produit.FindAllDisponible(categorie, query, page, maxResult));
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
