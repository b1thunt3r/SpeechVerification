using System;
using System.Text.Json.Serialization;

namespace SpeechVerificationServcies.Models
{
    public class Verification
    {
        [JsonPropertyName("result")]
        public String Result { get; set; }

        [JsonPropertyName("confidence")]
        public String Confidence { get; set; }

        [JsonPropertyName("phrase")]
        public String Phrase { get; set; }
    }
}