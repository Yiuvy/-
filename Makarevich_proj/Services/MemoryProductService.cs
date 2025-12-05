using Makarevich_proj.Services.contracts_interfaces_;
using Makarevich_sol_Domain.Entities;
using Makarevich_sol_Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Makarevich_proj.Services
{
    public class MemoryProductService : IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _config;
        List<Doctor> _doctors;
        List<Clinic> _clinics;

        public MemoryProductService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _clinics = categoryService.GetCategoryListAsync().Result.Data;
            _config = config;
            SetupData();
        }

        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _doctors = new List<Doctor>
            {
                new Doctor {
                    Id = 1,
                    Name = "Глеб",
                    Surname = "Романенко",
                    AmountOfPatients = 5,
                    Image = "/images/17.jpg",
                    Specialization="Интерн",
                    IdClinic = _clinics.Find(c => c.IdNormalizedName.Equals("Lode")).Id
                },

                new Doctor {
                    Id = 2,
                    Name = "Анастасия",
                    Surname = "Кисегач",
                    AmountOfPatients = 2,
                    Image = "/images/11.jpg",
                    Specialization="Главный врач",
                    IdClinic = _clinics.Find(c => c.IdNormalizedName.Equals("Lode")).Id
                },
                new Doctor {
                    Id = 3,
                    Name = "Андрей",
                    Surname = "Быков",
                    AmountOfPatients = 78,
                    Image = "/images/13.jpg",
                    Specialization="Заведующий терапевтическим отделением",
                    IdClinic = _clinics.Find(c => c.IdNormalizedName.Equals("Lode")).Id
                },
                new Doctor {
                    Id = 4,
                    Name = "Иван",
                    Surname = "Купитман",
                    AmountOfPatients = 12,
                    Image = "/images/16.jpg",
                    Specialization="Венеролог",
                    IdClinic = _clinics.Find(c => c.IdNormalizedName.Equals("Kravira")).Id
                }
            };
        }


        public Task<ResponseData<Doctor>> CreateProductAsync(Doctor product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ListModel<Doctor>>> GetProductListAsync(string? ClinicNormalizedName, int pageNo = 1) //передаём номер страницы (По умолчанию 1)
        {

            // Создать объект результата
            var result = new ResponseData<ListModel<Doctor>>();
            // Id категории для фильрации
            int? clinicId = null;

            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName

            if (ClinicNormalizedName != null)
                clinicId = _clinics
                .Find(c => c.IdNormalizedName.Equals(ClinicNormalizedName))
                ?.Id;


            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _doctors
            .Where(d => clinicId == null || d.IdClinic.Equals(clinicId))?
            .ToList();


            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

            // получить данные страницы
            var listData = new ListModel<Doctor>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            // поместить данные в объект результата
            result.Data = listData;

            // поместить данные в объект результата
           // result.Data = new ListModel<Doctor>() { Items = data };

            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }

            return Task.FromResult(result);
        }

        public Task UpdateProductAsync(int id, Doctor product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

    }
}

//var model = new ListModel<Doctor>() { Items = _doctors };
//var result = new ResponseData<ListModel<Doctor>>()

//{
//    Data = model
//};