using GraphQL.Types;

namespace gque.domain.GraphQL.Types
{
    public class ProductReviewInputGraph : InputObjectGraphType
    {
        public ProductReviewInputGraph()
        {
            Name = "reviewInput";
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("review");
            Field<NonNullGraphType<IntGraphType>>("productId");
        }
    }
}
