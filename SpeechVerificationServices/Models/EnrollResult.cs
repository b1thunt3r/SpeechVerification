using System;
using System.Text.Json.Serialization;

namespace SpeechVerificationServcies.Models
{
    public class EnrollResult
    {
        [JsonPropertyName("error")]
        public EnrollError Error { get; set; }

        [JsonPropertyName("enrollmentStatus")]
        public String EnrollmentStatus { get; set; }

        [JsonPropertyName("enrollmentsCount")]
        public Int32 EnrollmentsCount { get; set; }

        [JsonPropertyName("remainingEnrollments")]
        public Int32 RemainingEnrollments { get; set; }

        [JsonPropertyName("phrase   ")]
        public String Phrase { get; set; }
    }

    public class EnrollError
    {
        [JsonPropertyName("code")]
        public String Code { get; set; }

        [JsonPropertyName("message")]
        public String Message { get; set; }
    }
}
