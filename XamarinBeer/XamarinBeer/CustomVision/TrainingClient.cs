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
            //5
            return false;
        }

        public async Task<string> GetLatestModel()
        {
            var iterations = await _client.GetIterationsAsync(Config.ProjectId);
            return iterations.FirstOrDefault()?.PublishName;
        }

        public async Task<Guid> TrainModel()
        {
            var iteration = await _client.TrainProjectAsync(Config.ProjectId);
            while (iteration.Status == "Training")
            {
                await Task.Delay(1000);
                iteration = _client.GetIteration(Config.ProjectId, iteration.Id);
            }
            return iteration.Id;
        }

        public async Task PublishIteration(Guid iterationId, string publishName)
        {
            await _client.PublishIterationAsync(Config.ProjectId,
                                                iterationId,
                                                publishName,
                                                Config.CognitiveServicesResourceId);
        }
    }
}
