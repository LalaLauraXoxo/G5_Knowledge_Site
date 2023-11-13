using ASI.Basecode.AdminApp.Mvc;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;

namespace ASI.Basecode.AdminApp.Controllers
{
    public class TopicController : ControllerBase<TopicController>
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService,
                               IHttpContextAccessor httpContextAccessor,
                               ILoggerFactory loggerFactory,
                               IConfiguration configuration,
                               IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _topicService = topicService;
        }
        public IActionResult Topics()
        {
            var topic = _topicService.GetTopic();
            return View(topic);
        }


        public IActionResult CreateTopic()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTopic(TopicViewModel topicViewModel)
        {
            _topicService.AddTopic(topicViewModel, this.UserId);
            return RedirectToAction("Topics");
        }

        public IActionResult DeleteTopic(TopicViewModel topicViewModel)
        {
            bool isDeleted = _topicService.DeleteTopic(topicViewModel);
            if (isDeleted)
            {
                return RedirectToAction("Topics");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ViewTopic(int id)
        {
            var topic = _topicService.GetTopic(id);
            if (topic != null)
            {
               //var topic = _topicService.GetTopic(id);

                TopicViewModel topicViewModel = new()
                {
                    Id = id,
                    TopicName = topic.TopicName,
                    TopicDesc = topic.TopicDesc,
                    //TrainingId = topic.TrainingId,
                };
                return View(topicViewModel);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult EditTopic(int id)
        {
            var topic = _topicService.GetTopic(id);
            if (topic != null)
            {
                //List<CategoryViewModel> categoryViewModels = GetCategoryViewModels();

                //int selectedCategoryId = training.CategoryId;
                var url = "https://127.0.0.1:8080/";

                TopicViewModel topicViewModel = new()
                {
                    Id = id,
                    TopicName = topic.TopicName,
                    TopicDesc = topic.TopicDesc,
                    ImageUrl = Path.Combine(url, topic.TopicFile + ".png"),
                };

                return View(topicViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditTopic(TopicViewModel topicViewModel)
        {
            bool isUpdated = _topicService.UpdateTopic(topicViewModel, this.UserName);
            if (isUpdated)
            {
                return RedirectToAction("Topics");
            }
            return NotFound();
        }
    }
}
