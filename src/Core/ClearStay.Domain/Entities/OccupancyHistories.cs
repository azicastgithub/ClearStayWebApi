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
    [Table("OccupancyHistories")]
    public class OccupancyHistory : BaseEntity
    {
        [Key]
        public Guid OccupancyHistoryId { get; set; }

        [Required]
        public Guid ApartmentId { get; set; }

        // Updated to reference ResidentId
        [Required]
        public Guid ResidentId { get; set; }

        [Required]
        public DateTime MoveInDate { get; set; }

        public DateTime? MoveOutDate { get; set; }

        [ForeignKey(nameof(ApartmentId))]
        public Apartment Apartment { get; set; } = null!;

        // Updated to reference Resident
        [ForeignKey(nameof(ResidentId))]
        public Resident Resident { get; set; } = null!;
    }
}
