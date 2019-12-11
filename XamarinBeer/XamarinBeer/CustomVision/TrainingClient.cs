using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinBeer.CustomVision
{
    public class TrainingClient
    {
        private CustomVisionTrainingClient _client;

        public TrainingClient()
        {
            _client = new CustomVisionTrainingClient()
            {
                ApiKey = Config.CognitiveServicesKey,
                Endpoint = Config.CognitiveServicesEndpoint
            };
        }

        public async Task<bool> AddImage(Stream image, string tag)
        {
            var projectId = Config.AwesomeBeerProjectId;
            var tags = await _client.GetTagsAsync(projectId);
            var tagInfo = tags.FirstOrDefault(x => x.Name == tag);
            if(tagInfo == null)
            {
                tagInfo = await _client.CreateTagAsync(projectId, tag);
            }
            var result = await _client.CreateImagesFromDataAsync(projectId,            
                                                                 image,
                                                                 new Guid[] { tagInfo.Id });
            return result.IsBatchSuccessful;
        }

        public async Task<string> GetLatestModel()
        {
            var iterations = await _client.GetIterationsAsync(Config.AwesomeBeerProjectId);
            return iterations.FirstOrDefault()?.PublishName;
        }

        public async Task<Guid> TrainModel()
        {
            var iteration = await _client.TrainProjectAsync(Config.AwesomeBeerProjectId);
            while (iteration.Status == "Training")
            {
                await Task.Delay(1000);
                iteration = _client.GetIteration(Config.AwesomeBeerProjectId, iteration.Id);
            }
            return iteration.Id;
        }

        public async Task PublishIteration(Guid iterationId, string publishName)
        {
            await _client.PublishIterationAsync(Config.AwesomeBeerProjectId,
                                                iterationId,
                                                publishName,
                                                Config.CognitiveServicesResourceId);
        }
    }
}
