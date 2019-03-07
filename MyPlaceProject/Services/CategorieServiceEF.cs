using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Services
{
    public class CategorieServiceEF
    {   
        private CategorieServiceEF() { }
        private static CategorieServiceEF instance = new CategorieServiceEF();
        public static CategorieServiceEF getInstance()
        {
            return instance;
        }

        public List<Categorie> GetAll()
        {
            using (var context = new MyPlaceContext())
            {
                return context.categorie.ToList();
            }
        }
        public Categorie Get(int? id)
        {
            using(var context = new MyPlaceContext())
            {
                return context.categorie.Find(id);
            }
        }
        public void Save(Categorie categorie)
        {
            using (var context = new MyPlaceContext())
            {
                using(var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.categorie.Add(categorie);
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
        public void Update(Categorie categorie)
        {
            using (var context = new MyPlaceContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Entry(categorie).State = EntityState.Modified;
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
        public void Delete(Categorie categorie)
        {
            using (var context = new MyPlaceContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.categorie.Remove(categorie);
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