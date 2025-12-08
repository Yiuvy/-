using Makarevich_proj.Services.contracts_interfaces_;
using Microsoft.AspNetCore.Mvc;

namespace Makarevich_proj.Controllers
{
    [Route("Catalog")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        // Изменить маршруты, чтобы клиника передавалась как сегмент маршрута
        [HttpGet]
        [Route("")]
        [Route("{clinic}")]
        public async Task<IActionResult> Index(string? clinic, int pageNo = 1)
        {
            // Получить список категорий
            var categoriesResponse = await _categoryService.GetCategoryListAsync();

            if (!categoriesResponse.Success)
                return NotFound(categoriesResponse.ErrorMessage);

            ViewData["clinics"] = categoriesResponse.Data;

            // Определить текущую клинику
            var currentClinic = clinic == null
                ? "Все клиники"
                : categoriesResponse.Data.FirstOrDefault(c => c.IdNormalizedName == clinic)?.Name;
            ViewData["currentClinic"] = currentClinic;

            // Передать сегмент маршрута (clinic) в сервис
            var productResponse = await _productService.GetProductListAsync(clinic, pageNo);
            if (!productResponse.Success)
                ViewData["Error"] = productResponse.ErrorMessage;

            return View(productResponse.Data);
        }
    }
}