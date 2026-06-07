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
    [Table("Vehicles")]
    public class Vehicle : BaseEntity
    {
        [Key]
        public Guid VehicleId { get; set; }

        [Required]
        public Guid ResidentId { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; set; } = string.Empty; // e.g., KL-01-AB-1234

        [Required]
        [MaxLength(50)]
        public string VehicleType { get; set; } = string.Empty; // e.g., "Car", "Two-Wheeler"

        [MaxLength(100)]
        public string? MakeAndModel { get; set; } // e.g., "Hyundai Creta", "Royal Enfield"

        [MaxLength(50)]
        public string? Color { get; set; }

        // Sometimes an apartment has an assigned parking slot for a specific vehicle
        [MaxLength(20)]
        public string? AssignedParkingSlot { get; set; } // e.g., "P-101"

        [ForeignKey(nameof(ResidentId))]
        public Resident Resident { get; set; } = null!;
    }
}
