namespace DemoProjectECommerce.Models.Domain
{
    public class Product
    {
        public Guid productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productPrice { get; set; }
        public string productType { get; set; }
        public string? productImageUrl { get; set; }
        public int productQuantity { get; set; }    
    }
}
