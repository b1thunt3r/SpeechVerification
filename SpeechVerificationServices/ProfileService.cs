using SpeechVerificationServcies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace SpeechVerificationServcies
{
    public class ProfileService
    {
        private readonly ApiClient _apiClient;

        public ProfileService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Profile Create(String locale = "en-US")
        {
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes($"{{\"locale\":\"{locale}\"}}"));
            var result = _apiClient.PostAsync<Profile>("/verificationProfiles/", content).Result;

            return result;
        }

        public EnrollResult Enroll(String id, FileInfo audioFile)
        {
            var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString("u"))
                {
                    { new StreamContent(audioFile.OpenRead()), "Data", "testfile_" + audioFile.Name }
                };

            var result = _apiClient.PostAsync<EnrollResult>($"/verificationProfiles/{id}/enroll", content).Result;

            return result;
        }

        public Profile Get(String id)
        {
            var result = _apiClient.GetAsync<Profile>($"/verificationProfiles/{id}/").Result;
            return result;
        }

        public IEnumerable<Profile> GetAll()
        {
            var result = _apiClient.GetAsync<IEnumerable<Profile>>("/verificationProfiles/").Result;

            return result;
        }

        public void Delete(String id)
        {
            _apiClient.DeleteAsync($"/verificationProfiles/{id}/").ConfigureAwait(false);
        }

        public void ResetEnrollments(String id)
        {
            _apiClient.DeleteAsync($"/verificationProfiles/{id}/reset").ConfigureAwait(false);
        }
    }
}
