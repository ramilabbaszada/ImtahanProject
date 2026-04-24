using ExamApplication.Business.Abstract;
using ExamApplication.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamApplication.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await subjectService.GetAllSubjectsAsync();
            if (!result.Success || result.Data == null)
                return View(new List<Subject>());

            return View(result.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await subjectService.GetSubjectByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Subject());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            var result = await subjectService.CreateSubjectAsync(subject);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Əlavə etmə uğursuz oldu.");
                return View(subject);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await subjectService.GetSubjectByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subject subject)
        {
            if (id != subject.Id)
                return BadRequest();

            var result = await subjectService.UpdateSubjectAsync(subject);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Yeniləmə uğursuz oldu.");
                return View(subject);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await subjectService.GetSubjectByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Subject subject)
        {
            var existing = await subjectService.GetSubjectByIdAsync(id);
            if (!existing.Success || existing.Data == null)
                return NotFound();

            var result = await subjectService.DeleteSubjectAsync(existing.Data);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Silmə uğursuz oldu.");
                return View(existing.Data);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
