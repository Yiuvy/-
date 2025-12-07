using Makarevich_proj.Services.contracts_interfaces_;
using Makarevich_sol_Domain.Entities;
using Makarevich_sol_Domain.Models;

namespace Makarevich_proj.Services
{
    public class ApiCategoryService(HttpClient httpClient) : ICategoryService
    {
        public async Task<ResponseData<List<Clinic>>> GetCategoryListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<List<Clinic>>>();
            };

            var response = new ResponseData<List<Clinic>>
            { Success = false, ErrorMessage = "Ошибка чтения API" };
            return response;
        }

        //Task<ResponseData<List<Clinic>>> ICategoryService.GetCategoryListAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }

}
