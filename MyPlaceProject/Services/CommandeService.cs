using MyPlaceProject.DAO;
using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Services
{
    public class CommandeService
    {
        private CommandeDAO dao = new CommandeDAO();

        private CommandeService() { }
        private static CommandeService instance = new CommandeService();
        public static CommandeService getInstance()
        {
            return instance;
        }

        public List<Commande> findAll()
        {
            return dao.findAll(true);
        }

        public Commande find(int id)
        {
            return dao.findById(id,false);
        }

        public void create(Commande commande)
        {
            commande.Date = DateTime.Now;
            dao.save(commande);
        }
    }
}