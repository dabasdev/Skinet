using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData =
                        await File.ReadAllTextAsync("../Infrastructure" + @"/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    await context.ProductBrands.AddRangeAsync(brands);
                }


                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        await File.ReadAllTextAsync("../Infrastructure" + @"/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    await context.ProductTypes.AddRangeAsync(types);
                }


                if (!context.Products.Any())
                {
                    var productData =
                        await File.ReadAllTextAsync("../Infrastructure" + @"/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    await context.Products.AddRangeAsync(products);
                }

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}