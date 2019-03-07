using MyPlaceProject.DAO;
using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Services
{
    public class ProduitService
    {
        private ProduitDAO dao = new ProduitDAO();

        private ProduitService() { }
        private static ProduitService instance = new ProduitService();
        public static ProduitService getInstance()
        {
            return instance;
        }

        public List<Produit> findAll()
        {
            return dao.findAll(true);
        }
    }
}