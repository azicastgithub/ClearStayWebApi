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
    [Table("FinancialYears")]
    public class FinancialYear : BaseEntity
    {
        [Key]
        public Guid FinancialYearId { get; set; }

        [Required]
        [MaxLength(20)]
        public string YearName { get; set; } = string.Empty; // e.g., "FY 2025-2026"

        [Required]
        public DateTime StartDate { get; set; } // e.g., April 1, 2025

        [Required]
        public DateTime EndDate { get; set; } // e.g., March 31, 2026

        // The absolute most important field for accounting compliance:
        public bool IsClosed { get; set; } = false;
    }
}
