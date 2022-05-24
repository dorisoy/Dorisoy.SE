namespace Dreamer.Services.Interface
{
    public interface IStockPosting
    {
        decimal StockCheckForProductSale(int decProductId, int decBatchId);
    }
}
