﻿namespace Shoppe.Api.Models
{
    /* Include simple DTOs in here as records. This helps keeping the code concise while making sure that 
       we are creating immutable data. */

    public record Product(string Code, string Description, string ImageUrl, float Price, float Quantity = 0);

    public record Country(string Code, string Name, string CurrencySym, float FxRate);
}