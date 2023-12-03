using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public void AddTopic(TopicViewModel topicViewModel, string username, int trainingId)
        {
            var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
            var model = new Topic();
            model.TopicId = topicViewModel.TopicId;
            model.TrainingId = trainingId;
            model.TopicName = topicViewModel.TopicName;
            model.TopicDesc = topicViewModel.TopicDesc;
            model.TopicFile = Guid.NewGuid().ToString();
            model.CreatedBy = username;
            model.CreatedTime = DateTime.Now;
            model.UpdatedBy = username;
            model.UpdatedTime = DateTime.Now;

            string fileExtension = Path.GetExtension(topicViewModel.MaterialFile.FileName);

            string defaultExtension = ".pdf";

            if (fileExtension.Equals(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                defaultExtension = ".mp4";
            }
            else if (fileExtension.Equals(".ppt", StringComparison.OrdinalIgnoreCase))
            {
                defaultExtension = ".ppt";
            }
            else if (fileExtension.Equals(".pptx", StringComparison.OrdinalIgnoreCase))
            {
                defaultExtension = ".pptx";
            }

            var coverImageFileName = Path.Combine(coverImagesPath, model.TopicFile) + defaultExtension;

            using (var fileStream = new FileStream(coverImageFileName, FileMode.Create))
            {
                topicViewModel.MaterialFile.CopyTo(fileStream);
            }

            _topicRepository.AddTopic(model);
        }


        public List<Topic> GetTopic()
        {
            var topic = _topicRepository.GetTopic();
            return topic;
        }

        public Topic GetTopic(int topicId, int trainingId)
        {
            var topic = _topicRepository.GetTopic(topicId, trainingId);

            return topic;
        }

        public TopicViewModel GetTopicViewModel(int id, int trainingId)
        {
            var topic = _topicRepository.GetTopic(id, trainingId); 

            if (topic != null)
            {
                var fileExtension = GetFileExtensionFromStorage(topic.TopicFile);
                return new TopicViewModel
                {
                    TopicId = id,
                    TopicName = topic.TopicName,
                    TopicDesc = topic.TopicDesc,
                    TopicFile = topic.TopicFile + fileExtension
                    // Other properties 
                };
            }
            return null;
        }

        public TopicViewModel GetEditTopicViewModel(int id, int trainingId)
        {
            var topic = _topicRepository.GetTopic(id, trainingId);
            if (topic != null)
            {
                var fileExtension = GetFileExtensionFromStorage(topic.TopicFile);
                return new TopicViewModel
                {
                    TopicId = id,
                    TopicName = topic.TopicName,
                    TopicDesc = topic.TopicDesc,
                    TopicFile = topic.TopicFile + fileExtension
                };
            }
            return null;
        }


        public bool UpdateTopic(TopicViewModel topicViewModel, string username, int trainingId)
        {
            Topic topic = _topicRepository.GetTopic(topicViewModel.TopicId, trainingId);

            // remove old file
            var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
            var oldFilePath = Path.Combine(coverImagesPath, topic.TopicFile) + GetFileExtension(topic.TopicFile);
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }

            var model = new Topic();
            model.TopicName = topicViewModel.TopicName;
            model.TopicDesc = topicViewModel.TopicDesc;
            model.TopicFile = Guid.NewGuid().ToString();
            model.CreatedBy = username;
            model.CreatedTime = DateTime.Now;
            model.UpdatedBy = username;
            model.UpdatedTime = DateTime.Now;

            // Extract file extension
            string fileExtension = Path.GetExtension(topicViewModel.MaterialFile.FileName);

            // Define default extension for unsupported types (e.g., ppt)
            string defaultExtension = ".ppt";

            // Map extensions based on file types
            if (fileExtension.Equals(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                defaultExtension = ".mp4";
            }
            else if (fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                defaultExtension = ".pdf";
            }
            else if (fileExtension.Equals(".pptx", StringComparison.OrdinalIgnoreCase))
            {
                defaultExtension = ".pptx";
            }

            var newFilePath = Path.Combine(coverImagesPath, model.TopicFile) + defaultExtension;

            using (var fileStream = new FileStream(newFilePath, FileMode.Create))
            {
                topicViewModel.MaterialFile.CopyTo(fileStream);
            }

            if (topic != null)
            {
                topic.TopicName = model.TopicName;
                topic.TopicDesc = model.TopicDesc;
                topic.UpdatedBy = model.UpdatedBy;
                topic.UpdatedTime = model.UpdatedTime;
                topic.TopicFile = model.TopicFile;

                _topicRepository.UpdateTopic(topic);
                return true;
            }

            return false;
        }

        private string GetFileExtensionFromStorage(string topicFile)
        {
            var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
            var matchingExtensions = new[] { ".mp4", ".pdf", ".pptx", ".ppt" };

            string fileExtension = matchingExtensions.FirstOrDefault(extension =>
            {
                string filePath = Path.Combine(coverImagesPath, topicFile + extension);
                if (System.IO.File.Exists(filePath))
                {
                    return true;
                }
                return false;
            });

            return fileExtension ?? "";
        }
        private string GetFileExtension2(string filename)
        {
            string[] allowedExtensions = { ".mp4", ".pdf", ".pptx", ".ppt" };

            foreach (var extension in allowedExtensions)
            {
                if (filename.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    return extension;
                }
            }

            return ""; // Return empty string if extension is not found
        }

        private string GetFileExtension(string filename)
        {
            if (filename.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                return ".mp4";
            }
            else if (filename.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return ".pdf";
            }
            else if (filename.EndsWith(".pptx", StringComparison.OrdinalIgnoreCase))
            {
                return ".pptx";
            }
            else if (filename.EndsWith(".ppt", StringComparison.OrdinalIgnoreCase))
            {
                return ".ppt";
            }

            return ".ppt";

        }


        public List<Topic> GetTopicsByTrainingId(int trainingId)
        {
            return _topicRepository.GetTopicsByTrainingId(trainingId);
        }

        public bool DeleteTopic(int id, int trainingId)
        {
            Topic topic = _topicRepository.GetTopic(id, trainingId);
            if (topic != null)
            {
                _topicRepository.DeleteTopic(topic);
                return true;
            }

            return false;
        }

        public bool DeleteTopicsByTrainingId(int trainingId)
        {
            return _topicRepository.DeleteTopicsByTrainingId(trainingId);
        }

        public Topic GetTopicWithFileExtension(int topicId, int trainingId)
        {
            var topic = _topicRepository.GetTopic(topicId, trainingId);

            if (topic != null)
            {
                var coverImagesPath = PathManager.DirectoryPath.CoverImagesDirectory;
                var matchingExtensions = new[] { ".mp4", ".pdf", ".pptx", ".ppt" };

                string fileExtension = matchingExtensions.FirstOrDefault(extension =>
                    System.IO.File.Exists(Path.Combine(coverImagesPath, topic.TopicFile + extension))
                );

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    topic = new Topic
                    {
                        // Copy existing properties to a new Topic object
                        TopicId = topic.TopicId,
                        TrainingId = topic.TrainingId,
                        // Copy other properties as needed

                        // Append the file extension
                        TopicFile = topic.TopicFile + fileExtension
                    };
                }
            }

            return topic;
        }




    }
}
