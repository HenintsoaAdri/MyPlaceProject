using MyPlaceProject.Models;
using MyPlaceProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPlaceProject.Controllers
{
    public class ClientSelectController : Controller
    {
        // GET: SelectProduit
        [AllowAnonymous]
        public PartialViewResult ProduitResult(int categorie = 0, string query = "", int page = 1, int maxResult = 9)
        {
            ViewBag.Query = query;
            ViewBag.Categorie = categorie;
            ViewBag.ListCategorie = ProduitService.getInstance().findAllCategorie();
            return PartialView(ProduitService.getInstance().findLike(categorie, query, page, maxResult));
        }
        [AllowAnonymous]
        public PartialViewResult Produit()
        {
            return PartialView(new List<string>() { "3", "18" });
        }
    }
}