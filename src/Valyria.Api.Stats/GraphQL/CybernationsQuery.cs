using Api.GraphQL.Types;
using GraphQL.Types;
using System;
using Valyria.Models;
using Valyria.Models.Enums;

namespace Api.GraphQL
{
    public class CybernationsQuery : ObjectGraphType<object>
    {
        public CybernationsQuery()
        {
            Name = "Query";

            Field<NationType>(
                "nation", 
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context => new Nation
                {
                    Id = 1,
                    Name = "Test",
                    RulerName = "Dummy",
                    AllianceDate = DateTime.Now,
                    AllianceStatus = "Member",
                    GovernmentType = GovernmentType.Anarchy,
                    Religion = Religion.BahaiFaith,
                    Team = Team.None,
                    Created = DateTime.MinValue,
                    Technology = 0,
                    Infrastructure = 100,
                    BaseLand = 2,
                    WarStatus = NationalWarStatus.War,
                    Votes = 0,
                    Strength = 20.248m,
                    Defcon = 5,
                    BaseSoldiers = 5,
                    Tanks = 0,
                    CruiseMissiles = 0,
                    Nukes = 0,
                    RecentActivity = RecentActivity.ActiveInTheLast3Days
                });
        }
    }
}
