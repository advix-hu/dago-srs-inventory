using CommunityToolkit.Mvvm.ComponentModel;
using SRSInventory.Interfaces;
using System.Collections.ObjectModel;

namespace SRSInventory.Classes
{
    public class FilterableProductView : ObservableObject
    {
        private List<IProduct> _source = new List<IProduct>();
        private string _name; 
        private string _barcode;

        public int Count => _source.Count;
        public int FilteredCount => View.Count;

        public ObservableCollection<IProduct> View { get; } = new ObservableCollection<IProduct>();

        public void SetSource(IEnumerable<IProduct> source)
        {
            _source = source.ToList();
        }

        public void Refresh()
        {
            Filter(_name, _barcode);
        }

        public void Filter(string name, string barcode)
        {
            _name = name;
            _barcode = barcode;

            var items = _source.Where(p =>  (string.IsNullOrWhiteSpace(_name) || p.Name.Contains(_name, StringComparison.InvariantCultureIgnoreCase)) && (string.IsNullOrWhiteSpace(_barcode) || p.Barcode.Contains(_barcode, StringComparison.InvariantCultureIgnoreCase)));

            View.Clear();
           
            foreach (var item in items.OrderBy(p=>p.Name))
            {
                View.Add(item);
            }

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(FilteredCount));
        }

        public void Remove(IProduct product)
        {
            _source.Remove(product);
            Filter(_name, _barcode);
        }
    }
}
