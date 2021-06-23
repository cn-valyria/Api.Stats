using Dapper;
using MySql.Data.MySqlClient;
using Repository.DataAccessLayer.DTO;
using Repository.DataAccessLayer.QueryHelpers;
using Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public class WarQueryHandler : IQueryHandler<War>
    {
        private readonly string _connectionString;

        public WarQueryHandler(string connectionString) => _connectionString = connectionString;

        public async Task<War?> Query(int id)
        {
            const string query = @"
select 	id as WarId,
		attacking_nation_id as AttackingNationId,
		attacking_alliance_id as AttackingAllianceId,
		attacking_team as AttackingTeam,
		defending_nation_id as DefendingNationId,
		defending_alliance_id as DefendingAllianceId,
		defending_team as DefendingTeam,
		war_status as WarStatus,
		begin_date as BeginDate,
		end_date as EndDate,
		reason as Reason,
		destruction as Destruction,
		attack_percent as AttackPercent,
		defend_percent as DefendPercent,
		audit_updated_on as UpdatedOn,
		audit_updated_by as UpdatedBy
from 	war
where 	id = @id;";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<War>(query, new { id });
        }

        public Task<IEnumerable<War>> Query(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<(int, IEnumerable<War>)> Query(SearchFilter filter, IEnumerable<OrderByClause> orderBy, int limit, int offset)
        {
            throw new System.NotImplementedException();
        }
    }
}
