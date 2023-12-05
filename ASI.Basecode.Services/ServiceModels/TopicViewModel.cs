using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class TopicViewModel
    {
        public int TopicId { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_# ]*$", ErrorMessage = "Topic name cannot contain special characters.")]
        [Required(ErrorMessage = "Topic Name is required.")]
        public string TopicName { get; set; }

        [Required(ErrorMessage = "Topic Description is required.")]
        public string TopicDesc { get; set; }

        [Required(ErrorMessage = "Resource material is required.")]
        public IFormFile MaterialFile { get; set; }
        public string FileUrl { get; set; }

        public string TopicFile { get; set; }
    }
}
