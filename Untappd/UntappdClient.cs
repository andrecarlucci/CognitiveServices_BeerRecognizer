using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Untappd.Models;

namespace Untappd
{
    public class UntappdClient
    {
        private const string _baseUrl = "https://api.untappd.com/v4/{0}?client_id={1}&client_secret={2}";
        private readonly HttpClient _client;

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public UntappdClient(HttpClient client, string untappdClientId, string untappdClientSecret)
        {
            _client = client;
            ClientId = untappdClientId;
            ClientSecret = untappdClientSecret;
        }

        public async Task<CheckinResult> GetCheckins(int beerId, long startFromId = 0)
        {
            var methodName = $"beer/checkins/{beerId}";
            var url = String.Format(_baseUrl, methodName, ClientId, ClientSecret);
            if (startFromId > 0)
            {
                url += "&max_id=" + startFromId;
            }
            var result = await _client.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<Rootobject>(result);
            return new CheckinResult
            {
                Checkins = response.Response.Checkins.Items.ToList(),
                NextId = Convert.ToInt64(response.Response.Pagination.max_id)
            };
        }

        public async Task<Beer> GetBeer(int beerId)
        {
            var methodName = $"beer/info/{beerId}";
            var url = String.Format(_baseUrl, methodName, ClientId, ClientSecret);
            var result = await _client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<GetBeerRoot>(result).Response.Beer;
        }
    }
}
