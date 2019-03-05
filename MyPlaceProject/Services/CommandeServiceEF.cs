using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Services
{
    public class CommandeServiceEF
    {   
        public void Update(Commande commande)
        {
            using (var context = new MyPlaceContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        HashSet<int> selected = new HashSet<int>();
                        if (commande.DetailCommande == null)
                        {
                            commande.DetailCommande = new HashSet<DetailCommande>();
                        }
                        else
                        {
                            foreach (DetailCommande item in commande.DetailCommande)
                            {
                                selected.Add(item.ProduitId);
                                if(item.Quantite <= 0)
                                {
                                    context.Entry(item).State = EntityState.Deleted;
                                }
                                else if (item.CommandeId != commande.Id)
                                {
                                    item.CommandeId = commande.Id;
                                    context.Entry(item).State = EntityState.Added;
                                }
                                else
                                {
                                    context.Entry(item).State = EntityState.Modified;
                                }
                            }
                        }
                        var old = context.detailCommande.Where(dc => dc.CommandeId == commande.Id && !selected.Contains(dc.ProduitId)).ToList();
                        old.ForEach(x => context.Entry(x).State = EntityState.Deleted);
                        context.Entry(commande).State = EntityState.Modified;
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        foreach (DetailCommande item in commande.DetailCommande)
                        {
                            context.Entry(item).Reference(dc => dc.Produit).Load();
                        }
                        throw;
                    }
                }
            }
        }
    }
}