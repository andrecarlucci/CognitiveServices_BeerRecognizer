﻿using Newtonsoft.Json;

namespace Untappd.Models
{
    public class Beer
    {
        public int Bid { get; set; }
        [JsonProperty(PropertyName = "beer_name")]
        public string BeerName { get; set; }
        public string beer_label { get; set; }
        public float beer_abv { get; set; }
        public int beer_ibu { get; set; }
        public string beer_slug { get; set; }
        public string beer_description { get; set; }
        public string beer_style { get; set; }
        public bool has_had { get; set; }
        public int beer_active { get; set; }
        public int rating_count { get; set; }
        [JsonProperty(PropertyName = "rating_score")]
        public decimal RatingScore { get; set; }
    }
}
