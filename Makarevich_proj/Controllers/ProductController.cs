using Makarevich_proj.Services.contracts_interfaces_;
using Microsoft.AspNetCore.Mvc;

namespace Makarevich_proj.Controllers
{
    public class ProductController(ICategoryService categoryService, IProductService productService) : Controller
    {
        [Route("Catalog")]
        [Route("Catalog/{clinic}")]
        public async Task<IActionResult> Index(string? clinic, int pageNo = 1)
        {

            // получить список категорий
            var categoriesResponse = await categoryService.GetCategoryListAsync();

            // если список не получен, вернуть код 404 
            if (!categoriesResponse.Success)
                return NotFound(categoriesResponse.ErrorMessage);

            // передать список категорий во ViewData
            ViewData["clinics"] = categoriesResponse.Data;


            // передать во ViewData имя текущей категории
            var currentClinic = clinic == null
                ? "Все клиники"
                : categoriesResponse.Data.FirstOrDefault(c => c.IdNormalizedName == clinic)?.Name;
            ViewData["currentClinic"] = currentClinic;

            var productResponse =
            await productService.GetProductListAsync(clinic, pageNo);
            if (!productResponse.Success)
                ViewData["Error"] = productResponse.ErrorMessage;
            return View(productResponse.Data);
        }



    }

}
