namespace PIMS.Services.ProductServices;

public interface IProductService
{
    string CreateProduct(ProductInput productInput);
    bool UpdateProduct(string productid, ProductInput productInput);
    bool AdjustPrice(string productid, PriceAdjustmentInput adjustmentInput);
    ProductOutput GetProductById(string productId);
    List<ProductOutput> GetProducts(ProductFilterInput filterInput);
}