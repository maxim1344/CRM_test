using CRM_test.Models;

namespace CRM_test.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAll();

        Task<Notification> GetByIdAsync(int id);

        bool Add(Notification notification);

        bool Update(Notification notification);

        bool Delete(Notification notification);

        bool Save();
    }
}
