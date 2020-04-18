using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeechVerification.Data;
using SpeechVerification.Extensions;
using SpeechVerification.Models;
using SpeechVerificationServcies;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechVerification.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly VerificationService _verificationService;

        public HomeController(ApplicationDbContext dbContext, VerificationService verificationService)
        {
            _dbContext = dbContext;
            _verificationService = verificationService;
        }

        public IActionResult Index()
        {
            var users = _dbContext.ExtUsers.AsEnumerable().OrderBy(user => user.Username);

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] String id, [FromForm] IFormFile voice)
        {
            var wav = await voice.Upload();
            var result = _verificationService.Verify(id, wav);

            return Json(new { Message = $"{result.Result} with {result.Confidence} confidence!" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
