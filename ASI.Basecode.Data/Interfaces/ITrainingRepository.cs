using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface ITrainingRepository
    {
       public bool TrainingExists(string TrainingName);

       public void AddTraining(Training training);

       public List<Training> GetTraining();

       public Training GetTraining(int id);

       public List<Training> GetTrainingsByCategoryId(int categoryId);

       public void UpdateTraining(Training training);

       public void DeleteTraining(Training training);

       IQueryable<Training> GetTrainings();

       public string GetCategoryNameById(int categoryId);

       public void AddRating(Rating rating);

       public bool RatingEmailExists(string email);

       public List<Rating> GetRatingsByTrainingId(int trainingId);

        public double GetAverageRatingForTraining(int trainingId);
    }
}
