﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using SigmaProject.Data.Entities;

namespace SigmaProject.Data.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditFields(DbContext? context)
        {
            if (context == null) return;

            var entries = context.ChangeTracker.Entries<BaseEntity>();
            var currentTime = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.Created = currentTime;

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    entry.Entity.LastModified = currentTime;
            }
        }
    }
}