using CRM_test.Data;
using CRM_test.Interfaces;
using CRM_test.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_test.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationContext _context;

        public NotificationRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _context.Notifications.ToListAsync();
        }
        public bool Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            return Save();
        }

        public bool Delete(Notification notification)
        {
            _context.Notifications.Remove(notification);
            return Save();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Notification notification)
        {
            _context.Update(notification);
            return Save();
        }
    }
}
