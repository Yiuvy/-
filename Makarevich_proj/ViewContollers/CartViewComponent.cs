using Microsoft.AspNetCore.Mvc;

namespace Makarevich_proj.ViewContollers
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke() { return View(); }
    }
}
