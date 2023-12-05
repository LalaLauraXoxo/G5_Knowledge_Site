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
using System;

namespace ASI.Basecode.AdminApp.Controllers
{
    public class TrainingController : ControllerBase<TrainingController>
    {
        private readonly ITrainingService _trainingService;
        private readonly ICategoryService _categoryService;
        private readonly ITopicService _topicService;

        //constructor to call service

        public TrainingController(ITrainingService trainingService,
                                  ICategoryService categoryService,
                                  ITopicService topicService,
                                  IHttpContextAccessor httpContextAccessor,
                                  ILoggerFactory loggerFactory,
                                  IConfiguration configuration,
                                  IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _trainingService = trainingService;
            _categoryService = categoryService;
            _topicService = topicService;
        }
        public IActionResult Trainings()
        {
            var training = _trainingService.GetTraining();
            return View(training);
        }

        public IActionResult CreateTraining()
        {
            ViewBag.Categories = _categoryService.GetCategoryViewModels();
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateTraining(TrainingViewModel trainingViewModel)
        {
            try
            {
                _trainingService.AddTraining(trainingViewModel, this.UserId);
                return RedirectToAction("Trainings");
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.Categories = _categoryService.GetCategoryViewModels();
                return View(trainingViewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
                ViewBag.Categories = _categoryService.GetCategoryViewModels();
                return View(trainingViewModel);
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult ViewTraining(int id)
        {
            var training = _trainingService.GetTraining(id);
            if (training != null)
            {
                var category = _categoryService.GetCategory(training.CategoryId);
                var trainingViewModel = _trainingService.GetTrainingViewModel(training, id, category);

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
                var category = _categoryService.GetCategory(training.CategoryId);
                var categoryViewModels = _categoryService.GetCategoryViewModels();

                var trainingViewModel = _trainingService.GetEditTrainingViewModel(training, id, category, categoryViewModels); // Pass category view models as a parameter

                return View(trainingViewModel);
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult EditTraining(TrainingViewModel trainingViewModel)
        {
            if (ModelState.IsValid) // Ensure model state is valid before updating
            {
                bool isUpdated = _trainingService.UpdateTraining(trainingViewModel, this.UserName);
                if (isUpdated)
                {
                    return RedirectToAction("Trainings");
                }
            }
            // If update fails or model state is invalid, return to the edit view
            return View(trainingViewModel);
        }


        public IActionResult DeleteTraining(TrainingViewModel trainingViewModel)
        {
            var training = _trainingService.GetTraining(trainingViewModel.Id);

            if (training == null)
            {
                return NotFound();
            }

            bool isDeleted = _trainingService.DeleteTraining(trainingViewModel);
            if (isDeleted)
            {
                _topicService.DeleteTopicsByTrainingId(training.Id);
                return RedirectToAction("Trainings");
            }
            return NotFound();
        }
        

        public IActionResult ViewRatings(int trainingId)
        {
            ViewData["TrainingId"] = trainingId;
            // Call the service method to get ratings for the specified trainingId
            var ratings = _trainingService.GetRatingsByTrainingId(trainingId);
            // Return the view with the fetched ratings
            return View(ratings);
        }

    }
}
        
      
