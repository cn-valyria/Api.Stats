using System;
using System.Collections.Generic;
using System.Text;
using Valyria.Models.Enums;

namespace Valyria.Models
{
    public class Aid
    {
        public int Id { get; set; }
        public Nation SendingNation { get; set; }
        public Alliance SendingAlliance { get; set; }
        public Team SendingTeam { get; set; }
        public Nation ReceivingNation { get; set; }
        public Alliance ReceivingAlliance { get; set; }
        public Team ReceivingTeam { get; set; }
        public AidStatus Status { get; set; }
        public int Money { get; set; }
        public int Technology { get; set; }
        public int Soldiers { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
    }
}
