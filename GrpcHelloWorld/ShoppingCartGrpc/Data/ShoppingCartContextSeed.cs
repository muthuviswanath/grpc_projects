using Microsoft.AspNetCore.Components.Web;
using ShoppingCartGrpc.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartGrpc.Data
{
    public class ShoppingCartContextSeed
    {
        public static void SeedAsync(ShoppingCartContext shoppingCartContext)
        {
            if (!shoppingCartContext.ShoppingCart.Any())
            {

                var shoppingCarts = new List<ShoppingCart>
                {

                    new ShoppingCart
                    {
                        UserName = "muthu",
                        Items = new List<ShoppingCartItem>
                        {
                            new ShoppingCartItem
                            {
                                Quantity = 10,
                                Price = 456,
                                ProductId = 1,
                                ProductName = "Mouse"
                            },
                            new ShoppingCartItem
                            {
                                Quantity = 10,
                                Price = 23456,
                                ProductId = 2,
                                ProductName = "Projector"
                            },
                            new ShoppingCartItem
                            {
                                Quantity = 10,
                                Price = 4356,
                                ProductId = 3,
                                ProductName = "Digital Slate"
                            }
                        }
                    }
                };
                shoppingCartContext.ShoppingCart.AddRange(shoppingCarts);
                shoppingCartContext.SaveChanges();
            }
        }
    }
}
