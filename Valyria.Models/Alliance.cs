using System;

namespace Valyria.Models
{
    public class Alliance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }
        public int TotalNations { get; set; }
        public int ActiveNations { get; set; }
        public int PercentActive { get; set; }
        public int TotalStrength { get; set; }
        public int AverageStrength { get; set; }
        public decimal Score { get; set; }
        public int TotalLand { get; set; }
        public int TotalInfrastructure { get; set; }
        public int TotalTechnology { get; set; }
        public int NationsAtWar { get; set; }
        public int NationsAtPeace { get; set; }
        public int TotalSoldiers { get; set; }
        public int TotalTanks { get; set; }
        public int TotalCruiseMissiles { get; set; }
        public int TotalNuclearWeapons { get; set; }
        public int TotalAircraft { get; set; }
        public int TotalNavy { get; set; }
        public int TotalNationsInAnarchy { get; set; }
    }
}
