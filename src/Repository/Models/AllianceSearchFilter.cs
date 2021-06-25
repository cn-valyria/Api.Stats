namespace Repository.Models
{
    public class AllianceSearchFilter : SearchFilter
    {
        public string? AllianceName { get; set; }
        public decimal? AllianceScoreLowerBound { get; set; }
        public decimal? AllianceScoreUpperBound { get; set; }
    }
}
