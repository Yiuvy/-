using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makarevich_proj.Controllers;
using Makarevich_proj.Services.contracts_interfaces_;
using Makarevich_sol_Domain.Entities;
using Makarevich_sol_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Makarevich_sol_TEST
{
    public class ProductControllerTests
    {
        IProductService _productService;
        ICategoryService _categoryService;
        public ProductControllerTests()
        {
            SetupData();
        }
        // Список категорий сохраняется во ViewData

        [Fact]
        public async void IndexPutsCategoriesToViewData()
        {
            //arrange
            var controller = new ProductController(_categoryService, _productService);
            //act
            var response = await controller.Index(null);
            //assert
            var view = Assert.IsType<ViewResult>(response);
            var categories = Assert.IsType<List<Clinic>>(view.ViewData["clinics"]);
            Assert.Equal(2, categories.Count);
            Assert.Equal("Все клиники", view.ViewData["currentClinic"]);
        }

        // Имя текущей категории сохраняется во ViewData
        //
        [Fact]
        public async void IndexSetsCorrectCurrentClinic()
        {
            //arrange
            var categories = await _categoryService.GetCategoryListAsync();
            var currentCategory = categories.Data[0];
            var controller = new ProductController(_categoryService, _productService);
            //act
            var response = await controller.Index(currentCategory.IdNormalizedName);
            //assert
            var view = Assert.IsType<ViewResult>(response);

            Assert.Equal(currentCategory.Name, view.ViewData["currentClinic"]);
        }


        // В случае ошибки возвращается NotFoundObjectResult
        //
        [Fact]
        public async void IndexReturnsNotFound()
        {
            //arrange
            string errorMessage = "Test error";
            var categoriesResponse = new ResponseData<List<Clinic>>();
            categoriesResponse.Success = false; categoriesResponse.ErrorMessage = errorMessage;
            _categoryService.GetCategoryListAsync().Returns(Task.FromResult(categoriesResponse));
            var controller = new ProductController(_categoryService, _productService);
            //act
            var response = await controller.Index(null);
            //assert
            var result = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(errorMessage, result.Value.ToString());

        }
        // Настройка имитации ICategoryService и IProductService
        void SetupData()
        {
            _categoryService = Substitute.For<ICategoryService>();
            var categoriesResponse = new ResponseData<List<Clinic>>();
            categoriesResponse.Data = new List<Clinic>
            {
                new Clinic {Id=1,    Name="ООО Кравира",IdNormalizedName="Kravira"},
                new Clinic {Id=2, Name="ЗАО Лодэ",IdNormalizedName="Lode"}  
            };


            _categoryService.GetCategoryListAsync().Returns(Task.FromResult(categoriesResponse));
            _productService = Substitute.For<IProductService>();
            
            var dishes = new List<Doctor>
            {
                new Doctor {Id = 1 },
                new Doctor { Id = 2 },
                new Doctor { Id = 3 },
                new Doctor { Id = 4 },
                new Doctor { Id = 5 }
            };

            var productResponse = new ResponseData<ListModel<Doctor>>();
            productResponse.Data = new ListModel<Doctor> { Items = dishes };
            _productService.GetProductListAsync(Arg.Any<string?>(), Arg.Any<int>()).Returns(productResponse);
        }
    }





}
