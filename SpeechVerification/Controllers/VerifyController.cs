using Microsoft.AspNetCore.Mvc;
using SpeechVerificationServcies;
using System.Linq;

namespace SpeechVerification.Controllers
{
    public class VerifyController : Controller
    {
        private readonly VerificationService _verificationService;

        public VerifyController(VerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pharases()
        {
            var pharases = _verificationService.GetPharses().Select(p => p.Value);

            return View(pharases);
        }
    }
}
