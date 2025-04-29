using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTOs.Auth
{
    public class TokenResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
