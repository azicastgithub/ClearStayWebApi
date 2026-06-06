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
    [Table("Vendors")]
    public class Vendor : BaseEntity
    {
        [Key]
        public Guid VendorId { get; set; }

        [Required]
        [MaxLength(150)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ContactPersonName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? TaxRegistrationNumber { get; set; } // e.g., GSTIN for tax compliance

        [MaxLength(255)]
        public string? BillingAddress { get; set; }

        [MaxLength(50)]
        public string? BankAccountNumber { get; set; }

        [MaxLength(50)]
        public string? BankRoutingCode { get; set; } // IFSC or Swift Code

        // Navigation Properties (What has this vendor worked on or supplied?)
        public ICollection<AssociationExpense> Expenses { get; set; } = new List<AssociationExpense>();
        public ICollection<FixedAsset> AmcContracts { get; set; } = new List<FixedAsset>();
        public ICollection<MaintenanceRecord> MaintenanceServices { get; set; } = new List<MaintenanceRecord>();
    }
}
