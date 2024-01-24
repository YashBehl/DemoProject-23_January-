namespace DemoProjectECommerce.Models.Domain
{
    public class User
    {
        public Guid userId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        private string userPassword { get; set; }
        public int userRole {  get; set; }
        public string userAddress { get; set; }
    }
}
