using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int StarRating { get; set; }
    }
}
