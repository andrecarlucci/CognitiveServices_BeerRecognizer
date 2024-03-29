﻿using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinBeer.CustomVision
{
    public class PredictionClient
    {
        private CustomVisionPredictionClient _client;

        public PredictionClient()
        {
            _client = new CustomVisionPredictionClient()
            {
                ApiKey = Config.CognitiveServicesKey,
                Endpoint = Config.CognitiveServicesEndpoint
            };
        }

        public async Task<int> Predict(Stream image, string model)
        {
            return 5;
        }
    }
}
