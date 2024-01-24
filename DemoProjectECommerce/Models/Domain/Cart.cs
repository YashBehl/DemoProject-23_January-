namespace DemoProjectECommerce.Models.Domain
{
    public class Cart
    {
        public Guid cartId { get; set; }
        public Guid productId { get; set; }
        public Guid userId { get; set; }
    }
}
