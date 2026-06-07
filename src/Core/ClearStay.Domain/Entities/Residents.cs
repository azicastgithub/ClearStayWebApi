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
    [Table("Residents")]
    public class Resident : BaseEntity
    {
        [Key]
        public Guid ResidentId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        // --- New: Demographic & KYC Details ---

        [MaxLength(15)] // Usually 12 digits, but string allows formatting/encryption padding
        public string? AadhaarNumber { get; set; }

        [MaxLength(2048)]
        public string? IdProofDocumentUrl { get; set; } // Link to uploaded Aadhaar/Passport image

        [MaxLength(10)]
        public string? BloodGroup { get; set; } // Vital for medical emergencies in the building

        // --- New: Permanent Address Details ---
        // (Even if they live in the apartment, owners often have a separate permanent address)

        [MaxLength(255)]
        public string? PermanentAddressLine1 { get; set; }

        [MaxLength(255)]
        public string? PermanentAddressLine2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? PinCode { get; set; }

        // --- New: Emergency Contact ---

        [MaxLength(150)]
        public string? EmergencyContactName { get; set; }

        [MaxLength(20)]
        public string? EmergencyContactNumber { get; set; }

        [MaxLength(50)]
        public string? EmergencyContactRelation { get; set; } // e.g., "Brother", "Spouse"

        // --- Navigation Properties ---

        [ForeignKey(nameof(UserId))]
        public User UserAccount { get; set; } = null!;

        [InverseProperty(nameof(Apartment.Owner))]
        public ICollection<Apartment> OwnedApartments { get; set; } = new List<Apartment>();

        [InverseProperty(nameof(Apartment.CurrentTenant))]
        public ICollection<Apartment> RentedApartments { get; set; } = new List<Apartment>();

        public ICollection<OccupancyHistory> OccupancyHistories { get; set; } = new List<OccupancyHistory>();

        // New: A resident can have multiple vehicles registered with the society
        public ICollection<Vehicle> RegisteredVehicles { get; set; } = new List<Vehicle>();
    }
}
