using Dapper;
using MySql.Data.MySqlClient;
using Repository.DataAccessLayer.DTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public class NationQueryHandler : IQueryHandler<Nation>, IAuditQueryHandler<Nation>
    {
        private readonly string _connectionString;

        public NationQueryHandler(string connectionString) => _connectionString = connectionString;

        public async Task<Nation?> Query(int id)
        {
            const string query = @"
select  id as NationId,
        nation_name as NationName,
        ruler_name as RulerName,
        alliance_id as AllianceId,
        alliance_date as AllianceDate,
        government_type as GovernmentType,
        religion as Religion,
        team as Team,
        created as Created,
        technology as Technology,
        infrastructure as Infrastructure,
        base_land as BaseLand,
        war_status as WarStatus,
        votes as Votes,
        strength as Strength,
        defcon as Defcon,
        base_soldiers as BaseSoldiers,
        tanks as Tanks,
        cruise_missiles as CruiseMissiles,
        nukes as Nukes,
        recent_activity as RecentActivity,
        audit_updated_on as UpdatedOn,
        audit_updated_by as UpdatedBy
from    nation
where   id = @id;";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<Nation>(query, new { id });
        }

        public async Task<IEnumerable<Nation>> Query(IEnumerable<int> ids)
        {
            const string query = @"
select  id as NationId,
        nation_name as NationName,
        ruler_name as RulerName,
        alliance_id as AllianceId,
        alliance_date as AllianceDate,
        government_type as GovernmentType,
        religion as Religion,
        team as Team,
        created as Created,
        technology as Technology,
        infrastructure as Infrastructure,
        base_land as BaseLand,
        war_status as WarStatus,
        votes as Votes,
        strength as Strength,
        defcon as Defcon,
        base_soldiers as BaseSoldiers,
        tanks as Tanks,
        cruise_missiles as CruiseMissiles,
        nukes as Nukes,
        recent_activity as RecentActivity,
        audit_updated_on as UpdatedOn,
        audit_updated_by as UpdatedBy
from    nation
where   id in @ids";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<Nation>(query, new { ids });
        }

        public async Task<(int, IEnumerable<Nation>)> Query(SearchFilter filter, int limit, int offset)
        {
            using var sqlConnection = new MySqlConnection(_connectionString);

            // Call the main proc to execute the search
            await sqlConnection.ExecuteAsync("search_nations", new
            {
                _nation_name = filter.NationName,
                _ruler_name = filter.RulerName,
                _alliance_name = filter.AllianceName,
                _nation_strength_lower_bound = filter.NationStrengthLowerBound,
                _nation_strength_upper_bound = filter.NationStrengthUpperBound,
                _match_type = (int)(filter.Match ?? FilterMatchType.Any)
            }, commandType: CommandType.StoredProcedure);

            // Count all data that could be returned from the search results
            const string totalCountQuery = "select count(1) from tmpNationSearchResults";
            var totalCount = await sqlConnection.QueryFirstAsync<int>(totalCountQuery);

            // Grab the data in the temp table that the proc should have populated and return that
            const string resultsQuery = "select * from tmpNationSearchResults limit @offset, @limit";
            var searchResults = await sqlConnection.QueryAsync<Nation>(resultsQuery, new { limit, offset });

            return (totalCount, searchResults);
        }

        public async Task<IEnumerable<Nation>> QueryAuditData(
            int id, 
            DateTime? updatedAfter = null, 
            DateTime? updatedBefore = null, 
            int limit = 100, 
            int offset = 0)
        {
            // Update defaults for search dates since they're not compile-time constants
            updatedAfter ??= DateTime.MinValue;
            updatedBefore ??= DateTime.Now;

            const string query = @"
select	nation_id as NationId,
        nation_name as NationName,
        ruler_name as RulerName,
        alliance_date as AllianceDate,
        government_type as GovernmentType,
        religion as Religion,
        team as Team,
        created as Created,
        technology as Technology,
        infrastructure as Infrastructure,
        base_land as BaseLand,
        war_status as WarStatus,
        votes as Votes,
        strength as Strength,
        defcon as Defcon,
        base_soldiers as BaseSoldiers,
        tanks as Tanks,
        cruise_missiles as CruiseMissiles,
        nukes as Nukes,
        recent_activity as RecentActivity,
        change_timestamp as UpdatedOn,
        alliance_id as AllianceId
from 	audit_nation
where 	nation_id = @nation_id
and     change_timestamp >= @updated_after
and     change_timestamp <= @updated_before
order by change_timestamp desc
limit   @offset, @limit";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<Nation>(query, new
            {
                nation_id = id,
                updated_after = updatedAfter.Value,
                updated_before = updatedBefore.Value,
                limit,
                offset
            });
        }
    }
}
