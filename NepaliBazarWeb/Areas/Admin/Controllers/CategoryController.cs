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
        public IActionResult Edit(int? id)
        {
            if (id == 0|| id == null)
            {
                return NotFound();
            }
            var cat = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }
        [HttpPost]
        public IActionResult Edit(Category cat) 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(cat);
                _unitOfWork.Save();
                TempData["Success"] = cat.Name + "Category created successfully";
                return RedirectToAction("Index");

            }
            return View(cat);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var cat = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);

        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id) 
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var cat = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(cat);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
        
}
}
