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
            var tags = await _client.GetTagsAsync(Config.AwesomeBeerProjectId);
            var tagInfo = tags.FirstOrDefault(x => x.Name == tag);
            if(tagInfo == null)
            {
                tagInfo = await _client.CreateTagAsync(Config.AwesomeBeerProjectId, tag);
            }
            var result = await _client.CreateImagesFromDataAsync(Config.AwesomeBeerProjectId,            
                                                                 image, 
                                                                 new Guid[] { tagInfo.Id });
            return result.IsBatchSuccessful;
        }

        public async Task<string> GetLatestModel()
        {
            var iterations = await _client.GetIterationsAsync(Config.AwesomeBeerProjectId);
            return iterations.FirstOrDefault()?.PublishName;
        }
    }
}
