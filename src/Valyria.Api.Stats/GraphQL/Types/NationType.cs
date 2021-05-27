using GraphQL.Types;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class NationType : ObjectGraphType<Nation>
    {
        public NationType()
        {
            Name = "Nation";
            Description = "An individual nation. The core entity in Cybernations, and the thing that someone plays as.";

            Field(n => n.Id).Description("The Nation ID");
            Field(n => n.Name).Description("The name of the nation");
            Field(n => n.RulerName).Description("The name of the nation's ruler");
            Field<DateTimeGraphType>(nameof(Nation.AllianceDate), "The date that the nation last changed its alliance affiliation");
            Field(n => n.AllianceStatus).Description("The status of the nation within its alliance");
            Field<GovernmentTypeEnum>(nameof(Nation.GovernmentType), "The nation's selected government type");
            Field<ReligionEnum>(nameof(Nation.Religion), "The nation's selected religion");
            Field<TeamEnum>(nameof(Nation.Team), "The nation's selected team, aka color");
            Field<DateTimeGraphType>(nameof(Nation.Created), "The date that the nation was created");
            Field<DecimalGraphType>(nameof(Nation.Technology), "The amount of technology that the nation currently owns");
            Field<DecimalGraphType>(nameof(Nation.Infrastructure), "The amount of infrastructure that the nation currently owns");
            Field<DecimalGraphType>(nameof(Nation.BaseLand), "The base amount of land that the nation currently owns, not including game-driven modifiers");
            Field<NationalWarStatusEnum>(nameof(Nation.WarStatus), "The current war/peace mode selection of the nation");
            Field(n => n.Votes).Description("The number of senate votes that the nation has received");
            Field<DecimalGraphType>(nameof(Nation.Strength), "Nation Strength, a score reflecting the overall strength of the current nation");
            Field(n => n.Defcon).Description("The DEFCON level of the nation");
            Field(n => n.BaseSoldiers).Description("The amount of soldiers that the nation is currently holding, not including effective soldiers");
            Field(n => n.Tanks).Description("The amount of tanks that the nation is currently holding");
            Field(n => n.CruiseMissiles).Description("The amount of cruise missiles that the nation is currently holding");
            Field(n => n.Nukes).Description("The amount of nuclear missiles that the nation is currently holding");
            Field<RecentActivityEnum>(nameof(Nation.RecentActivity), "A rough measure of how recently the player logged into their nation");
        }
    }
}
