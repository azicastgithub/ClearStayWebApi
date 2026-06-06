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
    [Table("Apartments")]
    public class Apartment : BaseEntity
    {
        [Key]
        public Guid ApartmentId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ApartmentNumber { get; set; } = string.Empty;

        [Required]
        public Guid OwnerId { get; set; }
        public Guid? CurrentTenantId { get; set; }
        // Updated to reference Resident
        [ForeignKey(nameof(OwnerId))]
        public Resident Owner { get; set; } = null!;

        [ForeignKey(nameof(CurrentTenantId))]
        public Resident? CurrentTenant { get; set; }
        public ICollection<OccupancyHistory> OccupancyHistories { get; set; } = new List<OccupancyHistory>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
