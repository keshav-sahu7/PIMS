namespace PIMS.Services.ProductServices;

public class PriceAdjustmentInput
{
    public List<string> ProductIds { get; set; }
    public decimal? PercentageDecrease { get; set; }
    public decimal? FixedAmount { get; set; }
}