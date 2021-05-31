using Dapper;
using MySql.Data.MySqlClient;
using Repository.DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<Nation>> Query(string filter, int limit, int offset)
        {
            throw new NotImplementedException();
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
