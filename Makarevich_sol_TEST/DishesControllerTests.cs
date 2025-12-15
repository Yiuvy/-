using System.Data.Common;
using Makarevich_sol_API.Controllers;
using Makarevich_sol_API.Data;
using Makarevich_sol_Domain.Entities;
using Makarevich_sol_Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;


namespace Makarevich_sol_TEST
{
    public class DishesControllerTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<AppDBContext> _contextOptions;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _config;
        public DishesControllerTests()
        {
            _environment = Substitute.For<IWebHostEnvironment>();

            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlite(_connection)
            .Options;


            // Create the schema and seed some data
            using var context = new AppDBContext(_contextOptions);
            context.Database.EnsureCreated();
            var categories = new Clinic[]
            {
                new Clinic {Name="", IdNormalizedName="Merci", Adress="" },
                new Clinic {Name="", IdNormalizedName="MedAvenue",  Adress=""}
            };
            
            context.Clinics.AddRange(categories);
            context.SaveChanges();

            var dishes = new List<Doctor>
            {
                new Doctor {
                    Name="", Surname="",AmountOfPatients=0,
                   IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("Merci")).Id },
                                 
                new Doctor {
                    Name="",Surname="", AmountOfPatients=0,
                   IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("MedAvenue")).Id },

                new Doctor {
                    Name="",Surname="",AmountOfPatients=0,
                   IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("MedAvenue")).Id },

                new Doctor {
                    Name="",Surname="",AmountOfPatients=0,
                   IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("Merci")).Id },

                 new Doctor {
                    Name="",Surname="",AmountOfPatients=0,
                   IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("MedAvenue")).Id },
            };
            context.AddRange(dishes);
            context.SaveChanges();
        }
        public void Dispose() => _connection?.Dispose();
        AppDBContext CreateContext() => new AppDBContext(_contextOptions);

        //// Проверка фильтра по категории

        //[Fact]
        //public async Task ControllerFilterCategory()
        //{
        //    // arrange
        //    using var context = CreateContext();
        //    var category = context.Clinics.First();
        //    var controller = new DoctorsController(context, _environment, _config);


        //    // act
        //    var response = await controller.GetDoctor(category.Id);


        //    Doctor responseData = response.Value;
        //    var dishesList = responseData; // полученный список объектов

        //    //assert
        //    Assert.True(dishesList.All(d => d.IdClinic == category.Id));
        //}










        // Проверка подсчета количества страниц
        // Первый параметр - размер страницы
        // Второй параметр - ожидаемое количество страниц (при условии, что всего объектов 5)
        [Theory]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        public async Task ControllerReturnsCorrectPagesCount(int size, int qty)
        {
            using var context = CreateContext();
            var controller = new DoctorsController(context, _environment, _config);


            // act
            var response = await controller.GetDoctors(null, 1, size);
            ResponseData<ListModel<Doctor>> responseData = response.Value;
            var totalPages = responseData.Data.TotalPages; // полученное количество


            //assert
            Assert.Equal(qty, totalPages); // количество страниц совпадает
        }



        [Fact]
        public async Task ControllerReturnsCorrectPage()
        {
            using var context = CreateContext();
            var controller = new DoctorsController(context, _environment,  _config);
            // При размере страницы 3 и общем количестве объектов 5
            // на 2-й странице должно быть 2 объекта
            int itemsInPage = 2;
            // Первый объект на второй странице
            Doctor firstItem = context.Doctors.ToArray()[3];


            // act
            // Получить данные 2-й страницы
            var response = await controller.GetDoctors(null, 2);
            ResponseData<ListModel<Doctor>> responseData = response.Value;
            var dishesList = responseData.Data.Items; // полученный список объектов
            var currentPage = responseData.Data.CurrentPage; // полученный номер текущей  страницы


            //assert
            Assert.Equal(2, currentPage);// номер страницы совпадает
            Assert.Equal(2, dishesList.Count); // количество объектов на странице равно  2
            Assert.Equal(firstItem.Id, dishesList[0].Id); // 1-й объект в списке     правильный
        }
    }



}


       // [Theory]
       // [InlineData(2, 3)]
       //// [InlineData(3, 2)]

       // public async Task ControllerReturnsCorrectPagesCount(int size, int qty)
       // {
       //     using var context = CreateContext();
       //     var controller = new DoctorsController(context, _environment, _config);

       //     // act
       //     var response = await controller.GetDoctors(null, 1, size);

       //     // привести к типу OkObjectResult
       //     var okResult = Assert.IsType<OkObjectResult>(response);

       //     // извлечь содержимое
       //     var responseData = Assert.IsType<ResponseData<ListModel<Doctor>>>(okResult.Value);
       //     var totalPages = responseData.Data.TotalPages;

       //     //assert
       //     Assert.Equal(qty, totalPages); // количество страниц совпадает
       // }