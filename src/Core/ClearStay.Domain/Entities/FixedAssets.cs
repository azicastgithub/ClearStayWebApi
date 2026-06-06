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
    [Table("FixedAssets")]
    public class FixedAsset : BaseEntity
    {
        [Key]
        public Guid AssetId { get; set; }

        [Required]
        [MaxLength(150)]
        public string AssetName { get; set; } = string.Empty; // e.g., "Passenger Lift - Block A"

        [Required]
        public AssetCategory Category { get; set; }

        [MaxLength(100)]
        public string Location { get; set; } = string.Empty; // e.g., "Block A Lobby", "Basement 2"

        public DateTime? InstallationDate { get; set; }

        // AMC (Annual Maintenance Contract) Tracking
        public bool IsUnderAmc { get; set; }

        public Guid? AmcVendorId { get; set; }

        [ForeignKey(nameof(AmcVendorId))]
        public Vendor? AmcVendor { get; set; }

        public DateTime? AmcExpiryDate { get; set; }

        // Navigation Property
        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
    }
}
