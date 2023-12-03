using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface ITrainingService
    {
        void AddTraining(TrainingViewModel trainingViewModel, string username);

        List<Training> GetTraining();

        Training GetTraining(int id);

        bool UpdateTraining(TrainingViewModel trainingViewModel, string username);

        //void UpdateTraining(TrainingViewModel updatedTraining, string username);

        bool DeleteTraining(TrainingViewModel trainingViewModel);

        List<Training> GetTrainingsByCategoryId(int categoryId);

        List<TrainingViewModel> GetTrainings();

        TrainingViewModel GetTrainingViewModel(Training training, int id, Category category);

        TrainingViewModel GetEditTrainingViewModel(Training training, int id, Category category, List<CategoryViewModel> categoryViewModels);
        public string GetCategoryNameById(int categoryId);

        public void AddRating(RatingViewModel ratingViewModel, int trainingId);

        public List<Rating> GetRatingsByTrainingId(int trainingId);

        public TrainingViewModel GetTrainingWithAverageRating(int id);


    }
}
