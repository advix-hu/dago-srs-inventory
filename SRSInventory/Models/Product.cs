using SQLite;
using SRSInventory.Interfaces;

namespace SRSInventory.Models
{
    [Table("Products")]
    public sealed class Product : IProduct
    {
        [Column("Id")]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Barcode")]
        public string Barcode { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("Quantity")]
        public decimal Quantity { get; set; }

        public bool Modified { get; private set; }

        public object Clone()
        {
            var clone = new Product();
            clone.Update(this);
            clone.Modified = false;
            return clone;
        }

        public override string ToString()
        {
            return $"{Name} ({Barcode}) {Price} Ft [{Quantity} db]";
        }

        public void Update(IProduct product)
        {
            Id = product.Id;
            Name = product.Name;
            Barcode = product.Barcode;
            Price = product.Price;
            Quantity = product.Quantity;
            Modified = true;
        }
    }
}
