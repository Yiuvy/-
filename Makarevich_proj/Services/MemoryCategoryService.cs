using Makarevich_proj.Services.contracts_interfaces_;
using Makarevich_sol_Domain.Entities;
using Makarevich_sol_Domain.Models;

namespace Makarevich_proj.Services
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Clinic>>> GetCategoryListAsync()
        {
            var clinics = new List<Clinic>
            { new Clinic {Id=1,    Name="ООО Кравира",IdNormalizedName="Kravira"},
                new Clinic {Id=2, Name="ЗАО Лодэ",IdNormalizedName="Lode"}
            };
            var result = new ResponseData<List<Clinic>>();
            result.Data = clinics;
            return Task.FromResult(result);
        }

        //Task<ResponseData<List<Clinic>>> ICategoryService.GetCategoryListAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }

}
