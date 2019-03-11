using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyPlaceProject.DAO
{
    public class DetailCommandeDAO
    {
        
        public List<DetailCommande> findAllByCommande(Commande commande, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM DETAILCOMMANDE WHERE COMMANDEID = @id";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                sqlCommand.Parameters.AddWithValue("@id", commande.Id);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    List<DetailCommande> liste = new List<DetailCommande>();
                    if (!lazy)
                    {
                        while (reader.Read())
                        {
                            DetailCommande p = UtilDB.getInstance().createDetailCommande(reader);
                            p.Commande = commande;
                            p.Produit = new ProduitDAO().findById(p.Produit.Id, false);
                            liste.Add(p);
                        }
                    } else
                    {
                        while (reader.Read())
                        {
                            liste.Add(UtilDB.getInstance().createDetailCommande(reader));
                        }
                    }
                    return liste;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    reader.Close();
                    if (conn != null) conn.Close();
                }
            }
        }
        public List<DetailCommande> findAllByCommande(int id, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM DETAILCOMMANDE WHERE COMMANDEID = @id";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                sqlCommand.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    List<DetailCommande> liste = new List<DetailCommande>();
                    if (!lazy)
                    {
                        while (reader.Read())
                        {
                            DetailCommande p = UtilDB.getInstance().createDetailCommande(reader);
                            p.Commande = new CommandeDAO().findById(id, true);
                            p.Produit = new ProduitDAO().findById(p.Produit.Id, lazy);
                            liste.Add(p);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            liste.Add(UtilDB.getInstance().createDetailCommande(reader));
                        }
                    }
                    return liste;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    reader.Close();
                    if (conn != null) conn.Close();
                }
            }
        }

    }
}