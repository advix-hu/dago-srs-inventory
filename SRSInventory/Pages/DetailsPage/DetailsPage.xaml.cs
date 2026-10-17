using SRSInventory.Interfaces;

namespace SRSInventory.Pages.DetailsPage;

public partial class DetailsPage : ContentPage, IQueryAttributable
{
    private readonly IDatabaseService _database;
    private readonly ILogger _logger;
    private DetailsPageViewModel _viewModel;

    public DetailsPage(IDatabaseService database, ILogger logger)
    {
        _database = database;
        _logger = logger;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Product", out var product))
        {
            _viewModel = new DetailsPageViewModel((IProduct)product, _logger, _database);
            BindingContext = _viewModel;
        }
    }
}