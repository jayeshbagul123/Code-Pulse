using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test_1.Models;
using Test_1.Repository;

namespace Test_1.Controllers
{
    public class HomeController : Controller
    {
        public readonly IStudent _std;
        public HomeController(IStudent std)
        {
            _std = std;
        }
        public IActionResult Index()
        {
            var std = _std.GetAll();
            return View(std);
        }
        public IActionResult Create()
        {
            ViewBag.StudentList = _std.GetAllStudentsList();
            return View();
        }

        [HttpPost]   
        
        public IActionResult Create(Students std)
        {
            if (ModelState.IsValid) { 
                _std.AddStudent(std);
                return RedirectToAction("Index");
            }
            return View();
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
