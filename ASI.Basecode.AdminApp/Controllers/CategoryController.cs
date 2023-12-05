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
using System;
using System.Collections.Generic;
using System.IO;
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
            var category = _categoryService.GetCategories();
            return View(category);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                _categoryService.AddCategory(categoryViewModel, this.UserName);
                return RedirectToAction("TrainingCategories");
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
            }
            return View();
        }

        [HttpGet]
        public IActionResult ViewCategory(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category != null)
            {
                return View(_categoryService.GetCategoryViewModel(category));
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category != null)
            {
                return View(_categoryService.GetEditCategoryViewModel(category, id));
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                bool isUpdated = _categoryService.UpdateCategory(categoryViewModel, this.UserName);
                if (isUpdated)
                {
                    return RedirectToAction("TrainingCategories");
                }
                return NotFound();
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
                return View(categoryViewModel);
            }
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
