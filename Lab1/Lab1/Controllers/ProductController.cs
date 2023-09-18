using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
	public class ProductController : Controller
	{
		static List<Product> products = new List<Product>()
		{
			new Product { ID = 101, Name = "IPad 2018", Price = 499 }
		};

		public IActionResult Index()
		{
			return View();
		}
		
		public IActionResult ShowAll()
		{
			return View("ShowAll", products);
		}
		public IActionResult Create()
		{
			return View("Create");
		}
		public IActionResult CreateProduct([Bind("ID", "Name", "Price")] Product product)
		{
			//thêm vào danh sách
			products.Add(product);
			//gọi hiển thị danh sách
			return RedirectToAction("ShowAll");
		}
		public IActionResult Edit()
		{
			return View("Edit");
		}

		public IActionResult EditProduct(int id, [Bind("ID", "Name", "Price")] Product product)
		{
			//Sửa vào danh sách
			Product p = products.SingleOrDefault(q => q.ID == id);
			if (p != null) //tìm thấy
			{
				p.Name = product.Name;
				p.Price = product.Price;
			}
			//gọi hiển thị danh sách
			return RedirectToAction("ShowAll");
		}
		public IActionResult Delete(int? id)
		{
            Product p = products.SingleOrDefault(q => q.ID == id);
            if (p != null) //tìm thấy
            {
                products.Remove(p);
            }
            return RedirectToAction("ShowAll");
		}
		public IActionResult DeleteProduct(int id)
		{
			Console.WriteLine(id);
			//tìm Product cần xóa (dùng LINQ)
			Product p = products.SingleOrDefault(q => q.ID == id);
			if (p != null) //tìm thấy
			{
				products.Remove(p);
			}
			//gọi hiển thị danh sách
			return RedirectToAction("ShowAll");
		}
	}
}
