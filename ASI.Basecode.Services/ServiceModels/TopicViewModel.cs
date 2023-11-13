using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public string TopicDesc { get; set; }


        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }
    }
}
