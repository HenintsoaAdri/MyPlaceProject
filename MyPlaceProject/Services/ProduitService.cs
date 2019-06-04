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
            return dao.findAll(false);
        }
        public BaseModelPagination<Produit> findLike(int categorie, string query = "", int page = 1, int maxResult = 10)
        {
            BaseModelPagination<Produit> pagination = new BaseModelPagination<Produit>(page, maxResult);
            dao.findLike(categorie, query, pagination, false);
            return pagination;
        }
        public List<Categorie> findAllCategorie()
        {
            List<Categorie> list = dao.findAllCategorie();
            Categorie categorie = new Categorie();
            categorie.Nom = "Tous";
            list.Insert(0, categorie);
            return list;
        }
    }
}