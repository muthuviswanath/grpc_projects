﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService.Services
{
    public class ProductService : Product.ProductBase
    {
        public override Task<ProductSaveResponse> saveProducts(ProductsModel request, ServerCallContext context) {
            Console.WriteLine($"{request.ProductName} | {request.ProductDescription} | {request.ProductPrice}");
            var result = new ProductSaveResponse {
                StatusCode = 1,
            IsSuccessful = true
        };
        return Task.FromResult(result);
        }

        public override Task<ProductList> GetProducts(Empty request, ServerCallContext context) {
            DateTime stockDate = new DateTime(2023, 12, 19);
            var product1 = new ProductsModel
            {
                ProductName = "Laptop",
                ProductDescription = "Dell Laptop",
                ProductPrice = 23434,
                StockDate = stockDate.ToString("dd-MM-yyyy")
            };
            var product2 = new ProductsModel
            {
                ProductName = "Mouse",
                ProductDescription = "Wireless Mouse",
                ProductPrice = 234,
                StockDate = stockDate.ToString("dd-MM-yyyy")

            };
            var product3 = new ProductsModel
            {
                ProductName = "Projector",
                ProductDescription = "Ericson Laptop",
                ProductPrice = 54434,
                StockDate = stockDate.ToString("dd-MM-yyyy")
            };

            var result = new ProductList();
            result.Products.Add(product1);
            result.Products.Add(product2);
            result.Products.Add(product3);
            return Task.FromResult(result);
        }
    }
}

