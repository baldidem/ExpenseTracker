//using ExpenseTracker.Application.Interfaces.Auth;
//using ExpenseTracker.Application.Settings;
//using ExpenseTracker.Domain.Entities;
//using System.Security.Claims;

//namespace ExpenseTracker.Infrastructure.Auth
//{
//    public class TokenService : ITokenService
//    {
//        private readonly JwtSettings _jwtSettings;

//        public TokenService(JwtSettings jwtSettings)
//        {
//            _jwtSettings = jwtSettings;
//        }

//        //public string GenerateToken(User user) // jwtSettings'e gore token uretecek method.
//        //{
//        //    var claims = new List<Claim>
//        //    {
//        //        new Claim("UserId", user.Id.ToString()),
//        //        new Claim("FistName" , user.Name),
//        //        new Claim("Surname", user.Surname),
//        //        new Claim("UserName",user.Surname),
//        //        new Claim("Email",user.Email),
//        //        new Claim("RoleId",user.RoleId.ToString())

//        //    };
//        //}
//    }
//}
