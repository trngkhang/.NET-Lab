using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
	public class StudentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Details()
		{
			return View("Details");
		}
		public ActionResult Manage(StudentInfo model, String command)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(),
			"wwwroot", "Student.txt");
			if (command == "Lưu")
			{
				String[] lines = { model.Id, model.Name, model.Marks.ToString() };
				System.IO.File.WriteAllLines(path, lines);
				ViewBag.Message = "Đã ghi vào file !";
			}
			else if (command == "Mở")
			{
				String[] lines = System.IO.File.ReadAllLines(path);
				ViewBag.Id = lines[0];
				ViewBag.Name = lines[1];
				ViewBag.Marks = Convert.ToDouble(lines[2]);
				ViewBag.Message = "Đã đọc từ file !";
			}
			return View("Details");
		}
	}
}
