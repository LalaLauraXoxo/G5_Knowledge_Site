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
        public void AddTopic(TopicViewModel topicViewModel, string username, int trainingId);

        public List<Topic> GetTopic();

        public Topic GetTopic(int topicId, int trainingId);

        public bool UpdateTopic(TopicViewModel topicViewModel, string username, int trainingId);

        public List<Topic> GetTopicsByTrainingId(int trainingId);

        public TopicViewModel GetTopicViewModel(int id, int trainingId);

        public TopicViewModel GetEditTopicViewModel(int id, int trainingId);

        public bool DeleteTopic(int id, int trainingId);

        public bool DeleteTopicsByTrainingId(int trainingId);

        public Topic GetTopicWithFileExtension(int topicId, int trainingId);

    }
}
