using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialSnapshot.Database.Models
{
    public class UserSession
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
        public DateTime Created { get; set; }
        public byte Type { get; set; }

        public User User { get; set; }
    }
}
