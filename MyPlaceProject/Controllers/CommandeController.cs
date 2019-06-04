using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPlaceProject.Models;
using MyPlaceProject.Services;

namespace MyPlaceProject.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class CommandeController : Controller
    {
        private CommandeServiceEF service = CommandeServiceEF.getInstance();

        // GET: Commande
        public ActionResult Index(int page = 1, int maxResult = 10)
        {
            return View(service.GetAll(page, maxResult));
        }

        // GET: Commande/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Commande commande = service.Get(id);
            return View(commande);
        }
        public ActionResult Facture(int? id)
        {
            using (var writer = new StringWriter())
            {
                Commande commande = service.Get(id);
                if (commande == null)
                {
                    return RedirectToAction("Index");
                }
                ViewData.Model = commande;
                var partial  = ViewEngines.Engines.FindView(ControllerContext, "Facture", "_Layout");
                ViewContext viewContext = new ViewContext(ControllerContext, partial.View, ViewData, TempData, writer);
                partial.View.Render(viewContext, writer);
                
                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(writer.ToString(), Request.Url.AbsoluteUri);
                byte[] pdf = doc.Save();
                doc.Close();
                FileResult file = new FileContentResult(pdf, "application/pdf");
                file.FileDownloadName = "Facture" + DateTime.Now.ToString() + ".pdf";
                return file;
            }
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
                try
                {
                    service.Save(commande);
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
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
            Commande commande = service.Get(id);
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
            Commande commande = service.Get(id);
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
            try
            {
                service.Delete(id);
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(service.Get(id));
        }
    }
}
