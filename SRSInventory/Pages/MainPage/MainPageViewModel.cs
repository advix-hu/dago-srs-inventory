using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SRSInventory.Classes;
using SRSInventory.Interfaces;
using SRSInventory.Models;

namespace SRSInventory.Pages.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IDatabaseService _databaseService;
        private readonly Debouncer _debouncer = new Debouncer();
        private readonly ILogger _log;

        [ObservableProperty]
        private FilterableProductView _products = new FilterableProductView();

        [ObservableProperty]
        private IProduct _selectedProduct;

        [ObservableProperty]
        private string _nameFilter;
        [ObservableProperty]
        private string _barcodeFilter;

        public MainPageViewModel(ILogger log, IDatabaseService databaseService)
        {
            _log = log;
            _databaseService = databaseService;
        }

        private void CreateDummyData()
        {
            Task.Run(async () =>
            {
                foreach (var line in File.ReadLines("c:\\Users\\mail\\Downloads\\products_5000.csv"))
                {
                    var s = line.Split(";");
                    var p = new Product() { Name = s[0], Barcode = s[1], Price = int.Parse(s[2]), Quantity = int.Parse(s[3]) };
                    await _databaseService.AddProduct(p);
                }
            }).Wait();
        }

        public void OnAppearing()
        {
            Task.Run(async () =>
            {
                var lastSelectedId = SelectedProduct == null ? -1 : SelectedProduct.Id;

                var result = await _databaseService.QueryProducts();

                if (result.ResultState == ResultStates.Ok)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {                        
                        Products.SetSource(_databaseService.Products);
                        Products.Refresh();

                        if (lastSelectedId != -1)
                        {
                            SelectedProduct = _databaseService.Products.FirstOrDefault(p => p.Id == lastSelectedId);
                        }
                    });
                }
                else
                {
                    _log.Log(LogLevel.Error, result.Message);
                }
            });
        }

        partial void OnNameFilterChanged(string name)
        {
            _debouncer.Debounce(500, () =>
            {
                Products.Filter(name, BarcodeFilter);
            });
        }

        partial void OnBarcodeFilterChanged(string barcode)
        {
            _debouncer.Debounce(500, () =>
            {
                Products.Filter(NameFilter, barcode);
            });
        }

        [RelayCommand]
        public async Task Delete(IProduct product)
        {
            var accepted = await Shell.Current.DisplayAlertAsync("Confirmation", "Do you really want to remove the selected product?", "Yes", "No");
            if (accepted)
            {
                var result = await _databaseService.RemoveProduct(product);
                if (result.ResultState == ResultStates.Ok)
                {
                    Products.Remove(product);
                }
                else
                {
                    _log.Log(LogLevel.Error, result.Message);
                }
            }
        }

        [RelayCommand]
        public async Task Edit(IProduct product)
        {
            if (product == null)
            {
                product = new Product();
            }
            await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
                ["Product"] = product
            });
        }
    }
}
