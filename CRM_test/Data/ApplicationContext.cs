using CRM_test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CRM_test.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {           

        }
    }
}
