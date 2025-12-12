using Makarevich_proj.Extensions;
using Makarevich_sol_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Makarevich_proj.ViewContollers
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<Cart>("cart"); 
            return View(cart);
        }

    }
}
