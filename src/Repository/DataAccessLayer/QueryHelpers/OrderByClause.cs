using System;

namespace Repository.DataAccessLayer.QueryHelpers
{
    public class OrderByClause
    {
        public string ColumnName { get; }
        public string SortOrder { get; }

        public OrderByClause(string columnName, object sortOrder)
        {
            ColumnName = columnName;

            if (sortOrder is int intSortOrder)
                SortOrder = intSortOrder switch
                {
                    1 => "ASC",
                    2 => "DESC",
                    _ => ""
                };
            else if (sortOrder is string stringSortOrder)
                SortOrder = stringSortOrder;
            else
                throw new InvalidCastException($"Unable to determine sort order from {sortOrder}");
        }

        public static OrderByClause DefaultNationOrderBy => new OrderByClause("Strength", "DESC");
    }
}
