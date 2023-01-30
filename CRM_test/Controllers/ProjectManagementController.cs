using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using CRM_test.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM_test.Controllers
{
	public class ProjectManagementController : Controller
	{
        private readonly ApplicationContext _context;
        private readonly IProjectRepository _projectRepository;
		private readonly INotificationRepository _notificationRepository;

		public ProjectManagementController(ApplicationContext context, IProjectRepository projectRepository, INotificationRepository notificationRepository)
		{
            _context = context;
            _projectRepository = projectRepository;
			_notificationRepository = notificationRepository;
		}
		public async Task<IActionResult> Index()
		{
			IEnumerable<Project> projects = await _projectRepository.GetAll();


			IEnumerable<Notification> notifications = await _notificationRepository.GetAll();
			Dictionary<int, DateTime> dict = new Dictionary<int, DateTime>();
			List<DateTime> dtList = new List<DateTime>();
			foreach (var item in notifications)
			{
				DateTime tempDt = item.DateOfNotification.AddHours(-1);
				if (DateTime.Now > tempDt)
				{
					dict.Add(item.ProjectId, item.DateOfNotification);					
				}
			}
			ViewBag.D = dict;			
			return View(projects);
		}
		
		[HttpPost]
		public async Task<IActionResult> UpdateStatus(int id, ProjectStatus status)
		{
			var project = await _projectRepository.GetByIdAsync(id);
			if (project != null)
                _context.Entry(project).State = EntityState.Detached;           
			var updateProject = new Project
			{
				Id = project.Id,
				Name = project.Name,
				Description = project.Description,
				Status = status,
				ClientId = project.ClientId,
				dtNotify = project.dtNotify
			};		
			_projectRepository.Update(updateProject);
            return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> NavToNotification(int id)
		{
			var project = await _projectRepository.GetByIdAsync(id);           
            TempData["Id"] = id;
            return RedirectToAction("Create", "Notification");
		}
	}
}
