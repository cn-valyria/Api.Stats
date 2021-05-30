using GraphQL.Server;
using GraphQL;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using GraphQL.Types;
using Api.GraphQL;
using Api.GraphQL.Types;
using GraphQL.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using Repository.DataAccessLayer;
using DbNation = Repository.DataAccessLayer.DTO.Nation;
using DbAlliance = Repository.DataAccessLayer.DTO.Alliance;
using Valyria.Models;
using Repository;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddAutoMapper(typeof(Startup))
                .AddCnDbRepository(builder.GetContext().Configuration)
                .AddGraphQLQuery<CybernationsQuery>()
                .AddGraphQLTypes()
                .AddGraphQLSchema<CybernationsSchema>()
                .AddGraphQLDocumentTypes()
                .AddGraphQL();
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddCnDbRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var nationQueryHandler = new NationQueryHandler(configuration.GetConnectionString("CnDb"));
            var allianceQueryHandler = new AllianceQueryHandler(configuration.GetConnectionString("CnDb"));

            return services
                .AddSingleton<IQueryHandler<DbNation>>(nationQueryHandler)
                .AddSingleton<IQueryHandler<DbAlliance>>(allianceQueryHandler)
                .AddSingleton<IReferenceQueryHandler<DbAlliance>>(allianceQueryHandler)
                .AddSingleton<INationDataHandler, NationDataHandler>()
                .AddSingleton<IAllianceDataHelper, AllianceDataHandler>()
                .AddSingleton<ICnDbRepository, CnDbRepository>();
        }

        public static IServiceCollection AddGraphQLQuery<T>(this IServiceCollection services) where T : ObjectGraphType<object>
            => services.AddSingleton<T>();

        public static IServiceCollection AddGraphQLSchema<T>(this IServiceCollection services) where T : Schema
            => services.AddSingleton<ISchema, T>();

        public static IServiceCollection AddGraphQLTypes(this IServiceCollection services) 
            => services
                .AddSingleton<AllianceType>()
                .AddSingleton<GovernmentTypeEnum>()
                .AddSingleton<NationalWarStatusEnum>()
                .AddSingleton<NationType>()
                .AddSingleton<RecentActivityEnum>()
                .AddSingleton<ReligionEnum>()
                .AddSingleton<TeamEnum>();

        public static IServiceCollection AddGraphQLDocumentTypes(this IServiceCollection services)
            => services

                // Azure Functions do not use `Microsoft.Extensions.DependencyInjection`, instead they use
                // DryIoc - https://github.com/dadhi/DryIoc. This leads to a different behavior if multiple
                // constructors exists so for DocumentExecuter should be called one which has no arguments.
                // See also https://bitbucket.org/dadhi/dryioc/wiki/SelectConstructorOrFactoryMethod
                .AddSingleton<IDocumentExecuter>(sp => new DocumentExecuter())
                .AddSingleton<IDocumentWriter>(new DocumentWriter());
    }
}
