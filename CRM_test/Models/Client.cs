using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace CRM_test.Models
{
    public class Client
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public string? Comment { get; set; }

        public Project? Project { get; set; }
    }
}
