using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyPlaceProject.DAO
{
    public class CategorieDAO
    {
        public List<Categorie> findAll()
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM CATEGORIE";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    List<Categorie> liste = new List<Categorie>();
                    while (reader.Read())
                    {
                        Categorie c = UtilDB.getInstance().createCategorie(reader);
                        liste.Add(c);
                    }
                    return liste;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    if (reader != null) reader.Close();
                    if (sqlCommand != null) sqlCommand.Dispose();
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
                    if (reader != null) reader.Close();
                    if (sqlCommand != null) sqlCommand.Dispose();
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
                    if (reader != null) reader.Close();
                    if (sqlCommand != null) sqlCommand.Dispose();
                    if (conn != null) conn.Close();
                }
            }
        }
    }
}