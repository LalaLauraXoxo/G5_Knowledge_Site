using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface ICategoryRepository
    {
        bool CategoryExists(string CategoryName);

        public void AddCategory(Category category);

        public List<Category> GetCategories();

        public Category GetCategory(int id);

        public List<Category> GetCategorySelections();

        public void UpdateCategory(Category category);

        public void DeleteCategory(Category category);
    }
}
