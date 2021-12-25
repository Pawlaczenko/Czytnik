namespace Czytnik_Model.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }

        public Post Post { get; set; }
    }
}
