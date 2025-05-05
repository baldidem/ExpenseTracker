using AutoMapper;
using ExpenseTracker.Application.DTOs.ExpenseCategory;
using ExpenseTracker.Application.Interfaces;


namespace ExpenseTracker.Application.Services.ExpenseCategory
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ExpenseCategoryResponseDto>> GetAllAsync()
        {
            var expenseCategories = await _unitOfWork.ExpenseCategoryRepository.GetAllAsync();
            return _mapper.Map<List<ExpenseCategoryResponseDto>>(expenseCategories);
        }

        public async Task<ExpenseCategoryResponseDto> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero.");
            }

            var expenseCategory = await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(id);
            if (expenseCategory == null)
            {
                throw new KeyNotFoundException($"Expense category with id {id} was not found.");
            }

            return _mapper.Map<ExpenseCategoryResponseDto>(expenseCategory);
        }

        public async Task<ExpenseCategoryResponseDto> CreateAsync(ExpenseCategoryCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Input data is required.");
            }

            var existingCategory = await _unitOfWork.ExpenseCategoryRepository
                .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower() && !x.IsActive);

            if (existingCategory != null)
            {
                throw new InvalidOperationException("A category with the same name already exists.");
            }

            var expenseCategory = _mapper.Map<ExpenseTracker.Domain.Entities.ExpenseCategory>(dto);
            await _unitOfWork.ExpenseCategoryRepository.CreateAsync(expenseCategory);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ExpenseCategoryResponseDto>(expenseCategory);
        }

        public async Task<bool> UpdateAsync(int id, ExpenseCategoryUpdateDto dto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero.");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Input data is required.");
            }

            var category = await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Expense category with id {id} was not found.");
            }

            var isExist = await _unitOfWork.ExpenseCategoryRepository
                .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower() && x.Id != id && !x.IsActive);

            if (isExist != null)
            {
                throw new InvalidOperationException("Another category with the same name already exists.");
            }

            category.Name = dto.Name;

            _unitOfWork.ExpenseCategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero.");
            }

            var expenseCategory = await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(id);
            if (expenseCategory == null)
            {
                throw new KeyNotFoundException($"Expense category with id {id} was not found.");
            }
            if (!expenseCategory.IsActive)
            {
                throw new InvalidOperationException("Expense category is already deleted.");
            }

            var hasExpenses = await _unitOfWork.ExpenseRepository.AnyAsync(x => x.ExpenseCategoryId == id && x.IsActive);

            if (hasExpenses)
            {
                throw new InvalidOperationException("Cannot delete the category because it has associated expenses.");
            }

            _unitOfWork.ExpenseCategoryRepository.Delete(expenseCategory);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
