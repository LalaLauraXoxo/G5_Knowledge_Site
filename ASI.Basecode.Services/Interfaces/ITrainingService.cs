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
        public void AddTraining(TrainingViewModel trainingViewModel, string username);

        public List<Training> GetTraining();

        public Training GetTraining(int id);

        public bool DeleteTraining(TrainingViewModel trainingViewModel);

        public bool UpdateTraining(TrainingViewModel trainingViewModel, string username);

        public List<Training> GetTrainingsByCategoryId(int categoryId);

        List<TrainingViewModel> GetTrainings();
    }
}
