using System.Collections.Generic;
using Valyria.Models;

namespace Repository.Models
{
    public class SearchResult<T>
    {
        public int TotalCount { get; set; }
        public List<T> Results { get; set; } = new List<T>();
    }
}
