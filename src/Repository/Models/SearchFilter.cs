using System.Collections.Generic;

namespace Repository.Models
{
    public class SearchFilter
    {
        public List<int>? NationIds { get; set; }
        public string? NationName { get; set; }
        public string? RulerName { get; set; }
        public string? AllianceName { get; set; }
        public decimal? NationStrengthLowerBound { get; set; }
        public decimal? NationStrengthUpperBound { get; set; }
        public FilterMatchType? Match { get; set; }
    }

    public enum FilterMatchType
    {
        Any,
        All
    }
}
