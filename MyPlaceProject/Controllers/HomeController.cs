using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPlaceProject.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrateur")]
        [Route("Administrateur")]
        public ActionResult Administrateur()
        {
            using(var context = new MyPlaceContext())
            {
                using(var usercontext = new ApplicationDbContext())
                {
                    ViewBag.Categorie = context.categorie.Count();
                    ViewBag.Commande = context.commande.Count();
                    ViewBag.Produit = context.produit.Count();
                    ViewBag.Utilisateur = usercontext.Users.Count();
                    return View();
                }
            }
        }
    }
}