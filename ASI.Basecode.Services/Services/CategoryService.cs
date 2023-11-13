using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Resources.Views;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void AddCategory(CategoryViewModel categoryViewModel, string username)
        {
            Category category = new()
            {
                Id = categoryViewModel.Id,
                CategoryName = categoryViewModel.CategoryName,
                CategoryDesc = categoryViewModel.CategoryDesc,
                CreatedBy = username,
                CreatedTime = DateTime.Now,
                UpdatedBy = username,
                UpdatedTime = DateTime.Now,
            };

            _categoryRepository.AddCategory(category);
        }

        public List<Category> GetCategory()
        {
            var category = _categoryRepository.GetCategory();
            return category;
        }

        public Category GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);

            return category;
        }

        public bool UpdateCategory(CategoryViewModel categoryViewModel, string username)
        {
            Category category = _categoryRepository.GetCategory(categoryViewModel.Id);
            if (category != null)
            {
                category.Id = categoryViewModel.Id;
                category.CategoryName = categoryViewModel.CategoryName;
                category.CategoryDesc = categoryViewModel.CategoryDesc;
                category.UpdatedBy = username;
                category.UpdatedTime = System.DateTime.Now;

                _categoryRepository.UpdateCategory(category);
                return true;
            }

            return false;
        }
        public bool DeleteCategory(CategoryViewModel categoryViewModel)
        {
            Category category = _categoryRepository.GetCategory(categoryViewModel.Id);
            if (category != null)
            {
                _categoryRepository.DeleteCategory(category);
                return true;
            }

            return false;
        }


    }
}