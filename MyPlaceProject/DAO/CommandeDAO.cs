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

        public List<Commande> findAllByUser(int id, bool lazy)
        {
            using (SqlConnection conn = UtilDB.getInstance().getConnection())
            {
                SqlDataReader reader = null;
                String request = "SELECT * FROM COMMANDE WHERE IDUSER = @id";
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
                    Commande item = new Commande();
                    if (reader.Read())
                    {
                        if (lazy)
                        {
                            item = UtilDB.getInstance().createCommande(reader);
                        }
                        else
                        {
                            item = UtilDB.getInstance().createCommande(reader);
                            item.DetailCommande = dcDao.findAllByCommande(item.Id, lazy);
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

                    da.InsertCommand = new SqlCommand("INSERT INTO COMMANDE (DATE) output INSERTED.ID " +
                        "VALUES (@dateCommande) ", conn, trans);
                    da.InsertCommand.Parameters.AddWithValue("@dateCommande", commande.Date);
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