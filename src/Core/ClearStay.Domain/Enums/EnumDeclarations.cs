using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearStay.Domain.Enums
{
    public enum UserRole { Admin = 1, Owner = 2, Tenant = 3 }
    public enum ChargeType { Maintenance = 1, Gas = 2, Electricity = 3, Water = 4, LateFee = 5, Other = 6 }
    public enum ExpenseCategory { Salary = 1, Repairs = 2, Landscaping = 3, Utilities = 4, Legal = 5, Other = 6 }

    public enum AssetCategory
    {
        Lift = 1,
        Generator = 2,
        SewageTreatmentPlant = 3, // STP
        WaterPump = 4,
        FireSafety = 5,
        SecuritySystem = 6, // CCTV, Boom Barriers
        GymEquipment = 7,
        SolarPanels = 8,
        SolarRelatedInverters = 9,
        Batteries = 10,
        EVChargingStations = 11,
        Other = 10
    }

    public enum MaintenanceType
    {
        RoutineService = 1,     // Periodic oil change, lift greasing
        BreakdownRepair = 2,    // It broke, fix it now
        AmcRenewal = 3,         // Paying for the yearly contract
        SafetyInspection = 4    // Government or fire safety audit
    }
}
