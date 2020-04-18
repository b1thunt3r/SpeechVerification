using SpeechVerificationServcies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace SpeechVerificationServcies
{
    public class VerificationService
    {
        private readonly ApiClient _apiClient;

        public VerificationService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Verification Verify(String id, FileInfo audioFile)
        {
            var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString("u"))
            {
                { new StreamContent(audioFile.OpenRead()), "verificationData", "tempfile_" + audioFile.Name }
            };

            var result = _apiClient.PostAsync<Verification>($"/verify?verificationProfileId={id}", content).Result;

            return result;
        }

        public IEnumerable<Phrase> GetPharses(String locale = "en-US")
        {
            var result = _apiClient.GetAsync<IEnumerable<Phrase>>($"/verificationPhrases?locale={locale}").Result;
            return result;
        }
    }
}