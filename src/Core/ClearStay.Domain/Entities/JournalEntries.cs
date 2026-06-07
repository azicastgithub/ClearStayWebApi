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
    [Table("JournalEntries")]
    public class JournalEntry : BaseEntity
    {
        [Key]
        public Guid JournalEntryId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty; // e.g., "Payment received from Apt A-101"

        [MaxLength(100)]
        public string? ReferenceNumber { get; set; } // e.g., Cheque No or Invoice No

        [Required]
        public Guid FinancialYearId { get; set; }

        [ForeignKey(nameof(FinancialYearId))]
        public FinancialYear FinancialYear { get; set; } = null!;

        public ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }
}
