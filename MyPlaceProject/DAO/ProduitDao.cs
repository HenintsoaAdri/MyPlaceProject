using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyPlaceProject.DAO
{
    public class ProduitDAO
    {
        public List<Produit> findAll(bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM PRODUIT";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    List<Produit> liste = new List<Produit>();
                    if (!lazy)
                    {
                        while (reader.Read())
                        {
                            Produit p = UtilDB.getInstance().createProduit(reader);
                            p.Categorie = this.findCategorieById(p.Categorie.Id);
                            liste.Add(p);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            liste.Add(UtilDB.getInstance().createProduit(reader));
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
        


        public Produit findById(int id)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM PRODUIT WHERE ID = @id";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                sqlCommand.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    Produit item = new Produit();
                    if (reader.Read())
                    {
                        item = UtilDB.getInstance().createProduit(reader);
                    }
                    return item;
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

        public Categorie findCategorieById(int id)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM CATEGORIE WHERE ID = @id";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                sqlCommand.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    Categorie item = new Categorie();
                    if (reader.Read())
                    {
                        item = UtilDB.getInstance().createCategorie(reader);
                    }
                    return item;
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