using ExamApplication.Business.Abstract;
using ExamApplication.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamApplication.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await studentService.GetAllStudentsAsync();
            if (!result.Success || result.Data == null)
                return View(new List<Student>());

            return View(result.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await studentService.GetStudentByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            var result = await studentService.CreateStudentAsync(student);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Əlavə etmə uğursuz oldu.");
                return View(student);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await studentService.GetStudentByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest();

            var result = await studentService.UpdateStudentAsync(student);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Yeniləmə uğursuz oldu.");
                return View(student);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await studentService.GetStudentByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Student student)
        {
            var existing = await studentService.GetStudentByIdAsync(id);
            if (!existing.Success || existing.Data == null)
                return NotFound();

            var result = await studentService.DeleteStudentAsync(existing.Data);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Silmə uğursuz oldu.");
                return View(existing.Data);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
