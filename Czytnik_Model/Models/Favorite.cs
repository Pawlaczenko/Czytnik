namespace Czytnik_Model.Models
{
    public class Favourite
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
