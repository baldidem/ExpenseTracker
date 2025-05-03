using ExpenseTracker.Application.Interfaces.CurrentUser;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Persistence.Interceptors
{
    //public class AppSaveChangesInterceptor : SaveChangesInterceptor
    //{
    //    private readonly ICurrentUserService _currentUserService;

    //    public AppSaveChangesInterceptor(ICurrentUserService currentUserService)
    //    {
    //        _currentUserService = currentUserService;
    //    }

    //    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    //    {

    //        var dbContext = eventData.Context;
    //        if (dbContext == null) return base.SavingChanges(eventData, result);

    //        var userId = _currentUserService.CurrentUserId ?? 0;

    //        foreach (var entry in dbContext.ChangeTracker.Entries<BaseEntity>())
    //        {
    //            switch (entry.State)
    //            {
    //                case EntityState.Added:
    //                    entry.Entity.CreatedDate = DateTime.UtcNow;
    //                    entry.Entity.CreatedUserId = userId;
    //                    entry.Entity.IsActive = true;
    //                    break;

    //                case EntityState.Modified:
    //                    entry.Entity.UpdatedDate = DateTime.UtcNow;
    //                    entry.Entity.UpdatedUserId = userId;
    //                    break;

    //                case EntityState.Deleted:
    //                    entry.State = EntityState.Modified; // Soft-delete
    //                    entry.Entity.DeletedDate = DateTime.UtcNow;
    //                    entry.Entity.DeletedUserId = userId;
    //                    entry.Entity.IsActive = false;
    //                    break;
    //            }
    //        }


    //        return base.SavingChanges(eventData, result);
    //    }
    //}
}
