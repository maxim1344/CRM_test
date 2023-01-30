using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CRM_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

		public HomeController(ApplicationContext context)
        {           
            _context = context;
			_context.Database.EnsureCreated();

            if (!context.Clients.Any())
            {          
                Client client1 = new Client { Name = "А. А. Руднев", Comment = "Оплатил вперед" };
                Client client2 = new Client { Name = "И. М. Соколова", Comment = "Постоянный клиент" };
                Client client3 = new Client { Name = "Е. С. Наумов", Comment = "Скиндка 5%" };
                Client client4 = new Client { Name = "С. М. Одинцова", Comment = "Скидка 10%" };
                Client client5 = new Client { Name = "И. Я. Чернов", Comment = "Льготное обслуживание" };				

				Project project1 = new Project { Name = "Сбор анализов", Description = "Сбор анализов по нескольким категориям", Status = ProjectStatus.Заявка, Client = client1 };
				Project project2 = new Project { Name = "Узи", Description = "Узи сердца", Status = ProjectStatus.Подготовка, Client = client2 };
				Project project3 = new Project { Name = "Пакет услуг 1", Description = "Расширенный анализ крови", Status = ProjectStatus.Переговоры, Client = client3 };
				Project project4 = new Project { Name = "Пакет услуг 2", Description = "Расширенный анализ крови (экспресс)", Status = ProjectStatus.Доставка, Client = client4 };
				Project project5 = new Project { Name = "Пакет услуг 3", Description = "Многоцелевое обследование организма", Status = ProjectStatus.Оплата, Client = client5 };

				context.Clients.AddRange(client1, client2, client3, client4, client5);
				context.Projects.AddRange(project1, project2, project3, project4, project5);

				context.SaveChanges();
            }

        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "ProjectManagement");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

	}
}