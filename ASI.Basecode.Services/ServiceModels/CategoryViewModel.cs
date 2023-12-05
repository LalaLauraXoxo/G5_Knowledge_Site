using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Category name cannot contain special character.")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Category Description is required.")]
        public string CategoryDesc { get; set; }

        public List<TrainingViewModel> Trainings { get; set; }
    }
}
