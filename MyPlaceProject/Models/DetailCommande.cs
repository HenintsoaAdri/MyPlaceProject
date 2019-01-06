namespace MyPlaceProject.Models
{
    public class DetailCommande:BaseModele
    {
        public Produit Produit { get; set; }
        public int Quantite { get; set; }
        public Commande Commande { get; set; }
        public bool Done { get; set; }
    }
}