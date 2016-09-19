using System;
using Newtonsoft.Json;

namespace rentMyJunk.Models
{
    public class Request
    {
        [JsonProperty(PropertyName = "id")]
        public string itemId { get; set; }

        public string requesterId { get; set; }

        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime startDate { get; set; }

        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime endDate { get; set; }
    }
}
