using LinqKit;
using PIMS.Models;
using PIMS.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly PimsContext _dbContext; // Inject repository for data access

    public ProductService(PimsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string CreateProduct(ProductInput productInput)
    {
        // Create and save the product
        Product product = productInput.ToProudctEntity();
        _dbContext.Products.Add(product);

        if (productInput.Hascategories())
        {
            List<ProductCategory> productCategories = productInput.ToProductCategoryEntities();
            _dbContext.ProductCategories.AddRange(productCategories);
        }
        
        _dbContext.SaveChanges();
        
        return product.ProductId;
    }

    public bool UpdateProduct(ProductInput productInput)
    {
        var product = _dbContext.Products.FirstOrDefault(pr => pr.ProductId == productInput.ProductId);
        if (product == null)
        {
            return false;
        }

        product = productInput.UpdateProductEntity(product);
        _dbContext.Update(product);
        _dbContext.SaveChanges();
        return true;
    }

    public bool AdjustPrice(PriceAdjustmentInput adjustmentInput)
    {
        var productList = _dbContext.Products.Where(pr => adjustmentInput.ProductIds.Contains(pr.ProductId)).ToList();

        if (adjustmentInput.PercentageDecrease.HasValue)
        {
            productList.ForEach(product =>
            {
                product.Price = product.Price - (product.Price * adjustmentInput.PercentageDecrease.Value / 100.0M);
            });
        }
        else if (adjustmentInput.FixedAmount.HasValue)
        {
            productList.ForEach(product =>
            {
                product.Price = product.Price - adjustmentInput.FixedAmount.Value;
            });
        }

        _dbContext.UpdateRange(productList);
        _dbContext.SaveChanges();
        return true;
    }

    public ProductOutput GetProductById(string productId)
    {
        var productInfo = (from product in _dbContext.Products
            let categoryList = (from productCategory in _dbContext.ProductCategories
                join category in _dbContext.Categories on productCategory.CategoryId equals category.CategoryId
                where productCategory.ProductId == product.ProductId
                select category.Name).ToList()
            where product.ProductId == productId
            select new ProductOutput(product)
            {
                CategoryList = categoryList
            }).FirstOrDefault();
        if (productInfo == null)
        {
            throw new Exception("Product not found.");
        }

        return productInfo;
    }

    public List<ProductOutput> GetProducts(ProductFilterInput filter)
    {
        var predicate = PredicateBuilder.New<Product>(true);

        var products = _dbContext.Products.AsQueryable();
        
        if (!string.IsNullOrEmpty(filter.ProductName))
        {
            predicate = predicate.And(product => product.Name.Contains(filter.ProductName));
        }

        if (filter.CategoryIds?.Count > 0)
        {
            products = from product in products
                join productCategory in _dbContext.ProductCategories on product.ProductId equals productCategory.ProductId
                join category in _dbContext.Categories on productCategory.CategoryId equals category.CategoryId
                where filter.CategoryIds.Contains(category.CategoryId)
                select product;
        }

        if (filter.MaxPrice.HasValue)
        {
            predicate = predicate.And(product => product.Price <= filter.MaxPrice.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            predicate = predicate.And(product => product.Price >= filter.MinPrice.Value);
        }

        var query = from product in products.Where(predicate)
            let categoryList = (from productCategory in _dbContext.ProductCategories
                join category in _dbContext.Categories on productCategory.CategoryId equals category.CategoryId
                where productCategory.ProductId == product.ProductId
                select category.Name).ToList()
            select new ProductOutput(product)
            {
                CategoryList = categoryList
            };

        if (filter.PageNumber.HasValue && filter.PageSize.HasValue)
        {
            query = query.Skip(filter.PageNumber.Value * (filter.PageSize.Value - 1)).Take(filter.PageSize.Value);
        }

        return query.ToList();
    }
}
