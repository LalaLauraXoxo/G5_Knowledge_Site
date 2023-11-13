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
        public void AddCategory(Category category);

        public List<Category> GetCategory();

        public Category GetCategory(int id);

        public void UpdateCategory(Category category);

        public void DeleteCategory(Category category);
    }
}
