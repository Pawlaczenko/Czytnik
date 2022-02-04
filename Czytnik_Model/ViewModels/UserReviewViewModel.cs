using Czytnik_Model.Models;
using System;
using System.Collections.Generic;

namespace Czytnik_Model.ViewModels
{
    public class UserReviewViewModel
    {
        public Review Review { get; set; }
        public string BookTitle { get; set; }
        public List<string> Authors { get; set; }
    }
}
