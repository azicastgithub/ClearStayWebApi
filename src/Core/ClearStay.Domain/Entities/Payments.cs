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
    [Table("Payments")]
    public class Payment : BaseEntity
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid InvoiceId { get; set; }

        public Guid? PaidByResidentId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [MaxLength(100)]
        public string? ReferenceNumber { get; set; } // e.g., UPI Transaction ID

        public Guid? JournalEntryId { get; set; }

        [ForeignKey(nameof(JournalEntryId))]
        public JournalEntry? JournalEntry { get; set; }

        [Required]
        public Guid FinancialYearId { get; set; }

        [ForeignKey(nameof(FinancialYearId))]
        public FinancialYear FinancialYear { get; set; } = null!;

        // New: Stores the cloud URL or file path to the uploaded screenshot
        [MaxLength(2048)]
        public string? ReceiptImageUrl { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public Invoice Invoice { get; set; } = null!;

        [ForeignKey(nameof(PaidByResidentId))]
        public Resident? PaidBy { get; set; }
    }
}
