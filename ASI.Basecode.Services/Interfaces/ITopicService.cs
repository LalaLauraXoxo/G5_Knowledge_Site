using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface ITopicService
    {
        public void AddTopic(TopicViewModel topicViewModel, string username);

        public List<Topic> GetTopic();

        public Topic GetTopic(int id);

        public bool DeleteTopic(TopicViewModel topicViewModel);

        public bool UpdateTopic(TopicViewModel topicViewModel, string username);

        //public List<Topic> GetTopicsByTrainingId(int trainingId);

    }
}
