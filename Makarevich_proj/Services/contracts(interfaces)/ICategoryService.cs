using Makarevich_sol_Domain.Entities;

using Makarevich_sol_Domain.Models;

namespace Makarevich_proj.Services.contracts_interfaces_
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Clinic>>> GetCategoryListAsync();
    }

}
