namespace ExpenseTracker.Application.DTOs.Common
{

    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ApiResponse(string message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                IsSuccess = true;
            }
            else
            {
                IsSuccess = false;
                Message = message;
            }
        }
    }

    public class ApiResponse<TEntity>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }

        public ApiResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = isSuccess ? "Success" : "Error";
        }
        public ApiResponse(TEntity data)
        {
            IsSuccess = true;
            Data = data;
            Message = "Success";
        }
        public ApiResponse(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}
