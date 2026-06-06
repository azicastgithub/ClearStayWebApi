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
    [Table("MaintenanceRecords")]
    public class MaintenanceRecord : BaseEntity
    {
        [Key]
        public Guid MaintenanceRecordId { get; set; }

        [Required]
        public Guid AssetId { get; set; }

        [Required]
        public MaintenanceType Type { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; } // When is it supposed to happen?

        public DateTime? CompletedDate { get; set; } // Null means it is pending/upcoming

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; } // Can be 0.00 if covered under AMC

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty; // e.g., "Replaced broken sensor"

        public Guid? VendorId { get; set; }

        [ForeignKey(nameof(VendorId))]
        public Vendor? Vendor { get; set; }

        [ForeignKey(nameof(AssetId))]
        public FixedAsset Asset { get; set; } = null!;
    }
}
