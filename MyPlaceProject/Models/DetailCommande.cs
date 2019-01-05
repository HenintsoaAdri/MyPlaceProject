namespace MyPlaceApp.Models
{
    public class DetailCommande
    {
        public Produit produit { get; set; }
        public int quantite { get; set; }
        public Commande commande { get; set; }
    }
}