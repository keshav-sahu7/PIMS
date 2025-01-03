namespace PIMS.Services.ProductServices;

public interface IProductService
{
    Task<string> CreateProduct(ProductInput productInput);
    Task<bool> UpdateProduct(ProductInput productInput);
    Task<bool> AdjustPrice(PriceAdjustmentInput adjustmentInput);
    ProductOutput GetProductById(string productId);
    List<ProductOutput> GetProducts(ProductFilterInput filterInput);
}