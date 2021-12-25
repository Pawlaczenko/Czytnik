using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czytnik_Model.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }

        public Admin Admin { get; set; }
    }
}
