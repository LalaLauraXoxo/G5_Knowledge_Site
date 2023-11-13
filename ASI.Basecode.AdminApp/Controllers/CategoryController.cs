using ASI.Basecode.AdminApp.Mvc;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.AdminApp.Controllers
{
    public class CategoryController : ControllerBase<CategoryController>
    {
        private readonly ICategoryService _categoryService;
        private readonly ITrainingService _trainingService;

        //constructor to call service

        public CategoryController(ICategoryService categoryService, 
                                  ITrainingService trainingService,
                                  IHttpContextAccessor httpContextAccessor,
                                  ILoggerFactory loggerFactory,
                                  IConfiguration configuration,
                                  IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _categoryService = categoryService;
            _trainingService = trainingService;
        }


        public IActionResult TrainingCategories()
        {
            var category = _categoryService.GetCategory();
            return View(category);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            _categoryService.AddCategory(categoryViewModel, this.UserName);
            return RedirectToAction("TrainingCategories");
        }

        [HttpGet]
        public IActionResult ViewCategory(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category != null)
            {
                List<Training> trainings = _trainingService.GetTrainingsByCategoryId(id);

                List<TrainingViewModel> trainingViewModels = trainings.Select(training => new TrainingViewModel
                {
                    Id = training.Id,
                    TrainingName = training.TrainingName,
                    //CategoryId = category.Id,
                    
                }).ToList();

                CategoryViewModel categoryViewModel = new()
                {
                    Id = id,
                    CategoryName = category.CategoryName,
                    CategoryDesc = category.CategoryDesc,
                    Trainings = trainingViewModels
                };
                return View(categoryViewModel);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category != null)
            {
                CategoryViewModel categoryViewModel = new()
                {
                    Id = id,
                    CategoryName = category.CategoryName,
                    CategoryDesc = category.CategoryDesc,
                };

                return View(categoryViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryViewModel categoryViewModel)
        {
            bool isUpdated = _categoryService.UpdateCategory(categoryViewModel, this.UserName);
            if (isUpdated)
            {
                return RedirectToAction("TrainingCategories");
            }
            return NotFound();
        }

        public IActionResult DeleteCategory(CategoryViewModel categoryViewModel)
        {
            bool isDeleted = _categoryService.DeleteCategory(categoryViewModel);
            if (isDeleted)
            {
                return RedirectToAction("TrainingCategories");
            }
            return NotFound();
        }


    }
}
