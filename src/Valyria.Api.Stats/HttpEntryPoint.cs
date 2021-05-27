using Api.GraphQL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GraphQL.Server.Internal;
using GraphQL.Types;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class HttpEntryPoint
    {
        private readonly IGraphQLExecuter<ISchema> _graphQLExecuter;

        public HttpEntryPoint(IGraphQLExecuter<ISchema> graphQLExecuter) => _graphQLExecuter = graphQLExecuter;

        [FunctionName("graphql")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var result = await _graphQLExecuter.ExecuteAsync(req, log);

                if (result.Errors?.Any() ?? false)
                    log.LogError("GraphQL execution errors: {errors}", result.Errors);

                return new GraphQLExecutionResult(result);
            }
            catch (GraphQLBadRequestException ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }
    }
}
