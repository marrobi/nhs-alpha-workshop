using Microsoft.AspNetCore.Mvc;

namespace ImmForm.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }
}
