using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using CRM_test.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM_test.Controllers
{
    public class NotificationController : Controller
    {
		private readonly ApplicationContext _context;
		private readonly INotificationRepository _notificationRepository;
		private readonly IProjectRepository _projectRepository;

		public NotificationController(ApplicationContext context, INotificationRepository notificationRepository, IProjectRepository projectRepository)
        {
			_context = context;
			_notificationRepository = notificationRepository;
			_projectRepository = projectRepository;
		}
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Notification> notifications = await _notificationRepository.GetAll();
            return View(notifications);           
        }

        public async Task<IActionResult> Create()
        {			
			ViewBag.data = TempData["Id"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return View(notification);
            }
            int id = notification.ProjectId;
			var project = await _projectRepository.GetByIdAsync(id);
          
            _context.Entry(project).State = EntityState.Detached;
            var notifyProject = new Project
			{
				Id = project.Id,
				Name = project.Name,
				Description = project.Description,
				Status = project.Status,
				ClientId = project.ClientId,
				dtNotify = notification.DateOfNotification
			};
			
            _context.Update(notifyProject);

			_notificationRepository.Add(notification);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null) return View("Error");
			int id_pr = notification.ProjectId;
			var project = await _projectRepository.GetByIdAsync(id_pr);
			_context.Entry(project).State = EntityState.Detached;
			var notifyProject = new Project
			{
				Id = project.Id,
				Name = project.Name,
				Description = project.Description,
				Status = project.Status,
				ClientId = project.ClientId,
				dtNotify = null
			};
			_context.Update(notifyProject);
			_notificationRepository.Delete(notification);
            return RedirectToAction("Index");
        }
    }
}
