namespace PIMS.Services.ProductServices;

public class ProductFilterInput
{
    public string? ProductName { get; set; }
    public List<string> CategoryIds{ get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}