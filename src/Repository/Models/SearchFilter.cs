namespace Repository.Models
{
    public abstract class SearchFilter
    {
        public FilterMatchType? Match { get; set; }
    }

    public enum FilterMatchType
    {
        Any,
        All
    }
}
