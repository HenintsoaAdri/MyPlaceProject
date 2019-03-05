using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Models
{
    public class MyPlaceContext : DbContext
    {
        public MyPlaceContext() : base("MyPlaceDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyPlaceContext, MyPlaceProject.Migrations.Configuration>());
        }
        public DbSet<Produit> produit { get; set; }
        public DbSet<Categorie> categorie { get; set; }
        public DbSet<Commande> commande { get; set; }
        public DbSet<DetailCommande> detailCommande { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}