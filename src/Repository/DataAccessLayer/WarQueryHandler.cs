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

        public async Task<IEnumerable<War>> Query(IEnumerable<int> ids)
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
where 	id in @ids;";

			using var sqlConnection = new MySqlConnection(_connectionString);
			return await sqlConnection.QueryAsync<War>(query, new { ids });
		}

        public async Task<(int, IEnumerable<War>)> Query(SearchFilter filter, IEnumerable<OrderByClause> orderBy, int limit, int offset)
		{
			// Sanity check that the correct search filter is used for this query
			if (!(filter is WarSearchFilter warSearchFilter))
				throw new ArgumentException(nameof(filter));

			using var sqlConnection = new MySqlConnection(_connectionString);

			// Call the main proc to execute the search
			await sqlConnection.ExecuteAsync("search_wars", new
			{
				_attacking_nation = warSearchFilter.AttackingNation,
				_attacking_alliance = warSearchFilter.AttackingAlliance,
				_defending_nation = warSearchFilter.DefendingNation,
				_defending_alliance = warSearchFilter.DefendingAlliance,
				_match_type = (int)(warSearchFilter.Match ?? FilterMatchType.Any)
			}, commandType: CommandType.StoredProcedure);

			// Count all data that could be returned from the search results
			const string totalCountQuery = "select count(1) from tmpWarSearchResults";
			var totalCount = await sqlConnection.QueryFirstAsync<int>(totalCountQuery);

			// Grab the data in the temp table that the proc should have populated and return that
			var resultsQuery = $@"
select * 
from tmpWarSearchResults 
order by {string.Join(", ", orderBy.Select(clause => $"{clause.ColumnName} {clause.SortOrder}"))}
limit @offset, @limit";
			var searchResults = await sqlConnection.QueryAsync<War>(resultsQuery, new { limit, offset });

			return (totalCount, searchResults);
		}
    }
}
