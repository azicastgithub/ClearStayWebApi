using ClearStay.Domain.Common;
using ClearStay.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearStay.Domain.Entities
{
    [Table("LedgerAccounts")]
    public class LedgerAccount : BaseEntity
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required]
        [MaxLength(20)]
        public string AccountNumber { get; set; } = string.Empty; // e.g., "1000" for Cash, "4000" for Revenue

        [Required]
        [MaxLength(100)]
        public string AccountName { get; set; } = string.Empty; // e.g., "HDFC Operating Account"

        [Required]
        public AccountType AccountType { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<JournalEntryLine> JournalLines { get; set; } = new List<JournalEntryLine>();
    }
}
