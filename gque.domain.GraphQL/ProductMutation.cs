using gque.domain.GraphQL.Types;
using gque.domain.managers;
using GraphQL.Types;

namespace gque.domain.GraphQL
{
    public class ProductMutation : ObjectGraphType
    {
        public ProductMutation(ProductReviewManager reviewRepository)
        {
            FieldAsync<ProductReviewGraph>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductReviewInputGraph>> { Name = "review" }),
                resolve: async context =>
                {
                    var review = context.GetArgument<ProductReview>("review");
                    return await context.TryAsyncResolve(
                        async c => await reviewRepository.AddReview(review));
                });
        }
    }
}
