using MyPlaceProject.Models;
using MyPlaceProject.Services;
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
                string request = "SELECT * FROM PRODUIT";
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
                    if (reader != null) reader.Close();
                    if (sqlCommand != null) sqlCommand.Dispose();
                    if (conn != null) conn.Close();
                }
            }
        }

        public Produit findById(int id, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                string request = "SELECT * FROM PRODUIT WHERE ID = @id";
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
                        if (!lazy)
                        {
                            item.Categorie = new CategorieDAO().findCategorieById(item.Categorie.Id);
                        }
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

        public void findLike(int? categorie, string query, BaseModelPagination<Produit> pagination, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                string request = "SELECT * FROM PRODUIT WHERE NOM LIKE @query";
                string count = "SELECT count(*) FROM PRODUIT WHERE NOM LIKE @query";
                if (categorie > 0)
                {
                    request += " AND CATEGORIEID = @categorie";
                    count += " AND CATEGORIEID = @categorie";
                }
                request += " ORDER BY NOM OFFSET @offset ROWS FETCH NEXT @maxResult ROWS ONLY ";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                SqlCommand countSqlCommand = new SqlCommand(count, conn);
                sqlCommand.Parameters.AddWithValue("@query", "%" + query + "%");
                sqlCommand.Parameters.AddWithValue("@maxResult", pagination.maxResult);
                sqlCommand.Parameters.AddWithValue("@offset", pagination.offset());
                countSqlCommand.Parameters.AddWithValue("@query", "%" + query + "%");
                if(categorie > 0)
                {
                    sqlCommand.Parameters.AddWithValue("@categorie", categorie);
                    countSqlCommand.Parameters.AddWithValue("@categorie", categorie);
                }
                try
                {
                    conn.Open();
                    pagination.totalResult = (int)countSqlCommand.ExecuteScalar();
                    List<Produit> liste = new List<Produit>();
                    reader = sqlCommand.ExecuteReader();
                    
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
                    
                    pagination.liste = liste;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    if (reader != null) reader.Close();
                    if (sqlCommand != null) sqlCommand.Dispose();
                    if (countSqlCommand != null) countSqlCommand.Dispose();
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
        public List<Categorie> findAllCategorie()
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
                        liste.Add(UtilDB.getInstance().createCategorie(reader));
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
    }
}