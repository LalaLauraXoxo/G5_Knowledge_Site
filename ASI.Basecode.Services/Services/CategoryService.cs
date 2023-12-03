using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Resources.Views;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITrainingService _trainingService;

        public CategoryService(ITrainingService trainingService, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _trainingService = trainingService;
        }
        public void AddCategory(CategoryViewModel categoryViewModel, string username)
        {
            if (!_categoryRepository.CategoryExists(categoryViewModel.CategoryName))
            {
                Category category = new Category
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
            else
            {
                throw new InvalidDataException(Resources.Messages.Errors.CategoryExists);
            }

        }

        public List<Category> GetCategories()
        {
            var category = _categoryRepository.GetCategories();
            return category;
        }

        public Category GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);

            return category;
        }

        public CategoryViewModel GetCategoryViewModel(Category category)
        {
            var model = new CategoryViewModel();
            List<Training> trainings = _trainingService.GetTrainingsByCategoryId(category.Id);

            List<TrainingViewModel> trainingViewModels = trainings.Select(training => new TrainingViewModel
            {
                Id = training.Id,
                TrainingName = training.TrainingName,
                //CategoryId = category.Id,

            }).ToList();

            model = new()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryDesc = category.CategoryDesc,
                Trainings = trainingViewModels
            };
            return model;

        }

        public List<CategoryViewModel> GetCategoryViewModels()
        {
            List<Category> categories = _categoryRepository.GetCategorySelections();
            return categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            }).ToList();
        }

        public CategoryViewModel GetEditCategoryViewModel(Category category, int id)
        {
            var model = new CategoryViewModel();

            model = new()
            {
                Id = id,
                CategoryName = category.CategoryName,
                CategoryDesc = category.CategoryDesc,
            };

            return model;
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