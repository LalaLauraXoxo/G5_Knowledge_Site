using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.KnowledgeApp.Mvc;
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

namespace ASI.Basecode.KnowledgeApp.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : ControllerBase<HomeController>
    {
        private readonly ITrainingService _trainingService;
        private readonly ITopicService _topicService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="localizer"></param>
        /// <param name="mapper"></param>
        public HomeController(ITrainingService trainingService,
                              ITopicService topicService,
                              IHttpContextAccessor httpContextAccessor,
                              ILoggerFactory loggerFactory,
                              IConfiguration configuration,
                              IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _trainingService = trainingService;
            _topicService = topicService;
        }

        /// <summary>
        /// Returns Home View.
        /// </summary>
        /// <returns> Home View </returns>
        [HttpGet]
        public IActionResult Index()
        {
            var data = _trainingService.GetTrainings();
            return View("Index", data);
        }

        [HttpGet]
        public IActionResult ViewTraining(int id)
        {
            var trainingWithRating = _trainingService.GetTrainingWithAverageRating(id);

            if (trainingWithRating != null)
            {
                ViewData["CategoryName"] = trainingWithRating.CategoryName;
                ViewData["AverageRating"] = trainingWithRating.AverageRating;
                ViewData["ImageUrl"] = trainingWithRating.ImageUrl;

                var topics = _topicService.GetTopicsByTrainingId(id);
                ViewData["Topics"] = topics;

                return View(trainingWithRating);
            }

            return NotFound();
        }


        public IActionResult RateTraining(int trainingId)
        {
            ViewData["TrainingId"] = trainingId;
            return View();
        }

        [HttpPost]
        public IActionResult RateTraining(RatingViewModel ratingViewModel, int trainingId)
        {
            try
            {
                _trainingService.AddRating(ratingViewModel, trainingId);
                TempData["SuccessMessage"] = "Training rated successfully";
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = "Failed to rate training";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
            }

            return RedirectToAction("ViewTraining", "Home", new { id = trainingId });
        }

        [HttpGet]
        public IActionResult DownloadTopic(int trainingId, int topicId)
        {
            var topic = _topicService.GetTopicWithFileExtension(topicId, trainingId);

            if (topic != null)
            {
                var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
                var filePath = Path.Combine(coverImagesPath, topic.TopicFile);

                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    var newFileName = "KnowBody Training Material"; 
                    return File(fileBytes, "application/octet-stream", newFileName + Path.GetExtension(filePath));
                }
            }

            return NotFound();
        }






    }
}
