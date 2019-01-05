﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyPlaceApp.Models
{
    public class MyPlaceContext : DbContext
    {
        public MyPlaceContext() : base("MyPlaceDB")
        {

        }
        public DbSet<Produit> produits { get; set; }
        public DbSet<Categorie> categorie { get; set; }
        public DbSet<Commande> commandes { get; set; }
        public DbSet<DetailCommande> detailCommandes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}