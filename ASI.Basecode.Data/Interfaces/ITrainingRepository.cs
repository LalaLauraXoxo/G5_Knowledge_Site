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
       
       public void AddTraining(Training training);

       public List<Training> GetTraining();

       public Training GetTraining(int id);

       public void UpdateTraining(Training training);

       public void DeleteTraining(Training training);

       IQueryable<Training> GetTrainings();
    }
}
