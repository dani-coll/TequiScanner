using System.Collections.Generic;
using Newtonsoft.Json;

namespace TequiScanner.Shared.Model
{
    public class RecognitionResult
    {
        [JsonProperty("lines")]
        public List<Line> Lines { get; set; }
    }
}
