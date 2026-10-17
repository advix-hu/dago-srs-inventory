using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SRSInventory.Classes;
using SRSInventory.Interfaces;

namespace SRSInventory.Pages.DetailsPage
{
    public partial class DetailsPageViewModel : ObservableObject
    {
        private readonly IProduct _original;
        private readonly ILogger _log;
        private readonly IDatabaseService _databaseService;

        [ObservableProperty]
        private IProduct _product;

        [ObservableProperty]
        private bool _canEdit;

        public string Title => Product.Id == 0 ? "ADD" : "EDIT";

        public DetailsPageViewModel(IProduct product, ILogger log, IDatabaseService databaseService)
        {
            _original = product;
            _log = log;
            _databaseService = databaseService;

            _product = (IProduct)_original.Clone();
        }

        public void OnAppearing()
        {
            Validate();
        }

        [RelayCommand]
        public async Task Validate()
        {
            var invalidame = string.IsNullOrWhiteSpace(_product.Name) || _databaseService.Products.Any(p => p.Name.Equals(_product.Name, StringComparison.InvariantCultureIgnoreCase) && p.Id != _product.Id);
            var invalidBarcode = _databaseService.Products.Any(p => p.Barcode.Equals(_product.Barcode, StringComparison.InvariantCultureIgnoreCase) && p.Id != _product.Id);
            var invalidQuantity = _product.Quantity < 0;
            var invalidPrice = _product.Price <= 0;
            CanEdit = !(invalidame || invalidBarcode || invalidQuantity || invalidPrice);
        }

        [RelayCommand]
        public async Task Ok()
        {
            if (_original.Id == 0)
            {
                var updateResult = await _databaseService.AddProduct(_product);
                if (updateResult.ResultState != ResultStates.Ok)
                {
                    _log.Log(LogLevel.Error, updateResult.Message);
                }
            }
            else
            {
                _original.Update(_product);

                var updateResult = await _databaseService.Update(_original);
                if (updateResult.ResultState != ResultStates.Ok)
                {
                    _log.Log(LogLevel.Error, updateResult.Message);
                }
            }
            await Shell.Current.Navigation.PopToRootAsync();
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
