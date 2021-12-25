using System;

namespace Czytnik.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public string OriginalLanguage { get; set; }
        public string EditionLanguage { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string IssueNumber { get; set; }
        public int PageQuantity { get; set; }
        public string Dimensions { get; set; }
        public int CategoryId { get; set; }
        public int SeriesId { get; set; }
        public int PublisherId { get; set; }
        public float Rating { get; set; }
        public bool IsInStock { get; set; }
    }
}
