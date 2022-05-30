namespace Shoppe.Api.Models
{
    public class Cart
    {
        public IEnumerable<ProductSlim> Products { get; init; } = Enumerable.Empty<ProductSlim>();

        public float Total => Products.Any()
                    ? Products.Select(p => p.Price * p.Quantity).Sum()
                    : 0;

        public float ShippingCost
        {
            get
            {
                if (Total > 0 && Total <= 50)
                    return 10;
                else if (Total > 50)
                    return 20;
                else
                    return 0;
            }
        }
    }
}
