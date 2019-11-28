using System;

namespace XamarinBeer
{
    public static class Config
    {
        public static string CognitiveServicesKey = Environment.GetEnvironmentVariable("DEV_COGNITIVE_SERVICES_KEY");
        public static string CognitiveServicesEndpoint = Environment.GetEnvironmentVariable("DEV_COGNITIVE_SERVICES_ENDPOINT");
        public static string CognitiveServicesRegion = Environment.GetEnvironmentVariable("DEV_COGNITIVE_SERVICES_REGION");
        public static string UntappdClientId = Environment.GetEnvironmentVariable("DEV_UNTAPPD_CLIENT_ID");
        public static string UntappdSecret = Environment.GetEnvironmentVariable("DEV_UNTAPPD_CLIENT_SECRET");

        public static Guid AwesomeBeerProjectId = new Guid("958f3199-74b9-44d2-8f49-4f2a715849bc");

        //https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/language-support
        public static string Language = "en";
        public static string SpeechLanguage = "en";
    }
}
