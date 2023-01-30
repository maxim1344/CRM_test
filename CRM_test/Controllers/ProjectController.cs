using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using CRM_test.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM_test.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientRepository _clientRepository;


        public ProjectController(ApplicationContext context, IProjectRepository projectRepository, IClientRepository clientRepository)
        {
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Project> projects = await _projectRepository.GetAll();
			ViewBag.data = projects;
			return View(projects);
        }

        public async Task<IActionResult> Create()
        {
            List<Client> clients = (List<Client>)await _clientRepository.GetAll();
            ViewBag.data = clients;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }
            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            List<Client> clients = (List<Client>)await _clientRepository.GetAll();
            ViewBag.data = clients;
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return View("Error");
            var newProject = new Project
            {
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
				ClientId = project.ClientId,
                dtNotify = project.dtNotify
			};
            return View(newProject);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Project projectEdit)
        {
            if (projectEdit != null)
            {
                var newProject = new Project
                {
                    Id = id,
                    Name = projectEdit.Name,
                    Description = projectEdit.Description,
                    Status = projectEdit.Status,                    
                    ClientId = projectEdit.ClientId,
                    dtNotify = projectEdit.dtNotify
                };

                _projectRepository.Update(newProject);
                return RedirectToAction("Index");
            }

            return View(projectEdit);
        }
                
        public async Task<IActionResult> Delete(int id)
        {
            List<Client> clients = (List<Client>)await _clientRepository.GetAll();
            ViewBag.data = clients;
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return View("Error");
            var deleteProject = new Project
            {
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
				ClientId = project.ClientId,
			};
            return View(deleteProject);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return View("Error");
            _projectRepository.Delete(project);
            return RedirectToAction("Index");
        }
    }
}
