namespace Czytnik_Model.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public short HouseNumber { get; set; }
        public short FlatNumber { get; set; }
        public string StreetName{ get; set; }
        public short StreetNumber { get; set; }
        public string Town { get; set; }
        public string Post { get; set; }
        public string PostCode { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }


    }
}
