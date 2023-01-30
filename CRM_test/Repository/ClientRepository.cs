using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_test.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Add(Client client)
        {
            _context.Clients.Add(client);
            return Save();
        }

        public bool Delete(Client client)
        {
            _context.Clients.Remove(client);
            return Save();
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.Include(i => i.Project).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Client client)
        {
            _context.Update(client);
            return Save();
        }
    }
}
