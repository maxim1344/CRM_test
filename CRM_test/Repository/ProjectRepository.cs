using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_test.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationContext _context;

        public ProjectRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Add(Project project)
        {
            _context.Projects.Add(project);
            return Save();
        }

        public bool Delete(Project project)
        {
            _context.Projects.Remove(project);
            return Save();
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _context.Projects.Include(i => i.Client).ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(i => i.Id == id);
        }       

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Project project)
        {
            _context.Update(project);
            return Save();
        }
    }
}
