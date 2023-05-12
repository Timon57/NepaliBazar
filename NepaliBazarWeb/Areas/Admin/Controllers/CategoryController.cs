using Microsoft.AspNetCore.Mvc;
using NepaliBazar.DataAccess.Repository.IRepository;
using NepaliBazar.Models;

namespace NepaliBazarWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> CategoryList  = _unitOfWork.Category.GetAll();
            return View(CategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = obj.Name + "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
