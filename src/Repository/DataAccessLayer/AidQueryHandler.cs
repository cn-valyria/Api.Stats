using Dapper;
using MySql.Data.MySqlClient;
using Repository.DataAccessLayer.DTO;
using Repository.DataAccessLayer.QueryHelpers;
using Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public class AidQueryHandler : IQueryHandler<Aid>
    {
        private readonly string _connectionString;

        public AidQueryHandler(string connectionString) => _connectionString = connectionString;

        public async Task<Aid?> Query(int id)
        {
            const string query = @"
select 	id as AidId,
		sending_nation_id as SendingNationId,
        sending_alliance_id as SendingAllianceId,
        sending_team as SendingTeam,
        receiving_nation_id as ReceivingNationId,
        receiving_alliance_id as ReceivingAllianceId,
        receiving_team as ReceivingTeam,
        status as Status,
        money as Money,
        technology as Technology,
        soldiers as Soldiers,
        date as Date,
        reason as Reason,
        audit_updated_on as UpdatedOn,
        audit_updated_by as UpdatedBy
from aid
where id = @id";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<Aid>(query, new { id });
        }

        public async Task<IEnumerable<Aid>> Query(IEnumerable<int> ids)
        {
            const string query = @"
select 	id as AidId,
		sending_nation_id as SendingNationId,
        sending_alliance_id as SendingAllianceId,
        sending_team as SendingTeam,
        receiving_nation_id as ReceivingNationId,
        receiving_alliance_id as ReceivingAllianceId,
        receiving_team as ReceivingTeam,
        status as Status,
        money as Money,
        technology as Technology,
        soldiers as Soldiers,
        date as Date,
        reason as Reason,
        audit_updated_on as UpdatedOn,
        audit_updated_by as UpdatedBy
from aid
where id in @ids";

            using var sqlConnection = new MySqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<Aid>(query, new { ids });
        }

        public Task<(int, IEnumerable<Aid>)> Query(SearchFilter filter, IEnumerable<OrderByClause> orderBy, int limit, int offset)
        {
            throw new System.NotImplementedException();
        }
    }
}
