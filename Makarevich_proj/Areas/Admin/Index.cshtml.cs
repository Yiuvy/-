using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Makarevich_proj.Data;
using Makarevich_proj.Services.contracts_interfaces_;
using Makarevich_sol_Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Makarevich_proj.Areas.Admin
{
    public class IndexModel : PageModel
    {
        private readonly Makarevich_proj.Data.AppDBContext _context;
        private readonly IProductService _productService;
        public IndexModel( IProductService productService)
        {
            _productService = productService;
        }

        public IList<Doctor> Doctor { get;set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _productService.GetProductListAsync(null, pageNo.Value);
            if (response.Success)
            {
                Doctor = response.Data.Items; 
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }

    }
}
