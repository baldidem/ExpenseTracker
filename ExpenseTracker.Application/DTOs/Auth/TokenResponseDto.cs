namespace ExpenseTracker.Application.DTOs.Auth
{
    public class TokenResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
