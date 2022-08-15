using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialSnapshot.Models.Domain
{
    public class UserPasswordDto
    {
        public string Username { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}
