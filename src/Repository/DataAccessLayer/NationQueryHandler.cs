using Dapper;
using MySql.Data.MySqlClient;
using Repository.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public class NationQueryHandler : IQueryHandler<Nation>
    {
        private readonly string _connectionString;

        public NationQueryHandler(string connectionString) => _connectionString = connectionString;

        public async Task<Nation> Query(int id)
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

        public Task<IEnumerable<Nation>> Query(string filter, int limit, int offset)
        {
            throw new System.NotImplementedException();
        }
    }
}
