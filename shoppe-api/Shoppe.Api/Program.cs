using FluentValidation;
using Shoppe.Api.Models.Configs;
using Shoppe.Api.Models.Validators;
using Shoppe.Api.Repositories;
using Shoppe.Api.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var corsUrls = builder.Configuration.GetSection("Cors:UrlsAllowed").Get<string[]>();
builder.Services.AddCors(action =>
    action.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(corsUrls);
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    }));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddOptions<CoreSettings>()
    .Configure(opts => opts.AssetsUrl = builder.Configuration.GetValue<string>("AssetsUrl"));

builder.Services.AddMemoryCache();

// Adds all Validators to evaluate the incoming requests.
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ICountryService, CountryService>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ICountryRepository, CountryRepository>();
builder.Services.AddSingleton<ICartRepository, CartRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

app.Run();
