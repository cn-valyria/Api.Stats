namespace Repository.Models
{
    public class WarSearchFilter : SearchFilter
    {
        public string? AttackingNation { get; set; }
        public string? AttackingAlliance { get; set; }
        public string? DefendingNation { get; set; }
        public string? DefendingAlliance { get; set; }
    }
}
