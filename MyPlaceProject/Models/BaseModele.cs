namespace MyPlaceProject.Models
{
    public class BaseModele
    {
        public int Id { get; set; }

        public BaseModele() { }
        public BaseModele(int id)
        {
            Id = id;
        }
    }
}