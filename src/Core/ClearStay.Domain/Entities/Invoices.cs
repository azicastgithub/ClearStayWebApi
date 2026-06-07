using ClearStay.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearStay.Domain.Entities
{
    [Table("Invoices")]
    public class Invoice : BaseEntity
    {
        [Key]
        public Guid InvoiceId { get; set; }

        [Required]
        public Guid ApartmentId { get; set; }

        [Required]
        public int BillingMonth { get; set; }

        [Required]
        public int BillingYear { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public Guid? JournalEntryId { get; set; }

        [ForeignKey(nameof(JournalEntryId))]
        public JournalEntry? JournalEntry { get; set; }

        [Required]
        public Guid FinancialYearId { get; set; }

        [ForeignKey(nameof(FinancialYearId))]
        public FinancialYear FinancialYear { get; set; } = null!;

        [Required]
        public bool IsPaid { get; set; }

        [ForeignKey(nameof(ApartmentId))]
        public Apartment Apartment { get; set; } = null!;

        public ICollection<InvoiceLineItem> LineItems { get; set; } = new List<InvoiceLineItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [NotMapped]
        public decimal TotalAmount => LineItems.Sum(x => x.Amount);

        [NotMapped]
        public decimal TotalPaid => Payments.Sum(x => x.AmountPaid);

        [NotMapped]
        public decimal BalanceDue => TotalAmount - TotalPaid;
    }
}
