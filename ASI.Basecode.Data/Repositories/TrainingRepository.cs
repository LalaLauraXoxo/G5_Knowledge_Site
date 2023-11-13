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
    }
}
