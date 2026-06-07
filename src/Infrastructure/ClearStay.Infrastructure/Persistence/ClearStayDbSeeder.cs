using System;
using System.Linq;
using System.Threading.Tasks;
using ClearStay.Domain.Entities;
using ClearStay.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ClearStay.Infrastructure.Persistence
{
    public static class ClearStayDbSeeder
    {
        public static async Task SeedAsync(ClearStayDbContext context)
        {
            // Only run the seeder if the database is completely empty
            if (await context.Users.AnyAsync())
            {
                return; // Database has already been seeded
            }

            // 1. Seed the Initial Financial Year (FY 2026-2027)
            var initialFinancialYear = new FinancialYear
            {
                FinancialYearId = Guid.NewGuid(),
                YearName = "FY 2026-2027",
                StartDate = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2027, 3, 31, 23, 59, 59, DateTimeKind.Utc),
                IsClosed = false
            };
            await context.FinancialYears.AddAsync(initialFinancialYear);

            // 2. Seed the Default Admin User
            var adminUserId = Guid.NewGuid();
            var adminUser = new User
            {
                UserId = adminUserId,
                Username = "admin",
                Email = "admin@clearstay.local",
                // Note: In production, NEVER store plain text. Use a library like BCrypt.Net-Next to hash this.
                PasswordHash = "Admin@123!",
                IsActive = true
            };
            await context.Users.AddAsync(adminUser);

            // 3. Seed the Admin's Resident Profile
            var adminResident = new Resident
            {
                ResidentId = Guid.NewGuid(),
                UserId = adminUserId,
                FirstName = "System",
                LastName = "Administrator",
                PhoneNumber = "0000000000",
                Role = UserRole.Admin
            };
            await context.Residents.AddAsync(adminResident);

            // 4. Seed the Essential Chart of Accounts (Ledger)
            //var ledgerAccounts = new[]
            //{
            //    new LedgerAccount
            //    {
            //        AccountId = Guid.NewGuid(),
            //        AccountNumber = "1000",
            //        AccountName = "Cash / Operating Bank Account",
            //        AccountType = AccountType.Asset
            //    },
            //    new LedgerAccount
            //    {
            //        AccountId = Guid.NewGuid(),
            //        AccountNumber = "1200",
            //        AccountName = "Accounts Receivable (Unpaid Bills)",
            //        AccountType = AccountType.Asset
            //    },
            //    new LedgerAccount
            //    {
            //        AccountId = Guid.NewGuid(),
            //        AccountNumber = "4000",
            //        AccountName = "Maintenance Revenue",
            //        AccountType = AccountType.Revenue
            //    },
            //    new LedgerAccount
            //    {
            //        AccountId = Guid.NewGuid(),
            //        AccountNumber = "5000",
            //        AccountName = "General Repair & Maintenance Expense",
            //        AccountType = AccountType.Expense
            //    }
            //};
            //await context.LedgerAccounts.AddRangeAsync(ledgerAccounts);

            // Commit all initial data to the database
            await context.SaveChangesAsync();
        }
    }
}