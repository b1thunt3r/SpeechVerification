using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechVerification.Models
{
    public class EnrollViewModel
    {
        public User User { get; set; }
        public IEnumerable<String> Pharases { get; set; }
    }
}
