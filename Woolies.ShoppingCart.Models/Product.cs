namespace Woolies.Shopping.Models
{

    public class Product 
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; } 
    }

    public class TrollyProduct 
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class ProductQuantity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
