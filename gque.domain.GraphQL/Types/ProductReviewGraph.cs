using GraphQL.Types;

namespace gque.domain.GraphQL.Types
{
    public class ProductReviewGraph : ObjectGraphType<ProductReview>
    {
        public ProductReviewGraph()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Review);
        }
    }
}
