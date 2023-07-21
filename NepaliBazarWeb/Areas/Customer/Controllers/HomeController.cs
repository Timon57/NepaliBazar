using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NepaliBazar.DataAccess.Repository.IRepository;
using NepaliBazar.Models;
using NepaliBazar.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace NepaliBazarWeb.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productlist = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productlist);
        }
        public IActionResult Details(int ProductId)
        {
            ShoppingCart cart = new()
            {
                Count = 1,
                ProductId = ProductId,
                product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == ProductId, includeProperties: "Category")
            };
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.UserId = claim.Value;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u=>
            u.UserId == claim.Value && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                _unitOfWork.ShoppingCart.incrementCount(cartFromDb, shoppingCart.Count);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
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