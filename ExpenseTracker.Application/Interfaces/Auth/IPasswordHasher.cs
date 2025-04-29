using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces.Auth
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool ConfirmPassword(string password,string hashedPassword);
    }
}
