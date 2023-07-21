using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using NepaliBazar.DataAccess.Repository.IRepository;
using NepaliBazar.Models;
using NepaliBazar.Models.ViewModels;
using System.Security.Claims;

namespace NepaliBazarWeb.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private UserManager<IdentityUser> _userManager;

        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value, includeProperties:"product"),
                OrderHeader = new()
            };

            foreach(var cart in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Count * cart.product.actualPrice;
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Plus(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            unitOfWork.ShoppingCart.incrementCount(cart, 1);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                unitOfWork.ShoppingCart.decrementCount(cart, 1);
            }
            
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            unitOfWork.ShoppingCart.Remove(cart);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value, includeProperties: "product"),
                OrderHeader = new()
            };
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Count * cart.product.actualPrice;
            }
            return View(ShoppingCartVM);


        }
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST(ShoppingCartVM ShoppingCartVM) 
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.ListCart = unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value, includeProperties: "product");
            ShoppingCartVM.OrderHeader.PaymentStatus = "Pending";
            ShoppingCartVM.OrderHeader.OrderStatus = "Pending";
            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.UserId = claim.Value;

            unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            unitOfWork.Save();
            foreach(var cart in ShoppingCartVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.product.actualPrice,
                    Count = cart.Count
                };
                unitOfWork.OrderDetail.Add(orderDetail);
                unitOfWork.Save();
            }

            unitOfWork.ShoppingCart.RemoveRange(ShoppingCartVM.ListCart);
            unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }


    }
}
