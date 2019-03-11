using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyPlaceProject.DAO
{
    public class UtilDB
    {
        private String stringConnection = "Data Source=.\\SQLEXPRESS;Initial Catalog=MyPlaceDB;Integrated Security=True";

        private UtilDB() { }
        private static UtilDB instance = new UtilDB();
        public static UtilDB getInstance()
        {
            return instance;
        } 

        public SqlConnection getConnection()
        {
            return new SqlConnection(stringConnection); 
        }


        public Commande createCommande(SqlDataReader reader)
        {
            Commande item = new Commande();
            item.Id = Convert.ToInt32(reader["ID"]);
            item.Date = Convert.ToDateTime(reader["DATE"]);
            item.ApplicationUserID = Convert.ToString(reader["APPLICATIONUSERID"]);
            return item;
        }
        public DetailCommande createDetailCommande(SqlDataReader reader)
        {
            DetailCommande item = new DetailCommande();
            item.Commande = new Commande(Convert.ToInt32(reader["COMMANDEID"]));
            item.Produit = new Produit(Convert.ToInt32(reader["PRODUITID"]));
            item.Quantite = Convert.ToInt32(reader["QUANTITE"]);
            item.PrixUnitaire = Convert.ToSingle(reader["PRIXUNITAIRE"]);
            return item;
        }
        public Produit createProduit(SqlDataReader reader)
        {
            Produit item = new Produit();
            item.Id = Convert.ToInt32(reader["ID"]);
            item.Categorie = new Categorie(Convert.ToInt32(reader["CATEGORIEID"]));
            item.Nom = Convert.ToString(reader["NOM"]);
            item.Photo = Convert.ToString(reader["PHOTO"]);
            item.Description = Convert.ToString(reader["DESCRIPTION"]);
            item.DatePlat = Convert.ToDateTime(reader["DATEPLAT"]);
            item.Prix = Convert.ToSingle(reader["PRIX"]);
            item.QuantiteStock = Convert.ToInt32(reader["QUANTITESTOCK"]);
            return item;
        }
        public Categorie createCategorie(SqlDataReader reader)
        {
            Categorie item = new Categorie();
            item.Id = Convert.ToInt32(reader["ID"]);
            item.Nom = Convert.ToString(reader["NOM"]);
            return item;
        }
    }
}