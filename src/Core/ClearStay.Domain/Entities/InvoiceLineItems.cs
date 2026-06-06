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
    [Table("InvoiceLineItems")]
    public class InvoiceLineItem : BaseEntity
    {
        [Key]
        public Guid InvoiceLineItemId { get; set; }

        [Required]
        public Guid InvoiceId { get; set; }

        [Required]
        public ChargeType ChargeType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public Invoice Invoice { get; set; } = null!;
    }
}
