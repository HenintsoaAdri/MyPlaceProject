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
        public PartialViewResult Produit(int? id)
        {
            ViewBag.Selected = id;
            return PartialView(ProduitService.getInstance().findAll());
        }
    }
}