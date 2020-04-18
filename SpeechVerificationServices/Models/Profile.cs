using System;
using System.Text.Json.Serialization;

namespace SpeechVerificationServcies.Models
{
    public class Profile
    {
        [JsonPropertyName("verificationProfileId")]
        public String VerificationProfileId { get; set; }

        [JsonPropertyName("enrollmentsCount")]
        public Int32 EnrollmentsCount { get; set; }

        [JsonPropertyName("remainingEnrollmentsCount")]
        public Int32 RemainingEnrollmentsCount { get; set; }

        [JsonPropertyName("locale")]
        public String Locale { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonPropertyName("lastActionDateTime")]
        public DateTime LastActionDateTime { get; set; }

        [JsonPropertyName("EnrollmentStatus")]
        public String EnrollmentStatus { get; set; }
    }
}