using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
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
    public class TrainingService : ITrainingService
    {
        private readonly KnowBody_DBContext _dbContext;
        private readonly ITrainingRepository _trainingRepository;

        public TrainingService(ITrainingRepository trainingRepository, KnowBody_DBContext dbContext)
        {
            _trainingRepository = trainingRepository;
            _dbContext = dbContext;
        }
        
        public void AddTraining(TrainingViewModel trainingViewModel, string username)
        {

            var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
            var model = new Training();
            model.Id = trainingViewModel.Id;
            model.CategoryId = trainingViewModel.CategoryId;
            model.TrainingName = trainingViewModel.TrainingName;
            model.TrainingDesc = trainingViewModel.TrainingDesc;
            model.TrainingAuthor = trainingViewModel.TrainingAuthor;
            model.TrainingImage = Guid.NewGuid().ToString();
            model.CreatedBy = username;
            model.CreatedTime = DateTime.Now;
            model.UpdatedBy = username;
            model.UpdatedTime = DateTime.Now;

            var coverImageFileName = Path.Combine(coverImagesPath, model.TrainingImage) + ".png";
            using (var fileStream = new FileStream(coverImageFileName, FileMode.Create))
            {
                trainingViewModel.ImageFile.CopyTo(fileStream);
            }
           

            _trainingRepository.AddTraining(model);
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
           if (training != null)
           {
                training.Id = trainingViewModel.Id;
                training.CategoryId = trainingViewModel.CategoryId;
                training.TrainingName = trainingViewModel.TrainingName;
                training.TrainingDesc = trainingViewModel.TrainingDesc;
                training.TrainingAuthor = trainingViewModel.TrainingAuthor;
                training.UpdatedBy = username;
                training.UpdatedTime = System.DateTime.Now;

                _trainingRepository.UpdateTraining(training);
               return true;
           }

           return false;
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

        public List<Training> GetTrainingsByCategoryId(int categoryId)
        {
            return _dbContext.Trainings
                .Where(t => t.CategoryId == categoryId)
                .ToList();
        }

        public List<TrainingViewModel> GetTrainings()
        {
            var url = "https://127.0.0.1:8080/";
            var data = _trainingRepository.GetTrainings().Select(s => new TrainingViewModel
            {
                TrainingName = s.TrainingName,
                TrainingAuthor = s.TrainingAuthor,
                ImageUrl = Path.Combine(url, s.TrainingImage + ".png"),
        }).ToList();
            return data;
        }


    }
}
