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
    [Table("JournalEntryLines")]
    public class JournalEntryLine : BaseEntity
    {
        [Key]
        public Guid JournalEntryLineId { get; set; }

        [Required]
        public Guid JournalEntryId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DebitAmount { get; set; } // Left side of the ledger

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CreditAmount { get; set; } // Right side of the ledger

        [MaxLength(255)]
        public string? LineDescription { get; set; }

        [ForeignKey(nameof(JournalEntryId))]
        public JournalEntry JournalEntry { get; set; } = null!;

        [ForeignKey(nameof(AccountId))]
        public LedgerAccount LedgerAccount { get; set; } = null!;
    }
}
