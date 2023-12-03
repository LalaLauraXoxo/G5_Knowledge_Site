using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class TopicRepository : BaseRepository, ITopicRepository
    {
        public TopicRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void AddTopic(Topic topic)
        {
            this.GetDbSet<Topic>().Add(topic);
            UnitOfWork.SaveChanges();
        }

        public List<Topic> GetTopic()
        {
            var topic = GetDbSet<Topic>().ToList();
            return topic;
        }

        public Topic GetTopic(int topicId, int trainingId)
        {
            var topic = this.GetDbSet<Topic>().FirstOrDefault(x => x.TopicId == topicId && x.TrainingId == trainingId);
            return topic;
        }

        public void UpdateTopic(Topic topic)
        {
            this.GetDbSet<Topic>().Update(topic);
            UnitOfWork.SaveChanges();
        }

        public void DeleteTopic(Topic topic)
        {
            this.GetDbSet<Topic>().Remove(topic);
            UnitOfWork.SaveChanges();
        }

        public bool DeleteTopicsByTrainingId(int trainingId)
        {
            var topicsToDelete = GetDbSet<Topic>().Where(t => t.TrainingId == trainingId);
            if (topicsToDelete.Any())
            {
                GetDbSet<Topic>().RemoveRange(topicsToDelete);
                UnitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Topic> GetTopicsByTrainingId(int trainingId)
        {
            return GetDbSet<Topic>().Where(t => t.TrainingId == trainingId).ToList();
        }

    }
}
