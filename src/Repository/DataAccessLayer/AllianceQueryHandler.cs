using Dapper;
using MySql.Data.MySqlClient;
using Repository.DataAccessLayer.DTO;
using Repository.DataAccessLayer.QueryHelpers;
using Repository.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public class AllianceQueryHandler : IQueryHandler<Alliance>, IReferenceQueryHandler<Alliance>
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

        public async Task<Alliance?> Query(ReferenceQueryRequest referenceQueryRequest)
        {
            // Sanity check
            if (referenceQueryRequest.NationId is null)
                return null;

            const string query = @"
select 	alliance.id as AllianceId,
		alliance.name as AllianceName,
        updated as Updated,
        total_nations as TotalNations,
        active_nations as ActiveNations,
        percent_active as PercentActive,
        alliance.strength as Strength,
        average_strength as AverageStrength,
        score as Score,
        land as Land,
        alliance.infrastructure as Infrastructure,
        alliance.technology as Technology,
        war as War,
        peace as Peace,
        soldiers as Soldiers,
        alliance.tanks as Tanks,
        alliance.cruise as Cruise,
        alliance.nukes as Nukes,
        alliance.aircraft as Aircraft,
        alliance.navy as Navy,
        anarchy as Anarchy
from 	alliance
join	nation on alliance.id = nation.alliance_id
where	nation.id = @nation_id";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<Alliance>(query, new { nation_id = referenceQueryRequest.NationId });
        }

        public Task<(int, IEnumerable<Alliance>)> Query(SearchFilter filter, IEnumerable<OrderByClause> orderBy, int limit, int offset)
        {
            throw new System.NotImplementedException();
        }
    }
}
