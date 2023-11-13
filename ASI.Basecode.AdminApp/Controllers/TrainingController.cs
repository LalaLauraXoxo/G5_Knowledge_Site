using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IO;
using AutoMapper;
using ASI.Basecode.AdminApp.Mvc;

namespace ASI.Basecode.AdminApp.Controllers
{
    public class TrainingController : ControllerBase<TrainingController>
    {
        private readonly ITrainingService _trainingService;
        private readonly ICategoryService _categoryService;

        //constructor to call service

        public TrainingController(ITrainingService trainingService,
                                  ICategoryService categoryService,
                                  IHttpContextAccessor httpContextAccessor,
                                  ILoggerFactory loggerFactory,
                                  IConfiguration configuration,
                                  IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _trainingService = trainingService;
            _categoryService = categoryService;
        }
        private List<CategoryViewModel> GetCategoryViewModels()
        {
            List<Category> categories = _categoryService.GetCategory();
            return categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            }).ToList();
        }

        public IActionResult Trainings()
        {
            var training = _trainingService.GetTraining();
            return View(training);
        }

        public IActionResult CreateTraining()
        {
            ViewBag.Categories = GetCategoryViewModels();

            return View();
        }
        
        [HttpPost]
        public IActionResult CreateTraining(TrainingViewModel trainingViewModel)
        {
            _trainingService.AddTraining(trainingViewModel, this.UserId);
            return RedirectToAction("Trainings");
        }

        public IActionResult DeleteTraining(TrainingViewModel trainingViewModel)
        {
            bool isDeleted = _trainingService.DeleteTraining(trainingViewModel);
            if (isDeleted)
            {
                return RedirectToAction("Trainings");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ViewTraining(int id)
        {
            var training = _trainingService.GetTraining(id);
            if (training != null)
            {
                var category = _categoryService.GetCategory(training.CategoryId);
                var url = "https://127.0.0.1:8080/";

                TrainingViewModel trainingViewModel = new()
                {
                    Id = id,
                    TrainingName = training.TrainingName,
                    TrainingDesc = training.TrainingDesc,
                    TrainingAuthor = training.TrainingAuthor,
                    CategoryId = training.CategoryId,
                    CategoryName = category.CategoryName,
                    ImageUrl = Path.Combine(url, training.TrainingImage + ".png"),
                };
                return View(trainingViewModel);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult EditTraining(int id)
        {
            var training = _trainingService.GetTraining(id);
            if (training != null)
            {
                List<CategoryViewModel> categoryViewModels = GetCategoryViewModels();

                int selectedCategoryId = training.CategoryId;
                var url = "https://127.0.0.1:8080/";

                TrainingViewModel trainingViewModel = new()
                {
                    Id = id,
                    TrainingName = training.TrainingName,
                    TrainingDesc = training.TrainingDesc,
                    TrainingAuthor = training.TrainingAuthor,
                    CategoryId = training.CategoryId,
                    Categories = categoryViewModels,
                    ImageUrl = Path.Combine(url, training.TrainingImage + ".png"),
                };

                return View(trainingViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditTraining(TrainingViewModel trainingViewModel)
        {
            bool isUpdated = _trainingService.UpdateTraining(trainingViewModel, this.UserName);
            if (isUpdated)
            {
                return RedirectToAction("Trainings");
            }
            return NotFound();
        }


    }
}
