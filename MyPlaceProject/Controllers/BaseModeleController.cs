using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using MyPlaceProject.Models;
using Newtonsoft.Json;

namespace MyPlaceProject.Controllers
{
    [Authorize(Roles ="Administrateur")]
    public class BaseModeleController : Controller
    {
        private MyPlaceContext db = new MyPlaceContext();

        //// GET: api/BaseModele
        //public IQueryable<Categorie> Getcategorie()
        //{
        //    return db.categorie;
        //}

        //// GET: api/BaseModele/5
        //[ResponseType(typeof(BaseModele))]
        //public IHttpActionResult GetCategorie(int id)
        //{
        //    Categorie categorie = db.categorie.Find(id);
        //    if (categorie == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(categorie);
        //}

        //// PUT: api/BaseModele/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutCategorie(int id, Categorie categorie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != categorie.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(categorie).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategorieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/BaseModele
        //[ResponseType(typeof(Categorie))]
        //public IHttpActionResult PostCategorie(Categorie categorie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.categorie.Add(categorie);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = categorie.Id }, categorie);
        //}
        [Route("Create/{entite:alpha}")]
        [ValidateAntiForgeryToken]
        public JsonResult Post(String entite)
        {
            dynamic obj = toObject(entite);
            UpdateModel(obj);
            db.Set(GetType(entite)).Add(obj);
            db.SaveChanges();
            return Json(obj);
        }

        //// DELETE: api/BaseModele/5
        //[ResponseType(typeof(Categorie))]
        //public IHttpActionResult DeleteCategorie(int id)
        //{
        //    Categorie categorie = db.categorie.Find(id);
        //    if (categorie == null)
        //    {
        //        return NotFound();
        //    }

        //    db.categorie.Remove(categorie);
        //    db.SaveChanges();

        //    return Ok(categorie);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool CategorieExists(int id)
        //{
        //    return db.categorie.Count(e => e.Id == id) > 0;
        //}
        public Type GetType(String entite)
        {
            return Type.GetType("MyPlaceProject.Models." + entite, true, true);
        }
        public BaseModele toObject(String entite)
        {
            return (BaseModele)Activator.CreateInstance(GetType(entite));
        }
    }
}