using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddCategory(CategoryViewModel categoryViewModel, string username);

        List<Category> GetCategories();

        Category GetCategory(int id);

        bool UpdateCategory(CategoryViewModel categoryViewModel, string username);

        bool DeleteCategory(CategoryViewModel categoryViewModel);

        CategoryViewModel GetCategoryViewModel(Category category);

        List<CategoryViewModel> GetCategoryViewModels();

        CategoryViewModel GetEditCategoryViewModel(Category category, int id);



    }
}
