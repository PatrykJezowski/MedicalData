using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class MedicalContext : DbContext 
    {
        public MedicalContext(DbContextOptions<MedicalContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = null!;
    }
}