using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASI.Basecode.Services.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TrainingService(ITrainingRepository trainingRepository, ICategoryRepository categoryRepository)
        {
            _trainingRepository = trainingRepository;
            _categoryRepository = categoryRepository;
        }

        public void AddTraining(TrainingViewModel trainingViewModel, string username)
        {
            if (!_trainingRepository.TrainingExists(trainingViewModel.TrainingName))
            {
                var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
                Training training = new Training
                {
                    Id = trainingViewModel.Id,
                    CategoryId = trainingViewModel.CategoryId,
                    TrainingName = trainingViewModel.TrainingName,
                    TrainingDesc = trainingViewModel.TrainingDesc,
                    TrainingAuthor = trainingViewModel.TrainingAuthor,
                    TrainingImage = Guid.NewGuid().ToString(),
                    CreatedBy = username,
                    CreatedTime = DateTime.Now,
                    UpdatedBy = username,
                    UpdatedTime = DateTime.Now
                };

                var coverImageFileName = Path.Combine(coverImagesPath, training.TrainingImage) + ".png";
                using (var fileStream = new FileStream(coverImageFileName, FileMode.Create))
                {
                    trainingViewModel.ImageFile.CopyTo(fileStream);
                }

                _trainingRepository.AddTraining(training);
            }
            else
            {
                throw new InvalidDataException(Resources.Messages.Errors.TrainingExists);
            }
        }

        public List<Training> GetTraining()
        {
            var training = _trainingRepository.GetTraining();
            return training;
        }

        public Training GetTraining(int id)
        {
            var training = _trainingRepository.GetTraining(id);
            return training;
        }


        public bool UpdateTraining(TrainingViewModel trainingViewModel, string username)
        {
            Training training = _trainingRepository.GetTraining(trainingViewModel.Id);
            //remove old image
            var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
            var oldImagePath = Path.Combine(coverImagesPath, training.TrainingImage) + ".png";
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }

            var imageFileName = Guid.NewGuid().ToString();
            // Save the new image file
            var newImagePath = Path.Combine(coverImagesPath, imageFileName + ".png");
            using (var fileStream = new FileStream(newImagePath, FileMode.Create))
            {
                trainingViewModel.ImageFile.CopyTo(fileStream);
            }

            if (training != null)
            {
                training.Id = trainingViewModel.Id;
                training.CategoryId = trainingViewModel.CategoryId;
                training.TrainingName = trainingViewModel.TrainingName;
                training.TrainingDesc = trainingViewModel.TrainingDesc;
                training.TrainingAuthor = trainingViewModel.TrainingAuthor;
                training.TrainingImage = imageFileName;
                training.UpdatedBy = username;
                training.UpdatedTime = System.DateTime.Now;

                _trainingRepository.UpdateTraining(training);
                return true;
            }

            return false;
        }

        public List<Training> GetTrainingsByCategoryId(int categoryId)
        {
            return _trainingRepository.GetTrainingsByCategoryId(categoryId);
        }

        public TrainingViewModel GetTrainingViewModel(Training training, int id, Category category)
        {
            var model = new TrainingViewModel();
            var url = "https://127.0.0.1:8080/";

            model = new TrainingViewModel
            {
                Id = id,
                TrainingName = training.TrainingName,
                TrainingDesc = training.TrainingDesc,
                TrainingAuthor = training.TrainingAuthor,
                CategoryId = training.CategoryId,
                CategoryName = category != null ? category.CategoryName : "No category selected",
                ImageUrl = Path.Combine(url, training.TrainingImage + ".png"),
            };
            return model;
        }

        public TrainingViewModel GetEditTrainingViewModel(Training training, int id, Category category, List<CategoryViewModel> categoryViewModels)
        {
            var model = new TrainingViewModel();
            var url = "https://127.0.0.1:8080/";

            model = new TrainingViewModel
            {
                Id = id,
                TrainingName = training.TrainingName,
                TrainingDesc = training.TrainingDesc,
                TrainingAuthor = training.TrainingAuthor,
                CategoryId = training.CategoryId,
                Categories = categoryViewModels, // Pass category view models received from the controller
                CategoryName = category != null ? category.CategoryName : "No category selected",
                ImageUrl = Path.Combine(url, training.TrainingImage + ".png"),
            };

            return model;
        }

        public bool DeleteTraining(TrainingViewModel trainingViewModel)
        {
            Training training = _trainingRepository.GetTraining(trainingViewModel.Id);
            if (training != null)
            {
                _trainingRepository.DeleteTraining(training);
                return true;
            }

            return false;
        }

        // FOR KNOWBODY APP PART
        public List<TrainingViewModel> GetTrainings()
        {
            var url = "https://127.0.0.1:8080/";

            // Fetch training data
            var trainings = _trainingRepository.GetTrainings();

            // Get average ratings for each training separately
            var data = trainings.Select(s => new TrainingViewModel
            {
                Id = s.Id,
                TrainingName = s.TrainingName,
                TrainingAuthor = s.TrainingAuthor,
                CategoryId = s.CategoryId, // new added
                CategoryName = _trainingRepository.GetCategoryNameById(s.CategoryId),
                ImageUrl = Path.Combine(url, s.TrainingImage + ".png")
            }).ToList();

            // Calculate average rating for each training separately
            foreach (var trainingViewModel in data)
            {
                trainingViewModel.AverageRating = CalculateAverageRating(trainingViewModel.Id);
            }

            return data;
        }

        public TrainingViewModel GetTrainingWithAverageRating(int id)
        {
            var training = _trainingRepository.GetTraining(id);
            var categoryName = _trainingRepository.GetCategoryNameById(training.CategoryId);
            var imageUrl = Path.Combine("https://127.0.0.1:8080/", training.TrainingImage + ".png");

            var trainingViewModel = new TrainingViewModel
            {
                Id = training.Id,
                TrainingName = training.TrainingName,
                TrainingDesc = training.TrainingDesc,
                TrainingAuthor = training.TrainingAuthor,
                CategoryId = training.CategoryId,
                CategoryName = categoryName ?? "No category selected",
                ImageUrl = imageUrl,
                AverageRating = CalculateAverageRating(training.Id)
            };

            return trainingViewModel;
        }

        private double CalculateAverageRating(int trainingId)
        {
            var ratings = _trainingRepository.GetRatingsByTrainingId(trainingId);
            return ratings.Any() ? ratings.Average(r => r.StarRating) : 0;
        }

        public string GetCategoryNameById(int categoryId)
        {
            return _trainingRepository.GetCategoryNameById(categoryId);
        }

        public void AddRating(RatingViewModel ratingViewModel, int trainingId)
        {
            Rating rating = new Rating
            {
                Id = ratingViewModel.Id,
                TrainingId = trainingId,
                Name = ratingViewModel.Name,
                Email = ratingViewModel.Email,
                Comment = ratingViewModel.Comment,
                StarRating = ratingViewModel.StarRating,
            };

            _trainingRepository.AddRating(rating);

        }
       
        public List<Rating> GetRatingsByTrainingId(int trainingId)
        {
            return _trainingRepository.GetRatingsByTrainingId(trainingId);
        }
    }
}
