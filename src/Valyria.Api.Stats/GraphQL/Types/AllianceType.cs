using GraphQL.Types;
using System.Security.Cryptography.X509Certificates;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class AllianceType : ObjectGraphType<Alliance>
    {
        public AllianceType()
        {
            Name = "Alliance";
            Description = "A collective of nations under the same banner.";

            Field(a => a.Id).Description("The Alliance ID");
            Field(a => a.Name).Description("The name of the alliance");
            Field(a => a.Updated).Description("The last date that the alliance was updated by an alliance manager");
            Field<DateTimeGraphType>(nameof(Alliance.TotalNations), "The number of nations that are fully-approved members of the alliance");
            Field(a => a.ActiveNations).Description("The number of fully-approved members that have been active recently");
            Field(a => a.PercentActive).Description("The percentage of fully-approved members that have been active recently");
            Field(a => a.TotalStrength).Description("The total nation strength of all fully-approved members in the alliance");
            Field(a => a.AverageStrength).Description("The average nation strength of all fully-approved members in the alliance");
            Field<DecimalGraphType>(nameof(Alliance.Score), "Alliance Score, a score reflecting the size and strength of the alliance as a whole.");
            Field(a => a.TotalLand).Description("The total amount of land held by the alliance");
            Field(a => a.TotalInfrastructure).Description("The total amount of infrastructure held by the alliance");
            Field(a => a.TotalTechnology).Description("The total amount of technology held by the alliance");
            Field(a => a.NationsAtWar).Description("The number of fully-approved members currently set to War Mode");
            Field(a => a.NationsAtPeace).Description("The number of fully-approved members currently set to Peace Mode");
            Field(a => a.TotalSoldiers).Description("The total amount of soldiers held by the alliance");
            Field(a => a.TotalTanks).Description("The total amount of tanks held by the alliance");
            Field(a => a.TotalCruiseMissiles).Description("The total amount of cruise missiles held by the alliance");
            Field(a => a.TotalNuclearWeapons).Description("The total amount of nukes held by the alliance");
            Field(a => a.TotalAircraft).Description("The total amount of aircraft held by the alliance");
            Field(a => a.TotalNavy).Description("The total amount of navy vessels held by the alliance");
            Field(a => a.TotalNationsInAnarchy).Description("The number of fully-approved members currently in anarchy");
        }
    }
}
