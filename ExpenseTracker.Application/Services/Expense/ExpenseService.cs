using AutoMapper;
using ExpenseTracker.Application.DTOs.Expense;
using ExpenseTracker.Application.Interfaces;

namespace ExpenseTracker.Application.Services.Expense
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ExpenseResponseDto> CreateAsync(ExpenseCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExpenseResponseDto>> GetAll(int? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExpenseResponseDto>> GetAllByParameter(ExpenseFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExpenseResponseDto>> GetAllForCurrentUser()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, ExpenseUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
