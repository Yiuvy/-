using Makarevich_proj.Extensions;
using Makarevich_proj.Services.contracts_interfaces_;
using Makarevich_sol_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Makarevich_proj.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private Cart _cart;


        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: CartController 
        public IActionResult Index() //выводим список того,что есть в корзине
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new(); //поскольку это контролеер , доступен HttpContext и в нём Session. ИСпользуем расширяющий метод, который описали. Ищу корзину, если не было - создаём новый объект корзина. 
            return View(_cart.CartItems);
        }

        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _productService.GetProductByIdAsync(id); //находим объект по ИД
            if (data.Success)
            {
                _cart = HttpContext.Session.Get<Cart>("cart") ?? new(); //находим корзину
                _cart.AddToCart(data.Data);  //вызываем наш метод добавить
                HttpContext.Session.Set<Cart>("cart", _cart);
            }

            return Redirect(returnUrl); //возвращаем на страницу на которой были
        }

        [Route("[controller]/remove/{id:int}")]
        public ActionResult Remove(int id)
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            _cart.RemoveItems(id);
            HttpContext.Session.Set<Cart>("cart", _cart);
            return RedirectToAction("index");
        }


    }
}
