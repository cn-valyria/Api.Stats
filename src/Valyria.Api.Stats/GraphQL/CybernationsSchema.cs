using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace Api.GraphQL
{
    public class CybernationsSchema : Schema
    {
        public CybernationsSchema(IServiceProvider services) : base(services)
        {
            Query = services.GetRequiredService<CybernationsQuery>();
        }
    }
}
