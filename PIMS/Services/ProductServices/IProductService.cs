namespace PIMS.Services.ProductServices;

public interface IProductService
{
    string CreateProduct(ProductInput productInput);
    bool UpdateProduct(ProductInput productInput);
    bool AdjustPrice(PriceAdjustmentInput adjustmentInput);
    ProductOutput GetProductById(string productId);
    List<ProductOutput> GetProducts(ProductFilterInput filterInput);
}