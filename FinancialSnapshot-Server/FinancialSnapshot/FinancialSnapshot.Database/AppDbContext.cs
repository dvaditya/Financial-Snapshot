using FinancialSnapshot.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialSnapshot.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}