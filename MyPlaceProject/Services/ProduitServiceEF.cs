using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Services
{
    public class ProduitServiceEF
    {   
        private ProduitServiceEF() { }
        private static ProduitServiceEF instance = new ProduitServiceEF();
        public static ProduitServiceEF getInstance()
        {
            return instance;
        }

        public List<Produit> GetAll()
        {
            using (var context = new MyPlaceContext())
            {
                return context.produit.Include(p => p.Categorie).ToList();
            }
        }
        public Produit Get(int? id)
        {
            using(var context = new MyPlaceContext())
            {
                return context.produit.Find(id);
            }
        }
        public void Save(Produit produit)
        {
            using (var context = new MyPlaceContext())
            {
                using(var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.produit.Add(produit);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(Produit produit)
        {
            using (var context = new MyPlaceContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Entry(produit).State = EntityState.Modified;
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(Produit produit)
        {
            using (var context = new MyPlaceContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.produit.Remove(produit);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }

        }
    }
}