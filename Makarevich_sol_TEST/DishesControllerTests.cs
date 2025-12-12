using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Makarevich_proj.Controllers;
using Makarevich_sol_Domain.Entities;
using Makarevich_sol_Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Makarevich_sol_TEST
{
    public class DishesControllerTests : IDisposable
    {
        //    private readonly DbConnection _connection;
        //    private readonly DbContextOptions<ProductDbContext> _contextOptions;
        //    private readonly IWebHostEnvironment _environment;
        //    public DishesControllerTests()
        //    {
        //        _environment = Substitute.For<IWebHostEnvironment>();

        //        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
        //        // at the end of the test (see Dispose below).
        //        _connection = new SqliteConnection("Filename=:memory:");
        //        _connection.Open();

        //        // These options will be used by the context instances in this test suite, including the connection opened above.
        //        _contextOptions = new DbContextOptionsBuilder<ProductDbContext>()
        //        .UseSqlite(_connection)
        //        .Options;

        //        // Create the schema and seed some data
        //        using var context = new ProductDbContext(_contextOptions);
        //        context.Database.EnsureCreated();
        //        var categories = new Clinic[]
        //        {
        //            new Clinic {Name="", IdNormalizedName="Merci"},
        //            new Clinic {Name="", IdNormalizedName="MedAvenue"}
        //        };

        //        context.Categories.AddRange(categories);
        //        context.SaveChanges();

        //        var dishes = new List<Doctor>
        //        {
        //            new Doctor {
        //                Name="", Surname="",AmountOfPatients=0,
        //               IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("Merci")).Id },

        //            new Doctor {
        //                Name="",Surname="", AmountOfPatients=0,
        //               IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("MedAvenue")).Id },

        //            new Doctor {
        //                Name="",Surname="",AmountOfPatients=0,
        //               IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("MedAvenue")).Id },

        //            new Doctor {
        //                Name="",Surname="",AmountOfPatients=0,
        //               IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("Merci")).Id },

        //             new Doctor {
        //                Name="",Surname="",AmountOfPatients=0,
        //               IdClinic=categories.FirstOrDefault(c=>c.IdNormalizedName.Equals("MedAvenue")).Id },
        //        };
        //        context.AddRange(dishes);
        //        context.SaveChanges();
        //    }
        //    public void Dispose() => _connection?.Dispose();
        //    ProductDbContext CreateContext() => new ProductDbContext(_contextOptions);

        //    // Проверка фильтра по категории [Fact]
        //    public async void ControllerFiltersCategory()
        //    {
        //        // arrange
        //        using var context = CreateContext();
        //        var category = context.Categories.First();
        //        var controller = new DishesController(context, _environment);
        //        // act
        //        var response = await controller.GetDoctor(category.NormalizedName); 
        //        ResponseData<ListModel<Doctor>> responseData = response.Value;
        //        var dishesList = responseData.Data.Items; // полученный список объектов

        //        //assert
        //        Assert.True(dishesList.All(d => d.IdClinic == category.Id));
        //    }

        //    // Проверка подсчета количества страниц
        //    // Первый параметр - размер страницы
        //    // Второй параметр - ожидаемое количество страниц (при условии, что всего объектов 5)
        //    [Theory]
        //    [InlineData(2, 3)]
        //    [InlineData(3, 2)]
        //    public async void ControllerReturnsCorrectPagesCount(int size, int qty)
        //    {
        //        using var context = CreateContext();
        //        var controller = new DishesController(context, _environment);


        //        // act
        //        var response = await controller.GetDishes(null, 1, size);
        //        ResponseData<ListModel<Doctor>> responseData = response.Value;
        //        var totalPages = responseData.Data.TotalPages; // полученное количество


        //        //assert
        //        Assert.Equal(qty, totalPages); // количество страниц совпадает
        //    }

        //    [Fact]
        //    public async void ControllerReturnsCorrectPage()
        //    {
        //        using var context = CreateContext();
        //        var controller = new DishesController(context, _environment);
        //        // При размере страницы 3 и общем количестве объектов 5
        //        // на 2-й странице должно быть 2 объекта
        //        int itemsInPage = 2;
        //        // Первый объект на второй странице
        //        Doctor firstItem = context.Dishes.ToArray()[3];


        //        // act
        //        // Получить данные 2-й страницы
        //        var response = await controller.GetDishes(null, 2);
        //        ResponseData<ListModel<o>> responseData = response.Value;
        //        var dishesList = responseData.Data.Items; // полученный список объектов
        //        var currentPage = responseData.Data.CurrentPage; // полученный номер текущей  страницы


        //        //assert
        //        Assert.Equal(2, currentPage);// номер страницы совпадает
        //        Assert.Equal(2, dishesList.Count); // количество объектов на странице равно  2
        //    Assert.Equal(firstItem.Id, dishesList[0].Id); // 1-й объект в списке     правильный
        //    }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }



}
