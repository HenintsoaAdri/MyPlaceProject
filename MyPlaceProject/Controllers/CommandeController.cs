using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPlaceProject.Models;
using MyPlaceProject.Services;

namespace MyPlaceProject.Controllers
{
    public class CommandeController : Controller
    {
        private MyPlaceContext db = new MyPlaceContext();

        // GET: Commande
        public ActionResult Index()
        {
            return View(db.commande.ToList());
        }

        // GET: Commande/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Commande commande = db.commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // GET: Commande/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commande/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.commande.Add(commande);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commande);
        }

        // GET: Commande/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // POST: Commande/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Commande commande)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CommandeServiceEF service = new CommandeServiceEF();
                    service.Update(commande);
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(commande);
        }
        
        // GET: Commande/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // POST: Commande/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commande commande = db.commande.Find(id);
            db.commande.Remove(commande);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
