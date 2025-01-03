using LinqKit;
using System.Threading.Tasks;
using PIMS.Models;
using PIMS.Repository;


namespace PIMS.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Product> _products;
    private readonly IRepository<ProductCategory> _productCategories;
    private readonly IRepository<Category> _categories;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _products = _unitOfWork.GetRepository<Product>();
        _productCategories = _unitOfWork.GetRepository<ProductCategory>();
        _categories = _unitOfWork.GetRepository<Category>();
    }

    public async Task<string> CreateProduct(ProductInput productInput)
    {
        // Create and save the product
        Product product = productInput.ToProudctEntity();
        _products.Add(product);

        if (productInput.Hascategories())
        {
            List<ProductCategory> productCategories = productInput.ToProductCategoryEntities();
            _productCategories.AddMultiple(productCategories);
        }
        
        await _unitOfWork.SaveChangesAsync();
        
        return product.ProductId;
    }

    public async Task<bool> UpdateProduct(ProductInput productInput)
    {
        var product = _products.GetAll().FirstOrDefault(pr => pr.ProductId == productInput.ProductId);
        if (product == null)
        {
            return false;
        }

        product = productInput.UpdateProductEntity(product);
        _products.Update(product);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AdjustPrice(PriceAdjustmentInput adjustmentInput)
    {
        var productList = _products.GetAll().Where(pr => adjustmentInput.ProductIds.Contains(pr.ProductId)).ToList();

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
        
        _products.UpdateMultiple(productList);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public ProductOutput GetProductById(string productId)
    {
        var productInfo = (from product in _products.GetAll()
            let categoryList = (from productCategory in _productCategories.GetAll()
                join category in _categories.GetAll() on productCategory.CategoryId equals category.CategoryId
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
        var filteredProducts = _products.GetAll();
        if (!string.IsNullOrEmpty(filter.ProductName))
        {
            predicate = predicate.And(product => product.Name.Contains(filter.ProductName));
        }

        if (filter.CategoryIds?.Count > 0)
        {
            filteredProducts = from product in filteredProducts
                join productCategory in _productCategories.GetAll() on product.ProductId equals productCategory.ProductId
                join category in _categories.GetAll() on productCategory.CategoryId equals category.CategoryId
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

        var query = from product in filteredProducts.Where(predicate)
            let categoryList = (from productCategory in _productCategories.GetAll()
                join category in _categories.GetAll() on productCategory.CategoryId equals category.CategoryId
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
