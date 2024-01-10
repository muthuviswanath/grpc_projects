using AutoMapper;
using ShoppingCartGrpc.Protos;
using ShoppingCartGrpc.Models;
using System;
namespace ProductGrpc.Mappers

{
    public class ShoppingCartProfile:Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartModel>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemModel>().ReverseMap();
        }
    }
}
