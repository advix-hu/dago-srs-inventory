using System.ComponentModel;

namespace SRSInventory.Interfaces
{
    public interface IProduct : ICloneable
    {
        int Id { get; }
        string Name { get; set; }
        string Barcode { get; set; }
        decimal Price { get; set; }
        decimal Quantity { get; set; }

        bool Modified { get; }

        void Update(IProduct product);
    }
}
