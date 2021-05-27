using GraphQL.Server;
using GraphQL;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Types;
using Api.GraphQL;
using Api.GraphQL.Types;
using GraphQL.NewtonsoftJson;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddGraphQLQuery<CybernationsQuery>()
                .AddGraphQLTypes()
                .AddGraphQLSchema<CybernationsSchema>();

            // Azure Functions do not use `Microsoft.Extensions.DependencyInjection`, instead they use
            // DryIoc - https://github.com/dadhi/DryIoc. This leads to a different behavior if multiple
            // constructors exists so for DocumentExecuter should be called one which has no arguments.
            // See also https://bitbucket.org/dadhi/dryioc/wiki/SelectConstructorOrFactoryMethod
            builder.Services.AddSingleton<IDocumentExecuter>(sp => new DocumentExecuter());
            builder.Services.AddSingleton<IDocumentWriter>(new DocumentWriter());
            builder.Services.AddGraphQL();
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLQuery<T>(this IServiceCollection services) where T : ObjectGraphType<object>
            => services.AddSingleton<T>();

        public static IServiceCollection AddGraphQLSchema<T>(this IServiceCollection services) where T : Schema
            => services.AddSingleton<ISchema, T>();

        public static IServiceCollection AddGraphQLTypes(this IServiceCollection services) => services
            .AddSingleton<GovernmentTypeEnum>()
            .AddSingleton<NationalWarStatusEnum>()
            .AddSingleton<NationType>()
            .AddSingleton<RecentActivityEnum>()
            .AddSingleton<ReligionEnum>()
            .AddSingleton<TeamEnum>();
    }
}
