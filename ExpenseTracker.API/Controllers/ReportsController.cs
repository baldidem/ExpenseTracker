using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.DTOs.Report;
using ExpenseTracker.Application.Services.Report;
using ExpenseTracker.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("ExpenseReportForCurrentUser")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetExpenseReportDto()
        {
            var result = await _reportService.GetReportForCurrentUserAsync();

            return Ok(new ApiResponse<List<ExpenseReportDto>>(result));
        }
        [HttpGet("CompanyPaymentRate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanyPaymentRateReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _reportService.GetCompanyPaymentRateAsync(startDate, endDate);
            return Ok(new ApiResponse<CompanyPaymentRateReportResponseDto>(result));
        }

        [HttpGet("UserPaymentRate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserPaymentRateReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, int userId)
        {
            var result = await _reportService.GetUserPaymentRateAsync(startDate, endDate,userId);
            return Ok(new ApiResponse<UserPaymentReportResponseDto>(result));
        }
        [HttpGet("CompanyApprovalStatusReport")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetApprovalStatusReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] ExpenseStatus? status)
        {
            var result = await _reportService.GetApprovalStatusReportAsync(startDate, endDate, status);
            return Ok(new ApiResponse<ExpenseApprovalStatusReportResponseDto>(result));
        }

    }
}
