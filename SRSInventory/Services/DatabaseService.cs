using SQLite;
using SRSInventory.Classes;
using SRSInventory.Consts;
using SRSInventory.Interfaces;
using SRSInventory.Models;

namespace SRSInventory.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection? _connection;
        private volatile bool _initialized;

        public List<IProduct> Products { get; private set; }

        public async Task<IResult<ResultStates>> QueryProducts()
        {
            var initResult = await CheckInitialized();
            if (initResult.ResultState == ResultStates.Failed)
            {
                return new Result<ResultStates>(ResultStates.Failed, initResult.ResultValue);
            }

            var result = new List<IProduct>();
            try
            {
                var table = await _connection!.Table<Product>().ToListAsync();
                Products = table.Cast<IProduct>().ToList();
                return new Result<ResultStates>(ResultStates.Ok);
            }
            catch (Exception ex)
            {
                return new Result<ResultStates>(ResultStates.Failed, ex);
            }
        }

        public async Task<IResult<ResultStates>> RemoveProduct(IProduct product)
        {
            var initResult = await CheckInitialized();
            if (initResult.ResultState == ResultStates.Failed)
            {
                return new Result<ResultStates>(ResultStates.Failed, initResult.ResultValue);
            }

            try
            {
                await _connection!.Table<Product>().DeleteAsync(p => p.Id == product.Id);
                return new Result<ResultStates>(ResultStates.Ok);
            }
            catch (Exception ex)
            {
                return new Result<ResultStates>(ResultStates.Failed, ex);
            }
        }

        public async Task<IResult<ResultStates>> Update(IProduct product)
        {
            var initResult = await CheckInitialized();
            if (initResult.ResultState == ResultStates.Failed)
            {
                return new Result<ResultStates>(ResultStates.Failed, initResult.ResultValue);
            }

            try
            {
                await _connection!.UpdateAsync(product);
                return new Result<ResultStates>(ResultStates.Ok);
            }
            catch (Exception ex)
            {
                return new Result<ResultStates>(ResultStates.Failed, ex);
            }
        }

        public async Task<IResult<ResultStates>> AddProduct(IProduct product)
        {
            var initResult = await CheckInitialized();
            if (initResult.ResultState == ResultStates.Failed)
            {
                return new Result<ResultStates>(ResultStates.Failed, initResult.ResultValue);
            }

            try
            {
                await _connection!.InsertAsync(product);
                return new Result<ResultStates>(ResultStates.Ok);
            }
            catch (Exception ex)
            {
                return new Result<ResultStates>(ResultStates.Failed, ex);
            }
        }

        private async Task<Result<ResultStates, Exception>> CheckInitialized()
        {
            try
            {
                if (!_initialized)
                {                    
                    if (!File.Exists(Consts.DatabaseConsts.DatabasePath))
                    {
                        using var source = await FileSystem.OpenAppPackageFileAsync(DatabaseConsts.DatabaseFilename);

                        using var target = File.Create(Consts.DatabaseConsts.DatabasePath);

                        await source.CopyToAsync(target);
                    }

                    _connection = new SQLiteAsyncConnection(Consts.DatabaseConsts.DatabasePath, DatabaseConsts.Flags);
                    _initialized = true;
                }
                return new Result<ResultStates, Exception>(ResultStates.Ok, null);
            }
            catch (Exception ex)
            {
                return new Result<ResultStates, Exception>(ResultStates.Failed, ex);
            }
        }
    }
}
