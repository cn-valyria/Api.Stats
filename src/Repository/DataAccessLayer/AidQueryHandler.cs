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

        public async Task<(int, IEnumerable<Aid>)> Query(SearchFilter filter, IEnumerable<OrderByClause> orderBy, int limit, int offset)
        {
            // Sanity check that the correct search filter is used for this query
            if (!(filter is AidSearchFilter aidSearchFilter))
                throw new ArgumentException(nameof(filter));

            using var sqlConnection = new MySqlConnection(_connectionString);

            // Call the main proc to execute the search
            await sqlConnection.ExecuteAsync("search_aid", new
            {
                _sending_nation = aidSearchFilter.SendingNation,
                _sending_ruler = aidSearchFilter.SendingRuler,
                _sending_alliance = aidSearchFilter.SendingAlliance,
                _receiving_nation = aidSearchFilter.ReceivingNation,
                _receiving_ruler = aidSearchFilter.ReceivingRuler,
                _receiving_alliance = aidSearchFilter.ReceivingAlliance,
                _sent_earlier_than = aidSearchFilter.SentEarlierThan,
                _sent_later_than = aidSearchFilter.SentLaterThan,
                _match_type = (int)(aidSearchFilter.Match ?? FilterMatchType.Any)
            }, commandType: CommandType.StoredProcedure);

            // Count all data that could be returned from the search results
            const string totalCountQuery = "select count(1) from tmpAidSearchResults";
            var totalCount = await sqlConnection.QueryFirstAsync<int>(totalCountQuery);

            // Grab the data in the temp table that the proc should have populated and return that
            var resultsQuery = $@"
select * 
from tmpAidSearchResults 
order by {string.Join(", ", orderBy.Select(clause => $"{clause.ColumnName} {clause.SortOrder}"))}
limit @offset, @limit";
            var searchResults = await sqlConnection.QueryAsync<Aid>(resultsQuery, new { limit, offset });

            return (totalCount, searchResults);
        }
    }
}
