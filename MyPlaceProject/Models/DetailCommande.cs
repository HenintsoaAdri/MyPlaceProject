using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPlaceProject.Models
{
    public class DetailCommande
    {
        [Key, Column(Order = 0)]
        public int ProduitId { get; set; }
        [ForeignKey("ProduitId")]
        public virtual Produit Produit {
            get { return _Produit; }
            set { _Produit = value; PrixUnitaire = value.Prix; }
        }
        public virtual Produit _Produit { get; set; }

        public int Quantite { get; set; }

        [Key, Column(Order = 1)]
        public int CommandeId { get; set; }
        [ForeignKey("CommandeId")]
        public virtual Commande Commande { get; set; }

        public bool Done { get; set; }
        public float PrixUnitaire { get; set; }

        public float GetTotal()
        {
            return PrixUnitaire * Quantite;
        }
    }
}