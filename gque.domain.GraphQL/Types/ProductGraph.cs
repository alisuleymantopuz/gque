using gque.domain.managers;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace gque.domain.GraphQL.Types
{
    public partial class ProductGraph : ObjectGraphType<Product>
    {
        public ProductGraph(ProductReviewManager productReviewManager, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            Field<ProductTypeGraph>("Type", "The type of product");


            Field<ListGraphType<ProductReviewGraph>>(
                "reviews",
                resolve: context =>
                {
                    var loader =
                        dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                            "GetReviewsByProductId", productReviewManager.GetForProducts);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
