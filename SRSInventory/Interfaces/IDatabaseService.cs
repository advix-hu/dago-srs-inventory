using SRSInventory.Classes;

namespace SRSInventory.Interfaces
{
    public interface IDatabaseService
    {
        List<IProduct> Products { get; }
        Task <IResult<ResultStates>> QueryProducts();
        Task<IResult<ResultStates>> AddProduct(IProduct product);
        Task<IResult<ResultStates>> RemoveProduct(IProduct product);
        Task<IResult<ResultStates>> Update(IProduct product);
    }
}
