using System.Collections.Generic;
using System.Diagnostics;
using Makarevich_proj.Data;
using Makarevich_proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyModel;

namespace Makarevich_proj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly List<ListDemo> _listElements;

        //Конструктор    Получает логгер через внедрение зависимостей(Dependency Injection).
        //Хранит его в приватной переменной _logger для дальнейшего использования.

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _listElements = new List<ListDemo>()
            {
                new ListDemo{ Id=1, Name="Элемент 1 списка"},
                new ListDemo{ Id=2, Name="Элемент 2 списка"},
                new ListDemo{ Id=3, Name="Элемент 3 списка"},
                new ListDemo{ Id=4, Name="Элемент 4 списка"},
                new ListDemo{ Id=5, Name="Элемент 5 списка"},
            };
        }

          
           // Возвращает представление Index.cshtml.
           // Обычно это главная страница сайта.
           public IActionResult Index()
           {
            ViewData["text"] = "Лабораторная работа №8";
            SelectList data = new SelectList(_listElements, "Id", "Name");
               return View(data);
           }


        public class ListDemo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        /* Возвращает страницу Privacy.cshtml.
        Обычно это страница с политикой конфиденциальности

        public IActionResult Privacy()
        {
            return View();
        }
        */

        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //Используется для отображения ошибок и логирования.
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        */
    }
}
