using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models
{
    public partial class Topic
    {
        public int TopicId { get; set; }
        public int TrainingId { get; set; }
        public string TopicName { get; set; }
        public string TopicDesc { get; set; }
        public string TopicFile { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
