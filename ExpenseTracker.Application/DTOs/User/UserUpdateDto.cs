using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTOs.User
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; } // Opsiyonel, null ise şifre değişmez
        public int RoleId { get; set; }
        public string Iban { get; set; }
    }
}
