namespace Shoppe.Api.Models
{
    public record CartUpdateRequest(string UserId, IEnumerable<ProductSlim> Products);
}
