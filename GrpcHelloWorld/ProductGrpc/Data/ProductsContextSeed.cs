using ProductGrpc.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductGrpc.Data
{
    public class ProductsContextSeed
    {
        public static void SeedAsync(ProductsContext productContext) {
            if (!productContext.Products.Any()) {
                var products = new List<Product> {
                    new Product{
                         ProductId = 1,
                         Name= "Mouse",
                         Description= "Wireless Mouse",
                         Price = 456,
                         Status = ProductGrpc.Models.ProductStatus.INSTOCK,
                         CreatedTime=DateTime.UtcNow
                    },
                    new Product{
                         ProductId = 2,
                         Name= "Projector",
                         Description= "Epson Projector",
                         Price = 23456,
                         Status = ProductGrpc.Models.ProductStatus.INSTOCK,
                         CreatedTime=DateTime.UtcNow
                    },
                    new Product{
                         ProductId = 3,
                         Name= "Digital Slate",
                         Description= "Dell Digital Slate",
                         Price = 4356,
                         Status = ProductGrpc.Models.ProductStatus.INSTOCK,
                         CreatedTime=DateTime.UtcNow
                    }
                };
                productContext.Products.AddRange(products);
                productContext.SaveChanges();
            }
        }
    }
}
