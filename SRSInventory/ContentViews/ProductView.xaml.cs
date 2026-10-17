using SRSInventory.Interfaces;

namespace SRSInventory.ContentViews;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty ProductProperty = BindableProperty.Create(nameof(Product), typeof(IProduct), typeof(ProductView), default(IProduct));
    public IProduct Product
    {
        get => (IProduct)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }

    public ProductView()
	{
		InitializeComponent();
	}
}