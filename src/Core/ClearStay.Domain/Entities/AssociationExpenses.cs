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
    [Table("AssociationExpenses")]
    public class AssociationExpense : BaseEntity
    {
        [Key]
        public Guid ExpenseId { get; set; }

        [Required]
        public ExpenseCategory Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DateIncurred { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public Guid? VendorId { get; set; }

        [ForeignKey(nameof(VendorId))]
        public Vendor? Vendor { get; set; }
    }
}
