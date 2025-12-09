using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Makarevich_proj.Data;
using Makarevich_sol_Domain.Entities;
using Makarevich_proj.Services.contracts_interfaces_;

namespace Makarevich_proj.Areas.Admin
{
    public class CreateModel(ICategoryService categoryService, IProductService productService) : PageModel
    {
        private readonly Makarevich_proj.Data.AppDBContext _context;
        private readonly IProductService _productService;

        //public CreateModel(IProductService productService)
        //{
        //    _productService = productService;
        //}

        public async Task<IActionResult> OnGet()
        {
            var clinicListData = await categoryService.GetCategoryListAsync();
            ViewData["IdClinic"] = new SelectList(clinicListData.Data, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Doctor Doctor { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var response = await _productService.CreateProductAsync(Doctor, null);

            await productService.CreateProductAsync(Doctor, Image);

            //if (!response.Success)
            //{
            //    // Обработка ошибки (например, установка ModelState ошибки)
            //    ModelState.AddModelError(string.Empty, response.ErrorMessage);
            //    return Page();
            //}


            return RedirectToPage("./Index");
        }
    }
}
