using GraphQL.Types;

namespace gque.domain.GraphQL.Types
{
    public class ProductTypeGraph : EnumerationGraphType<ProductType>
    {
        public ProductTypeGraph()
        {
            Name = "Type";
            Description = "The type of product";
        }
    }
}
