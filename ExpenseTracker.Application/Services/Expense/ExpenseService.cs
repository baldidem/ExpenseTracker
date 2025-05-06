using AutoMapper;
using ExpenseTracker.Application.DTOs.Expense;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.CurrentUser;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Services.Expense
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<List<ExpenseResponseDto>> GetAllForAdmin(int? userId = null)
        {
            // This method is used for admin role.
            List<Domain.Entities.Expense> expenses;

            if (userId != null)
            {
                expenses = await _unitOfWork.ExpenseRepository.GetAllByParametersAsync(x => x.UserId == userId && x.IsActive);
            }

            expenses = await _unitOfWork.ExpenseRepository.GetAllByParametersAsync(x => x.IsActive);
            var mappedExpenses = _mapper.Map<List<ExpenseResponseDto>>(expenses);
            return mappedExpenses;
        }
        public async Task<List<ExpenseResponseDto>> GetAllForCurrentUser()
        {
            var currentUserId = _currentUserService.CurrentUserId;
            if (currentUserId == null)
            {
                throw new UnauthorizedAccessException("User is not found!");
            }
            var expenses = await _unitOfWork.ExpenseRepository.GetAllByParametersAsync(x => x.UserId == currentUserId);
            var mappedExpenses = _mapper.Map<List<ExpenseResponseDto>>(expenses);
            return mappedExpenses;
        }

        public async Task<ExpenseResponseDto> GetByIdAsync(int expenseId)
        {
            if (expenseId <= 0)
            {
                throw new ArgumentException("Expense ID must be greater than zero.");
            }

            var expense = await _unitOfWork.ExpenseRepository.GetByIdAsync(expenseId);
            if (expense == null)
            {
                throw new KeyNotFoundException($"Expense with ID {expenseId} was not found.");
            }

            return _mapper.Map<ExpenseResponseDto>(expense);

        }
        public Task<List<ExpenseResponseDto>> GetByParametersForCurrentUser(ExpenseFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public async Task<ExpenseResponseDto> CreateAsync(ExpenseCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Input data is required.");
            }

            var currentUserId = _currentUserService.CurrentUserId;
            var currentUserRole = _currentUserService.CurrentUserRole;

            if (currentUserId == null)
            {
                throw new UnauthorizedAccessException("Current user could not be determined.");
            }

            if (string.Equals(currentUserRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Admins are not allowed to create expenses.");
            }

            // ExpenseCategory kontrolü
            var category = await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(dto.ExpenseCategoryId);

            if (category == null || !category.IsActive)
            {
                throw new KeyNotFoundException($"Expense category with id {dto.ExpenseCategoryId} was not found or is inactive.");
            }

            // 🔥 Dosya kaydetme işlemi (opsiyonel)
            string? documentPath = null;
            if (dto.Document != null && dto.Document.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{dto.Document.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Document.CopyToAsync(fileStream);
                }

                documentPath = $"/uploads/{uniqueFileName}";
            }

            // Expense nesnesi oluştur
            var expense = new ExpenseTracker.Domain.Entities.Expense
            {
                UserId = currentUserId.Value,
                Amount = dto.Amount,
                ExpenseCategoryId = dto.ExpenseCategoryId,
                DocumentPath = documentPath,
                ExpenseStatus = ExpenseStatus.Pending,
                IsActive = true
            };

            await _unitOfWork.ExpenseRepository.CreateAsync(expense);
            await _unitOfWork.SaveChangesAsync();
            var createdExpense = await _unitOfWork.ExpenseRepository.GetByIdAsync(expense.Id, "User");


            return _mapper.Map<ExpenseResponseDto>(createdExpense);
        }

        public async Task<bool> UpdateAsync(int id, ExpenseUpdateDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Expense id must be greater than zero.");

            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Input data is required.");

            var currentUserId = _currentUserService.CurrentUserId;
            var currentUserRole = _currentUserService.CurrentUserRole;

            if (currentUserId == null)
                throw new UnauthorizedAccessException("User identity not found.");

            var expense = await _unitOfWork.ExpenseRepository.GetByIdAsync(id, "User");

            if (expense == null)
                throw new KeyNotFoundException($"Expense with id {id} was not found.");

            if (!string.Equals(currentUserRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                // Staff ise sadece kendi kaydını güncelleyebilir
                if (expense.UserId != currentUserId.Value)
                    throw new UnauthorizedAccessException("You can only update your own expenses.");
            }

            // 🔥 Amount güncelleme (nullable check)
            if (dto.Amount.HasValue)
            {
                if (dto.Amount.Value <= 0)
                    throw new InvalidOperationException("Amount must be greater than zero.");

                expense.Amount = dto.Amount.Value;
            }

            // 🔥 Category güncelleme (nullable check)
            if (dto.ExpenseCategoryId.HasValue)
            {
                var category = await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(dto.ExpenseCategoryId.Value);
                if (category == null || !category.IsActive)
                    throw new InvalidOperationException("Expense category is not valid.");

                expense.ExpenseCategoryId = dto.ExpenseCategoryId.Value;
            }

            // 🔥 Dosya işlemleri
            if (dto.Document != null && dto.Document.Length > 0)
            {
                // Eski dosyayı sil
                if (!string.IsNullOrEmpty(expense.DocumentPath))
                {
                    var existingPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", expense.DocumentPath.TrimStart('/'));
                    if (File.Exists(existingPath))
                    {
                        File.Delete(existingPath);
                    }
                }

                // Yeni dosyayı kaydet
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}_{dto.Document.FileName}";
                var newPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    await dto.Document.CopyToAsync(fileStream);
                }

                expense.DocumentPath = $"/uploads/{uniqueFileName}";
            }

            _unitOfWork.ExpenseRepository.Update(expense);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Expense id must be greater than zero.");

            var currentUserId = _currentUserService.CurrentUserId;
            var currentUserRole = _currentUserService.CurrentUserRole;

            if (currentUserId == null)
                throw new UnauthorizedAccessException("User identity not found.");

            var expense = await _unitOfWork.ExpenseRepository.GetByIdAsync(id);

            if (expense == null)
                throw new KeyNotFoundException($"Expense with id {id} was not found.");

            if (!expense.IsActive)
                throw new InvalidOperationException("Expense is already deleted.");

            // Eğer Admin değilse kendi kaydını silebilir mi kontrol et
            if (!string.Equals(currentUserRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                if (expense.UserId != currentUserId.Value)
                    throw new UnauthorizedAccessException("You can only delete your own expenses.");
            }

            _unitOfWork.ExpenseRepository.Delete(expense);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateExpenseStatus(int expenseId, ExpenseStatusDto dto)
        {
            if (expenseId <= 0)
                throw new ArgumentException("Expense id must be greater than zero.");
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Input data is required.");

            var expense = await _unitOfWork.ExpenseRepository.GetByIdAsync(expenseId);

            if (expense == null)
                throw new KeyNotFoundException($"Expense with id {expenseId} was not found.");

            if (expense.ExpenseStatus != ExpenseStatus.Pending)
                throw new InvalidOperationException("Only pending expenses can be updated.");

            if (dto.NewStatus == ExpenseStatus.Approved)
            {
                // ✅ 1. Expense onaylandı
                expense.ExpenseStatus = ExpenseStatus.Approved;

                // ✅ 2. PaymentSimulation oluştur
                var payment = new PaymentSimulation
                {
                    ExpenseId = expense.Id,
                    Amount = expense.Amount,
                    PaidDate = DateTime.UtcNow,
                    PaymentTransactionStatus = PaymentTransactionStatus.Confirmed
                };

                await _unitOfWork.PaymentSimulationRepository.CreateAsync(payment);
            }
            else if (dto.NewStatus == ExpenseStatus.Rejected)
            {
                // Red durumunda gerekçe kontrolü
                if (string.IsNullOrWhiteSpace(dto.RejectionReason))
                    throw new InvalidOperationException("Rejection reason is required when rejecting an expense.");

                expense.ExpenseStatus = ExpenseStatus.Rejected;
                expense.RejectionReason = dto.RejectionReason;

            }
            else
            {
                throw new InvalidOperationException("Invalid status provided.");
            }

            _unitOfWork.ExpenseRepository.Update(expense);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
