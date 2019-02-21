using gque.domain.GraphQL.Types;
using gque.domain.infrastructure;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace gque.domain.GraphQL
{
    public class ProductGraphQuery : ObjectGraphType
    {
        public ProductGraphQuery(AppDbContext dbContext)
        {
            Field<ListGraphType<ProductGraph>>(
                "products",
                resolve: context => dbContext.Products.ToListAsync()
            );

            Field<ProductGraph>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return dbContext.Products.Find(id);
                }
            );
        }
    }
}
