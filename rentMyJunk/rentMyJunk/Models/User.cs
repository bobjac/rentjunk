using Newtonsoft.Json;

namespace rentMyJunk.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string address { get; set; } 
    }

}
