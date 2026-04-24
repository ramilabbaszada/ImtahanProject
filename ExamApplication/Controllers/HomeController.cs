using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace ExamApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionFeature?.Error is ValidationException validationException)
            {
                ViewBag.ValidationErrors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else if (exceptionFeature?.Error != null)
            {
                ViewBag.ErrorMessage = exceptionFeature.Error.Message;
            }

            return View();
        }
    }
}
