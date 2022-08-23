using System.Text.Json.Serialization;

namespace AdviceSlipService.Models
{
    public class SearchObject
    {
        [JsonPropertyName("total_results")]
        public string TotalResults { get; set; }

        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("slips")]
        public List<SlipObject> Slips { get; set; }
    }

    public class SlipObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("advice")]
        public string Advice { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}
