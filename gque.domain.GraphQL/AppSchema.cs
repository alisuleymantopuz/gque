using GraphQL;
using GraphQL.Types;

namespace gque.domain.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ProductGraphQuery>();
            Mutation = resolver.Resolve<ProductMutation>();
        }
    }
}
