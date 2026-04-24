using ExamApplication.Business.Abstract;
using ExamApplication.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamApplication.Controllers
{
    [Authorize]
    public class ExamsController : Controller
    {
        private readonly IExamService examService;
        private readonly ISubjectService subjectService;
        private readonly IStudentService studentService;

        public ExamsController(IExamService examService, ISubjectService subjectService, IStudentService studentService)
        {
            this.examService = examService;
            this.subjectService = subjectService;
            this.studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await examService.GetAllExamsAsync();
            if (!result.Success || result.Data == null)
                return View(new List<Exam>());

            return View(result.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await examService.GetExamByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateSelectionsAsync();
            return View(new Exam { ExamDate = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam exam)
        {
            var result = await examService.CreateExamAsync(exam);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Əlavə etmə uğursuz oldu.");
                await PopulateSelectionsAsync();
                return View(exam);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await examService.GetExamByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            await PopulateSelectionsAsync();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exam exam)
        {
            if (id != exam.Id)
                return BadRequest();

            var result = await examService.UpdateExamAsync(exam);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Yeniləmə uğursuz oldu.");
                await PopulateSelectionsAsync();
                return View(exam);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await examService.GetExamByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Exam exam)
        {
            var existing = await examService.GetExamByIdAsync(id);
            if (!existing.Success || existing.Data == null)
                return NotFound();

            var result = await examService.DeleteExamAsync(existing.Data);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Silmə uğursuz oldu.");
                return View(existing.Data);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateSelectionsAsync()
        {
            var subjectsResult = await subjectService.GetAllSubjectsAsync();
            var studentsResult = await studentService.GetAllStudentsAsync();

            var subjects = new List<SelectListItem>();
            if (subjectsResult.Success && subjectsResult.Data != null)
            {
                subjects.AddRange(subjectsResult.Data.Select(s => new SelectListItem
                {
                    Value = s.Code,
                    Text = $"{s.Code} - {s.Name} (Sinif {s.Class})"
                }));
            }

            var students = new List<SelectListItem>();
            if (studentsResult.Success && studentsResult.Data != null)
            {
                students.AddRange(studentsResult.Data.Select(s => new SelectListItem
                {
                    Value = s.Number.ToString(),
                    Text = $"{s.Number} - {s.FirstName} {s.LastName} (Sinif {s.Class})"
                }));
            }

            ViewBag.Subjects = subjects;
            ViewBag.Students = students;
        }
    }
}
