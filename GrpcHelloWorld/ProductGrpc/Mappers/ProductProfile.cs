using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using ProductGrpc.Protos;
using System;
namespace ProductGrpc.Mappers

{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, ProductModel>()
                .ForMember(dest => dest.CreatedTime,
                            opt => opt.MapFrom(src => Timestamp.FromDateTime(src.CreatedTime)));

            CreateMap<ProductModel, Models.Product>()
                .ForMember(dest => dest.CreatedTime,
                            opt => opt.MapFrom(src => src.CreatedTime.ToDateTime()));


        }
    }
}
