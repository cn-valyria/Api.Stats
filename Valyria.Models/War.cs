using System;
using System.Collections.Generic;
using System.Text;
using Valyria.Models.Enums;

namespace Valyria.Models
{
    public class War
    {
        public int Id { get; set; }
        public Nation AttackingNation { get; set; }
        public Alliance AttackingAlliance { get; set; }
        public Team AttackingTeam { get; set; }
        public decimal AttackingDestruction { get; set; }
        public Nation DefendingNation { get; set; }
        public Alliance DefendingAlliance { get; set; }
        public Team DefendingTeam { get; set; }
        public decimal DefendingDestruction { get; set; }
        public WarStatus WarStatus { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
    }
}
