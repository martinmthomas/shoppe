namespace Shoppe.Api.Models
{
    public record ProductSlim(string Code, float Price, int MaxAvailable, int Quantity = 0);
}
