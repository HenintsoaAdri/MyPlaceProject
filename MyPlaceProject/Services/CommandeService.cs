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
        public List<Commande> findAllByUser(string id)
        {
            return dao.findAllByUser(id, true);
        }

        public Commande find(int id, string userid)
        {
            return dao.findByIdAndUser(id, userid,false);
        }

        public void create(Commande commande, string id)
        {
            commande.Date = DateTime.Now;
            commande.ApplicationUserID = id;
            dao.save(commande);
        }
    }
}