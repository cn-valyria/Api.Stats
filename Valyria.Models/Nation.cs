using System;
using Valyria.Models.Enums;

namespace Valyria.Models
{
    public class Nation
    {
        public int Id { get; set; }
        public string RulerName { get; set; }
        public string NationName { get; set; }
        public Alliance? Alliance { get; set; }
        public DateTime? AllianceDate { get; set; }
        public string AllianceStatus { get; set; }
        public GovernmentType GovernmentType { get; set; }
        public Religion Religion { get; set; }
        public Team Team { get; set; }
        public DateTime Created { get; set; }
        public decimal Technology { get; set; }
        public decimal Infrastructure { get; set; }
        public decimal BaseLand { get; set; }
        public NationalWarStatus WarStatus { get; set; }
        public int Votes { get; set; }
        public decimal Strength { get; set; }
        public int Defcon { get; set; }
        public int BaseSoldiers { get; set; }
        public int Tanks { get; set; }
        public int CruiseMissiles { get; set; }
        public int Nukes { get; set; }
        public RecentActivity RecentActivity { get; set; }
    }
}
