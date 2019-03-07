using MyPlaceProject.Models;
using MyPlaceProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPlaceProject.Controllers
{
    public class ClientController : Controller
    {
        CommandeService service = CommandeService.getInstance();

        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Commande()
        {
            return View(service.findAll());
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Commande commande)
        {
            try
            {
                CommandeService.getInstance().create(commande);
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Commande/Details/5
        public ActionResult DetailsCommande(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Commande");
            }
            Commande commande = service.find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
