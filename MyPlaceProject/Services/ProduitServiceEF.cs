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
        public List<Categorie> GetCategories()
        {
            using(var context = new MyPlaceContext())
            {
                return context.categorie.ToList();
            }
        }

        public BaseModelPagination<Produit> GetAll(int page = 1, int maxResult = 10)
        {
            using (var context = new MyPlaceContext())
            {
                BaseModelPagination<Produit> pagination = new BaseModelPagination<Produit>(page, maxResult);
                pagination.totalResult = context.produit.Count();
                pagination.liste = context.produit.Include(i=>i.Categorie).OrderBy(i=>i.Id).Skip(pagination.offset()).Take(maxResult).ToList();
                return pagination;
            }
        }

        public List<Produit> GetAll()
        {
            using (var context = new MyPlaceContext())
            {
                return context.produit.Include(p => p.Categorie).ToList();
            }
        }

        public BaseModelPagination<Produit> FindAllDisponible(int categorie, string query, int page = 1, int maxResult = 10)
        {
            using (var context = new MyPlaceContext())
            {
                BaseModelPagination<Produit> pagination = new BaseModelPagination<Produit>(page, maxResult);
                IQueryable<Produit> queryable = context.produit.Include(p => p.Categorie).Where(p => p.QuantiteStock > 0);
                if (!string.IsNullOrWhiteSpace(query))
                {
                    queryable = queryable.Where(p => p.Nom.ToLower().Contains(query.ToLower()));
                }
                if(categorie > 0)
                {
                    queryable = queryable.Where(p => p.CategorieId == categorie);
                }
                pagination.liste = queryable.OrderBy(i => i.Nom)
                    .Skip(pagination.offset())
                    .Take(maxResult)
                    .ToList();
                pagination.totalResult = queryable.Count();
                return pagination;
            }
        }
        public Produit Get(int? id)
        {
            using(var context = new MyPlaceContext())
            {
                return context.produit.Include(p => p.Categorie).Where(p => p.Id == id).FirstOrDefault();
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
        public void Delete(int id)
        {
            using (var context = new MyPlaceContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Produit produit = context.produit.Find(id);
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