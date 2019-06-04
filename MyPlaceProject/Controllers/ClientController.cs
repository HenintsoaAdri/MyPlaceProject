using Microsoft.AspNet.Identity;
using MyPlaceProject.Models;
using MyPlaceProject.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPlaceProject.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        CommandeService service = CommandeService.getInstance();
                
        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Client/Commande
        [Authorize(Roles = "Client")]
        public ActionResult Commande()
        {
            return View(service.findAllByUser(User.Identity.GetUserId()));
        }

        // POST: Client/Create

        [Authorize(Roles = "Client")]
        [HttpPost]
        public ActionResult Create(Commande commande)
        {
            try
            {
                CommandeService.getInstance().create(commande, User.Identity.GetUserId());
                return RedirectToAction("Commande");
            }
            catch
            {
                return View();
            }
        }

        // GET: Commande/Details/5
        [Authorize(Roles = "Client")]
        public ActionResult DetailsCommande(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Commande");
            }
            Commande commande = service.find(id, User.Identity.GetUserId());
            if (commande == null)
            {
                return RedirectToAction("Commande");
            }
            return View(commande);
        }
        public ActionResult Facture(int id)
        {
            using (var writer = new StringWriter())
            {
                Commande commande = service.find(id, User.Identity.GetUserId());
                if (commande == null)
                {
                    return RedirectToAction("Commande");
                }
                ViewData.Model = commande;
                var partial = ViewEngines.Engines.FindView(ControllerContext, "Facture", "_Layout");
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
    }
}
