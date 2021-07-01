using System;

namespace Repository.Models
{
    public class AidSearchFilter : SearchFilter
    {
        public string? SendingNation { get; set; }
        public string? SendingRuler { get; set; }
        public string? SendingAlliance { get; set; }
        public string? ReceivingNation { get; set; }
        public string? ReceivingRuler { get; set; }
        public string? ReceivingAlliance { get; set; }
        public DateTime? SentEarlierThan { get; set; }
        public DateTime? SentLaterThan { get; set; }
    }
}
