using Newtonsoft.Json;

namespace TequiScanner.Shared.Model
{
    public class AnalyticsResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty("failed")]
        public bool Failed { get; set; }
        [JsonProperty("finished")]
        public bool Finished { get; set; }
        [JsonProperty("recognitionResult")]
        public RecognitionResult RecognitionResult { get; set; }
    }
}
