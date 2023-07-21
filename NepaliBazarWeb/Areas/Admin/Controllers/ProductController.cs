using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NepaliBazar.DataAccess.Repository.IRepository;
using NepaliBazar.Models;
using NepaliBazar.Models.ViewModels;
using System.Configuration;
using System.Reflection.Metadata;
using System;

namespace NepaliBazarWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;    
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<ProductVM> products = _unitOfWork.Product.GetAll().Select(p => new ProductVM
            {
                Product = p,
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            }).ToList();

            return View(products);
        }
        public IActionResult Create()
        {
            ProductVM productVm = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            return View(productVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM pro,IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootpath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootpath, @"Images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    pro.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                pro.Product.actualPrice = pro.Product.Price-pro.Product.DiscountedPrice;
                _unitOfWork.Product.Add(pro.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(pro);
        }

        public IActionResult Edit(int id)
        {
            //var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //ProductVM productVm = new ProductVM
            //{
            //    Product = product,
            //    CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            //    {
            //        Text = i.Name,
            //        Value = i.Id.ToString()
            //    }),
            //    ImageUrl = product.ImageUrl // Add this line to populate the ImageUrl property
            //};
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM pro, IFormFile? file,int id)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                string wwwRootpath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootpath, @"Images\products");
                    var extension = Path.GetExtension(file.FileName);

                    //Console.WriteLine(pro.ImageUrl);
                    //Console.WriteLine(pro.Product.Name);
                    //if (pro.ImageUrl != null)
                    //{
                    //    var oldImagePath = Path.Combine(wwwRootpath, pro.Product.ImageUrl.TrimStart('\\'));
                    //    if (System.IO.File.Exists(oldImagePath))
                    //    {
                    //        System.IO.File.Delete(oldImagePath);
                    //    }
                    //}


                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    pro.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                // Update other properties of the product
                //product.Name = pro.Product.Name;
                //product.Description = pro.Product.Description;
                //product.CategoryId = pro.Product.CategoryId;
                //product.Weight = pro.Product.Weight;

                _unitOfWork.Product.Update(pro.Product);
                _unitOfWork.Save();

                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }

            return View(pro);
        }
        public IActionResult Delete(int id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(x=>x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

        public double ActualPrice(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            var actualPrice = product.Price - product.DiscountedPrice;
            return (actualPrice);
        }

    }
}
