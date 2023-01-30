using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using CRM_test.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CRM_test.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(ApplicationContext context, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Client> clients = await _clientRepository.GetAll();
            return View(clients);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            _clientRepository.Add(client);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return View("Error");
            var newClient = new Client
            {
                Name = client.Name,
                Comment = client.Comment,
                Project = client.Project
            };
            return View(newClient);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Client clientEdit)
        {
            if (clientEdit != null)
            {
                var newClient = new Client
                {
                    Id = id,
                    Name = clientEdit.Name,
                    Comment = clientEdit.Comment,
                    Project = clientEdit.Project
                };

                _clientRepository.Update(newClient);
                return RedirectToAction("Index");
            }
            
            return View(clientEdit);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return View("Error");
            var deleteClient = new Client
            {
                Name = client.Name,
                Comment = client.Comment,
                Project = client.Project
            };
            return View(deleteClient);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return View("Error");
            _clientRepository.Delete(client);
            return RedirectToAction("Index");
        }
    }
}
