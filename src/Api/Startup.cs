using GraphQL.Server;
using GraphQL;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Types;
using Api.GraphQL;
using GraphQL.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using Repository;
using Repository.DataAccessLayer;
using DbNation = Repository.DataAccessLayer.DTO.Nation;
using DbAlliance = Repository.DataAccessLayer.DTO.Alliance;
using DbWar = Repository.DataAccessLayer.DTO.War;

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
                .AddGraphQLSchema<CybernationsSchema>()
                .AddGraphQLDocumentTypes()
                .AddGraphQL()
                    .AddDataLoader()
                    .AddGraphTypes();
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddCnDbRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var cnDbConnectionString = configuration.GetConnectionString("CnDb");
            var nationQueryHandler = new NationQueryHandler(cnDbConnectionString);
            var allianceQueryHandler = new AllianceQueryHandler(cnDbConnectionString);
            var warQueryHandler = new WarQueryHandler(cnDbConnectionString);

            return services
                .AddSingleton<IQueryHandler<DbNation>>(nationQueryHandler)
                .AddSingleton<IQueryHandler<DbAlliance>>(allianceQueryHandler)
                .AddSingleton<IQueryHandler<DbWar>>(warQueryHandler)
                .AddSingleton<IAuditQueryHandler<DbNation>>(nationQueryHandler)
                .AddSingleton<IAuditQueryHandler<DbAlliance>>(allianceQueryHandler)
                .AddSingleton<INationDataHandler, NationDataHandler>()
                .AddSingleton<IAllianceDataHelper, AllianceDataHandler>()
                .AddSingleton<IWarDataHandler, WarDataHandler>()
                .AddSingleton<ICnDbRepository, CnDbRepository>();
        }

        public static IServiceCollection AddGraphQLSchema<T>(this IServiceCollection services) where T : Schema
            => services.AddSingleton<ISchema, T>();

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
