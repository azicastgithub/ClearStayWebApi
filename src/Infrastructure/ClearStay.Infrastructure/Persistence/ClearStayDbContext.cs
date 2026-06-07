using ClearStay.Domain.Common;
using ClearStay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearStay.Infrastructure.Persistence
{
    public class ClearStayDbContext : DbContext
    {
        public ClearStayDbContext(DbContextOptions<ClearStayDbContext> options) : base(options)
        {
        }

        // --- Identity & Residents ---
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Resident> Residents { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;

        // --- Property & Occupancy ---
        public DbSet<Apartment> Apartments { get; set; } = null!;
        public DbSet<OccupancyHistory> OccupancyHistories { get; set; } = null!;

        // --- Financial Accounting & Years ---
        public DbSet<FinancialYear> FinancialYears { get; set; } = null!;
        public DbSet<LedgerAccount> LedgerAccounts { get; set; } = null!;
        public DbSet<JournalEntry> JournalEntries { get; set; } = null!;
        public DbSet<JournalEntryLine> JournalEntryLines { get; set; } = null!;

        // --- Operational Ledger ---
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<AssociationExpense> AssociationExpenses { get; set; } = null!;

        // --- Assets & Vendors ---
        public DbSet<Vendor> Vendors { get; set; } = null!;
        public DbSet<FixedAsset> FixedAssets { get; set; } = null!;
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. Core Relationship Protections
            // ==========================================

            // Prevent deleting an Apartment just because an Owner or Tenant is removed.
            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Owner)
                .WithMany(r => r.OwnedApartments)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.CurrentTenant)
                .WithMany(r => r.RentedApartments)
                .HasForeignKey(a => a.CurrentTenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // If a resident leaves the society and is deleted, preserve their payment history
            // but nullify the reference so the financial ledger remains intact.
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaidBy)
                .WithMany()
                .HasForeignKey(p => p.PaidByResidentId)
                .OnDelete(DeleteBehavior.SetNull);

            // ==========================================
            // 2. Financial Year Protections
            // ==========================================
            // CRITICAL: Prevent accidental deletion of a Financial Year if transactions exist.

            modelBuilder.Entity<JournalEntry>()
                .HasOne(j => j.FinancialYear)
                .WithMany()
                .HasForeignKey(j => j.FinancialYearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.FinancialYear)
                .WithMany()
                .HasForeignKey(i => i.FinancialYearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.FinancialYear)
                .WithMany()
                .HasForeignKey(p => p.FinancialYearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssociationExpense>()
                .HasOne(e => e.FinancialYear)
                .WithMany()
                .HasForeignKey(e => e.FinancialYearId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 3. Unique Constraints
            // ==========================================
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Apartment>()
                .HasIndex(a => a.ApartmentNumber)
                .IsUnique();

            modelBuilder.Entity<FinancialYear>()
                .HasIndex(f => f.YearName)
                .IsUnique(); // Ensure no duplicate FY names like "FY 2025-2026"
        }

        // ==========================================
        // 4. The Automatic Audit Trail Interceptor
        // ==========================================
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    // Ensure EF doesn't overwrite the original CreatedAt date on updates
                    entityEntry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
