using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeechVerification.Data;
using SpeechVerification.Extensions;
using SpeechVerification.Models;
using SpeechVerificationServcies;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechVerification.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ProfileService _profiles;
        private readonly VerificationService _verificationService;

        public UserController(ApplicationDbContext dbContext, ProfileService profiles, VerificationService verificationService)
        {
            _dbContext = dbContext;
            _profiles = profiles;
            _verificationService = verificationService;
        }

        public IActionResult Index()
        {
            var users = _dbContext.ExtUsers.Select(user => new UserViewModel(user, _profiles)).OrderBy(user => user.Username).AsEnumerable();

            return View(users);
        }

        public IActionResult Enroll(String id)
        {
            var viewModel = new EnrollViewModel
            {
                User = _dbContext.ExtUsers.Find(id),
                Pharases = _verificationService.GetPharses().Select(p => p.Value)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollPost([FromForm] String id, [FromForm] IFormFile voice)
        {
            var wav = await voice.Upload();
            var results = _profiles.Enroll(id, wav);

            return results.Error != null
                ? Json(new { Message = $"({results.Error.Code}) {results.Error.Message}"})
                : Json(new { Message = $"{results.EnrollmentStatus} with {results.EnrollmentsCount} count(s), still remaining {results.RemainingEnrollments} count(s)." });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] String email, [FromForm] String username)
        {
            var result = String.Empty;
            var error = String.Empty;

            try
            {
                var id = _profiles.Create();
                var userObj = new User
                {
                    Email = email,
                    Username = username,
                    ProfileId = id.VerificationProfileId
                };

                _dbContext.ExtUsers.Add(userObj);
                await _dbContext.SaveChangesAsync();

                result = "success";
            }
            catch (Exception ex)
            {
                result = "success";
                error = ex.Message;
            }

            return RedirectToAction(nameof(Index), new { result, error });
        }

        public async Task<IActionResult> Delete(String id)
        {
            var user = _dbContext.ExtUsers.Find(id);
            _dbContext.ExtUsers.Remove(user);
            await _dbContext.SaveChangesAsync();

            _profiles.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Reset(String id)
        {
            _profiles.ResetEnrollments(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
