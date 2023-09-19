using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IFileProvider fileProvider;
        public FileUploadController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult UploadFilesView()
		{
			return View("UploadFilesView");
		}
		public IActionResult ListFiles()
		{
			var model = new FilesViewModel();
			foreach (var item in
			this.fileProvider.GetDirectoryContents("UploadFiles"))
			{
				model.Files.Add(
				new FileDetails
				{
					Name = item.Name,
					Path = item.PhysicalPath
				});
			}
			return View("ListFiles", model);
		}
		[HttpPost]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return Content("file not selected");
			var fileName = Path.GetFileName(file.FileName);
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","UploadFiles", fileName);//thay đỗi so với lab
			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return RedirectToAction("ListFiles");
		}
		[HttpPost]
		public async Task<IActionResult> UploadFiles(List<IFormFile> files)
		{
			if (files == null || files.Count == 0)
				return Content("files not selected");
			foreach (var file in files)
			{
				var fileName = Path.GetFileName(file.FileName);
				var path = Path.Combine(
				Directory.GetCurrentDirectory(), "wwwroot",
				"UploadFiles", fileName);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
			}
			return RedirectToAction("ListFiles");
		}
		[HttpPost]
		public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
		{
			if (model == null || model.FileToUpload == null || model.FileToUpload.Length == 0)
				return Content("file not selected");
			var fileName = Path.GetFileName(model.FileToUpload.FileName);
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles", fileName);
			using (var stream = new FileStream(path, FileMode.Create))
			{
				await model.FileToUpload.CopyToAsync(stream);
			}
			return RedirectToAction("ListFiles");
		}
	}
}
