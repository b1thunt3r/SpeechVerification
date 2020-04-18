using System;
using System.Runtime.Serialization;

namespace SpeechVerification.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public String ProfileId { get; set; }

        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public String Username { get; set; }
    }
}
