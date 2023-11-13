using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
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

        public Topic GetTopic(int id)
        {
            var topic = this.GetDbSet<Topic>().FirstOrDefault(x => x.Id == id);
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

    }
}
