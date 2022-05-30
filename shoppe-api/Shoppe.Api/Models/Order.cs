namespace Shoppe.Api.Models
{
    public class Order
    {
        public string OrderId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<ProductSlim> Products { get; set; }

        public DateTime DatePlaced { get; set; }

        public Order() { }

        public Order(PlaceOrderRequest request)
        {
            OrderId = Guid.NewGuid().ToString();
            UserId = request.UserId;
            Products = request.Products;
            DatePlaced = DateTime.UtcNow;
        }
    }
}
