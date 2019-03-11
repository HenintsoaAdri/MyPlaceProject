using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MyPlaceProject.DAO
{
    public class CommandeDAO
    {
        DetailCommandeDAO dcDao = new DetailCommandeDAO();

        public List<Commande> findAll(bool lazy){
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM COMMANDE";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    List<Commande> listeCommande = new List<Commande>();
                    if (lazy)
                    {
                        while (reader.Read())
                        {
                            listeCommande.Add(UtilDB.getInstance().createCommande(reader));
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Commande c = UtilDB.getInstance().createCommande(reader);
                            c.DetailCommande = dcDao.findAllByCommande(c, lazy);
                            listeCommande.Add(c);
                        }
                    }
                    return listeCommande;
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

        public List<Commande> findAllByUser(string id, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM COMMANDE WHERE APPLICATIONUSERID = @id";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                sqlCommand.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    List<Commande> listeCommande = new List<Commande>();
                    if (lazy)
                    {
                        while (reader.Read())
                        {
                            listeCommande.Add(UtilDB.getInstance().createCommande(reader));
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Commande c = UtilDB.getInstance().createCommande(reader);
                            c.DetailCommande = dcDao.findAllByCommande(c.Id, lazy);
                            listeCommande.Add(c);
                        }
                    }
                    return listeCommande;
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

        public Commande findByIdAndUser(int id, string userid, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM COMMANDE WHERE ID = @id AND APPLICATIONUSERID = @userid";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@userid", userid);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    Commande item = null;
                    if (reader.Read())
                    {
                        item = new Commande();
                        if (lazy)
                        {
                            item = UtilDB.getInstance().createCommande(reader);
                        }
                        else
                        {
                            item = UtilDB.getInstance().createCommande(reader);
                            item.DetailCommande = dcDao.findAllByCommande(item, lazy);
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
                    reader.Close();
                    if (conn != null) conn.Close();
                }
            }
        }

        public Commande findById(int id, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM COMMANDE WHERE ID = @id";
                SqlCommand sqlCommand = new SqlCommand(request, conn);
                sqlCommand.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    reader = sqlCommand.ExecuteReader();
                    Commande item = null;
                    if (reader.Read())
                    {
                        item = new Commande();
                        if (lazy)
                        {
                            item = UtilDB.getInstance().createCommande(reader);
                        }
                        else
                        {
                            item = UtilDB.getInstance().createCommande(reader);
                            item.DetailCommande = dcDao.findAllByCommande(item, lazy);
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
                    reader.Close();
                    if (conn != null) conn.Close();
                }
            }
        }

        public void save(Commande commande)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlTransaction trans = null;
                int idCommande;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    da.InsertCommand = new SqlCommand("INSERT INTO COMMANDE (DATE, APPLICATIONUSERID) output INSERTED.ID " +
                        "VALUES (@dateCommande, @id) ", conn, trans);
                    da.InsertCommand.Parameters.AddWithValue("@dateCommande", commande.Date);
                    da.InsertCommand.Parameters.AddWithValue("@id", commande.ApplicationUserID);
                    idCommande = (int)da.InsertCommand.ExecuteScalar();
                        
                    foreach (DetailCommande dc in commande.DetailCommande)
                    {
                        da.InsertCommand = new SqlCommand("INSERT INTO DETAILCOMMANDE (QUANTITE, DONE, PRIXUNITAIRE, COMMANDEID, PRODUITID) " +
                            "VALUES (@Quantite, @done, @PrixUnit, @CommandeId, @ProduitId) ", conn, trans);
                        da.InsertCommand.Parameters.AddWithValue("@Quantite", dc.Quantite);
                        da.InsertCommand.Parameters.AddWithValue("@PrixUnit", dc.PrixUnitaire);
                        da.InsertCommand.Parameters.AddWithValue("@done", dc.Done);
                        da.InsertCommand.Parameters.AddWithValue("@CommandeId", idCommande);
                        da.InsertCommand.Parameters.AddWithValue("@ProduitId", dc.ProduitId);
                        da.InsertCommand.ExecuteNonQuery();

                        da.UpdateCommand = new SqlCommand("UPDATE PRODUIT SET QUANTITESTOCK = QUANTITESTOCK - @Quantite", conn, trans);
                        da.UpdateCommand.Parameters.AddWithValue("@Quantite", dc.Quantite);
                        da.UpdateCommand.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw e;
                }
                finally
                {
                    if(conn!=null) conn.Close();
                }
            }
        }

    }
}