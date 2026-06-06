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

        // 1-to-1 Foreign Key linking to the Auth User
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        [ForeignKey(nameof(UserId))]
        public User UserAccount { get; set; } = null!;

        // Navigation Properties to Apartments
        [InverseProperty(nameof(Apartment.Owner))]
        public ICollection<Apartment> OwnedApartments { get; set; } = new List<Apartment>();

        [InverseProperty(nameof(Apartment.CurrentTenant))]
        public ICollection<Apartment> RentedApartments { get; set; } = new List<Apartment>();

        public ICollection<OccupancyHistory> OccupancyHistories { get; set; } = new List<OccupancyHistory>();
    }
}
