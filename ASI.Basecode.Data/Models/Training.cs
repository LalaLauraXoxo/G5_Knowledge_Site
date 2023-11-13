using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models
{
    public partial class Training
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDesc { get; set; }
        public string TrainingAuthor { get; set; }
        public string TrainingImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
