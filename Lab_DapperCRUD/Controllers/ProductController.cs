using DAL.Entities;
using DAL.Interface;

using Microsoft.AspNetCore.Mvc;

namespace Lab_DapperCRUD.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _productRepo; 
        public ProductController(IProductRepository productRepo) 
        {
            _productRepo = productRepo; 
        }
        public IActionResult Index() 
        {
            var products = _productRepo.GetAllProducts();
            return View(products); 
        }
        public IActionResult Create() 
        {
            ViewBag.Categories = _productRepo.GetCategories(); 
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            ModelState.Remove("ProductId"); //It is optional field for create, so removed it
            if (ModelState.IsValid) 
            {
                _productRepo.InsertProduct(model); 
                return RedirectToAction("Index"); // product Home page
            } 
            ViewBag.Categories = _productRepo.GetCategories(); 
            return View();
        }
        public IActionResult Edit(int id) 
        {
            ViewBag.Categories = _productRepo.GetCategories(); 
            Product model = _productRepo.GetSingleProduct(id); 
            return View("Create", model); //Call Create View and pass model
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _productRepo.UpdateProduct(model); 
                return RedirectToAction("Index"); // product Home page
            } 
            ViewBag.Categories = _productRepo.GetCategories();
            return View(); 
        }
        public IActionResult Delete(int id)
        {
            _productRepo.DeleteProduct(id); 
            return RedirectToAction("Index"); // Go to the Listing Page
        }
    }
}

