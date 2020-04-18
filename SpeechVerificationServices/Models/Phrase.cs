using System;
using System.Text.Json.Serialization;

namespace SpeechVerificationServcies.Models
{
    public class Phrase
    {
        [JsonPropertyName("phrase")]
        public String Value { get; set; }
    }
}