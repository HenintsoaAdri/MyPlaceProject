namespace MyPlaceProject.Models
{
    public class Categorie : BaseModele
    {
        public string Nom { get; set; }


        public Categorie() { }
        public Categorie(int id) : base(id) { }


    }
}