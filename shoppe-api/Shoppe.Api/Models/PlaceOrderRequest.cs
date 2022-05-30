namespace Shoppe.Api.Models
{
    public record PlaceOrderRequest(string UserId, IEnumerable<ProductSlim> Products);
}
