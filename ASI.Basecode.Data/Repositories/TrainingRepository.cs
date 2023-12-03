using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASI.Basecode.Data.Repositories
{
    public class TrainingRepository : BaseRepository, ITrainingRepository
    {
        public TrainingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public bool TrainingExists(string TrainingName)
        {
            return this.GetDbSet<Training>().Any(x => x.TrainingName == TrainingName);
        }

        //add training
        public void AddTraining(Training training)
        {
            this.GetDbSet<Training>().Add(training);
            UnitOfWork.SaveChanges();
        }
        
        //get all trainings
        public List<Training> GetTraining()
        {
            var training = GetDbSet<Training>().ToList();
            return training;
        }

        //retrieve single training
        public Training GetTraining(int id)
        {
            var training = this.GetDbSet<Training>().FirstOrDefault(x => x.Id == id);
            return training;
        }

        public List<Training> GetTrainingsByCategoryId(int categoryId)
        {
            return GetDbSet<Training>()
                .Where(t => t.CategoryId == categoryId)
                .ToList();
        }

        //update training
        public void UpdateTraining(Training training)
        {
            this.GetDbSet<Training>().Update(training);
            UnitOfWork.SaveChanges();
        }

        public void DeleteTraining(Training training)
        {
            this.GetDbSet<Training>().Remove(training);
            UnitOfWork.SaveChanges();
        }

        public IQueryable<Training> GetTrainings() 
        {
            return this.GetDbSet<Training>(); 
        }

        public string GetCategoryNameById(int categoryId)
        {
            var categoryName = GetDbSet<Category>()
                .Where(c => c.Id == categoryId)
                .Select(c => c.CategoryName)
                .FirstOrDefault();

            return categoryName;
        }

        public void AddRating(Rating rating)
        {
            this.GetDbSet<Rating>().Add(rating);
            UnitOfWork.SaveChanges();
        }

        public bool RatingEmailExists(string email)
        {
            return this.GetDbSet<Rating>().Any(x => x.Email == email);
        }

        public List<Rating> GetRatingsByTrainingId(int trainingId)
        {
            return GetDbSet<Rating>()
                    .Where(r => r.TrainingId == trainingId)
                    .ToList();
        }

        public double GetAverageRatingForTraining(int trainingId)
        {
            var ratings = GetDbSet<Rating>()
                            .Where(r => r.TrainingId == trainingId)
                            .Select(r => r.StarRating);

            if (!ratings.Any())
            {
                return 0; // or any default value for no ratings
            }

            return ratings.Average();
        }



    }
}
