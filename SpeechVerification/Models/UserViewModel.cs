using SpeechVerificationServcies;
using SpeechVerificationServcies.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpeechVerification.Models
{
    public class UserViewModel
    {
        public String ProfileId { get; set; }
        public String Email { get; set; }
        public String Username { get; set; }
        public Profile Profile { get; set; }
        public String ProfileJson { get; set; }

        public UserViewModel(User user, ProfileService profiles)
        {
            ProfileId = user.ProfileId;
            Email = user.Email;
            Username = user.Username;

            Profile = profiles.Get(user.ProfileId);

            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.WriteIndented = true;

            ProfileJson = JsonSerializer.Serialize(Profile, options);
        }
    }
}
