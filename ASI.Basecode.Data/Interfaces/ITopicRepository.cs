﻿using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface ITopicRepository
    {
        public void AddTopic(Topic topic);

        public List<Topic> GetTopic();

        public Topic GetTopic(int topicId, int trainingId);

        public void UpdateTopic(Topic topic);

        public void DeleteTopic(Topic topic);

        public List<Topic> GetTopicsByTrainingId(int trainingId);

        public bool DeleteTopicsByTrainingId(int trainingId);

    }
}
