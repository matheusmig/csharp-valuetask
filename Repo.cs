using System.Text.Json.Serialization;

namespace csharp_valuetask
{
    public class Repo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
    }
}
