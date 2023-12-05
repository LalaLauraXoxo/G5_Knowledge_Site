using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASI.Basecode.Services.ServiceModels
{
    public class TrainingViewModel
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_# ]*$", ErrorMessage = "Training name cannot contain special characters.")]
        [Required(ErrorMessage = "Training Name is required.")]
        public string TrainingName { get; set; }

        [Required(ErrorMessage = "Training Descripiton is required.")]
        public string TrainingDesc { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Author cannot contain special characters.")]
        [Required(ErrorMessage = "Training Author is required.")]
        public string TrainingAuthor { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        [Required(ErrorMessage = "Training Image is required.")]
        public IFormFile ImageFile { get; set; }

        public string ImageUrl { get; set; }

        public double AverageRating { get; set; }
    }
}
