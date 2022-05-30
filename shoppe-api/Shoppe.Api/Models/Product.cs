namespace Shoppe.Api.Models
{
    public record Product(string Code, string Description, string ImageUrl, float Price, int MaxAvailable, int Quantity = 0)
        : ProductSlim(Code, Price, MaxAvailable, Quantity);
}
