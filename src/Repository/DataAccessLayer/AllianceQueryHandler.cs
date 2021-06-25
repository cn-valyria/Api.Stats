using Dapper;
using MySql.Data.MySqlClient;
using Repository.DataAccessLayer.DTO;
using Repository.DataAccessLayer.QueryHelpers;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public class AllianceQueryHandler : IQueryHandler<Alliance>, IAuditQueryHandler<Alliance>
    {
        private readonly string _connectionString;

        public AllianceQueryHandler(string connectionString) => _connectionString = connectionString;

        public async Task<Alliance?> Query(int id)
        {
            const string query = @"
select 	id as AllianceId,
		name as AllianceName,
        updated as Updated,
        total_nations as TotalNations,
        active_nations as ActiveNations,
        percent_active as PercentActive,
        strength as Strength,
        average_strength as AverageStrength,
        score as Score,
        land as Land,
        infrastructure as Infrastructure,
        technology as Technology,
        war as War,
        peace as Peace,
        soldiers as Soldiers,
        tanks as Tanks,
        cruise as Cruise,
        nukes as Nukes,
        aircraft as Aircraft,
        navy as Navy,
        anarchy as Anarchy
from 	alliance
where   id = @alliance_id";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<Alliance>(query, new { alliance_id = id });
        }

        public async Task<IEnumerable<Alliance>> Query(IEnumerable<int> ids)
        {
            const string query = @"
select 	id as AllianceId,
		name as AllianceName,
        updated as Updated,
        total_nations as TotalNations,
        active_nations as ActiveNations,
        percent_active as PercentActive,
        strength as Strength,
        average_strength as AverageStrength,
        score as Score,
        land as Land,
        infrastructure as Infrastructure,
        technology as Technology,
        war as War,
        peace as Peace,
        soldiers as Soldiers,
        tanks as Tanks,
        cruise as Cruise,
        nukes as Nukes,
        aircraft as Aircraft,
        navy as Navy,
        anarchy as Anarchy
from 	alliance
where   id in @ids";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<Alliance>(query, new { ids = ids.Distinct() });
        }

        public async Task<(int, IEnumerable<Alliance>)> Query(SearchFilter filter, IEnumerable<OrderByClause> orderBy, int limit, int offset)
        {
            // Sanity check that the correct search filter is used for this query
            if (!(filter is AllianceSearchFilter allianceSearchFilter))
                throw new ArgumentException(nameof(filter));

            using var sqlConnection = new MySqlConnection(_connectionString);

            // Call the main proc to execute the search
            await sqlConnection.ExecuteAsync("search_alliances", new
            {
                _alliance_name = allianceSearchFilter.AllianceName,
                _alliance_score_lower_bound = allianceSearchFilter.AllianceScoreLowerBound,
                _alliance_score_upper_bound = allianceSearchFilter.AllianceScoreUpperBound,
                _match_type = (int)(allianceSearchFilter.Match ?? FilterMatchType.Any)
            }, commandType: CommandType.StoredProcedure);

            // Count all data that could be returned from the search results
            const string totalCountQuery = "select count(1) from tmpAllianceSearchResults";
            var totalCount = await sqlConnection.QueryFirstAsync<int>(totalCountQuery);

            // Grab the data in the temp table that the proc should have populated and return that
            var resultsQuery = $@"
select * 
from tmpAllianceSearchResults 
order by {string.Join(", ", orderBy.Select(clause => $"{clause.ColumnName} {clause.SortOrder}"))}
limit @offset, @limit";
            var searchResults = await sqlConnection.QueryAsync<Alliance>(resultsQuery, new { limit, offset });

            return (totalCount, searchResults);
        }

        public async Task<IEnumerable<Alliance>> QueryAuditData(int id, DateTime? updatedAfter = null, DateTime? updatedBefore = null, int limit = 100, int offset = 0)
        {
            // Update defaults for search dates since they're not compile-time constants
            updatedAfter ??= DateTime.MinValue;
            updatedBefore ??= DateTime.Now;

            const string query = @"
select	alliance_id as AllianceId,
		name as AllianceName,
        updated as Updated,
        total_nations as TotalNations,
        active_nations as ActiveNations,
        percent_active as PercentActive,
        strength as Strength,
        average_strength as AverageStrength,
        score as Score,
        land as Land,
        infrastructure as Infrastructure,
        technology as Technology,
        war as War,
        peace as Peace,
        soldiers as Soldiers,
        tanks as Tanks,
        cruise as Cruise,
        nukes as Nukes,
        aircraft as Aircraft,
        navy as Navy,
        anarchy as Anarchy,
        change_timestamp as UpdatedOn
from 	audit_alliance
where 	alliance_id = @alliance_id
and     change_timestamp >= @updated_after
and     change_timestamp <= @updated_before
order by change_timestamp desc
limit   @offset, @limit";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<Alliance>(query, new
            {
                alliance_id = id,
                updated_after = updatedAfter.Value,
                updated_before = updatedBefore.Value,
                limit,
                offset
            });
        }
    }
}
