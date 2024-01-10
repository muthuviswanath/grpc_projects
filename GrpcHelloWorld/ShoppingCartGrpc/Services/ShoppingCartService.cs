using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingCartGrpc.Data;
using ShoppingCartGrpc.Protos;
using System;
using System.Threading.Tasks;
using ShoppingCartGrpc.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
namespace ShoppingCartGrpc.Services

{
    [Authorize]
    public class ShoppingCartService : ShoppingCartProtoService.ShoppingCartProtoServiceBase
    {
        private readonly ShoppingCartContext _shoppingCartDbContext;
        private readonly DiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly ILogger<ShoppingCartService> _logger;

        public ShoppingCartService(ShoppingCartContext shoppingCartDbContext,
                                    DiscountService discountService, IMapper mapper,
                                    ILogger<ShoppingCartService> logger)
        {

            _shoppingCartDbContext = shoppingCartDbContext ?? throw new ArgumentNullException(nameof(shoppingCartDbContext));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ShoppingCartModel> GetShoppingCart(GetShoppingCartRequest request, ServerCallContext context)
        {
            var shoppingCart = await _shoppingCartDbContext.ShoppingCart.FirstOrDefaultAsync(s => s.UserName == request.Username);
            if (shoppingCart == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Shopping cart for the username : {request.Username} is not found"));
            }
            var shoppingCartModel = _mapper.Map<ShoppingCartModel>(shoppingCart);
            return shoppingCartModel;
        }

        public override async Task<ShoppingCartModel> CreateShoppingCart(ShoppingCartModel request, ServerCallContext context)
        {
            var shoppingCart = _mapper.Map<ShoppingCart>(request);

            var isExist = await _shoppingCartDbContext.ShoppingCart.AnyAsync(s => s.UserName == shoppingCart.UserName);
            if (isExist)
            {
                _logger.LogError($"Username {shoppingCart.UserName} is invalid for shopping cart creation.");
                throw new RpcException(new Status(StatusCode.NotFound, $"Shopping cart with username {request.Username} is not found"));

            }

            _shoppingCartDbContext.ShoppingCart.Add(shoppingCart);
            await _shoppingCartDbContext.SaveChangesAsync();

            _logger.LogInformation($"Shopping Cart is successfully created for the user: {shoppingCart.UserName}");

            var shoppingCartModel = _mapper.Map<ShoppingCartModel>(shoppingCart);
            return shoppingCartModel;
        }
        [AllowAnonymous]
        public override async Task<AddItemsIntoShoppingCartResponse> AddItemsIntoShoppingCart(IAsyncStreamReader<AddItemsIntoShoppingCartRequest> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext()) {
                var shoppingCart = await _shoppingCartDbContext.ShoppingCart.FirstOrDefaultAsync(s => s.UserName == requestStream.Current.Username);
                if (shoppingCart != null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"Shopping Cart with the username : {requestStream.Current.Username} is not found"));

                }

                var newAddedCartItem = _mapper.Map<ShoppingCartItem>(requestStream.Current.NewCartItem);
                var cartItem = shoppingCart.Items.FirstOrDefault(i => i.ProductId == newAddedCartItem.ProductId);
                if (null != cartItem)
                {
                    cartItem.Quantity++;
                }
                else {
                    var discount = await _discountService.GetDiscount(requestStream.Current.DiscountCode);
                    newAddedCartItem.Price -= discount.Amount;
                    shoppingCart.Items.Add(newAddedCartItem);
                }
            }
            var insertCount = await _shoppingCartDbContext.SaveChangesAsync();
            var response = new AddItemsIntoShoppingCartResponse
            {
                Success = insertCount > 0,
                InsertCount = insertCount
            };
            return response;
        }
        [AllowAnonymous]
        public override async Task<RemoveItemsFromShoppingCartResponse> RemoveItemsFromShoppingCart(RemoveItemsFromShoppingCartRequest request, ServerCallContext context)
        {
            var shoppingCart = await _shoppingCartDbContext.ShoppingCart.FirstOrDefaultAsync(s => s.UserName == request.Username);
            if (shoppingCart == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Shopping cart for the user {request.Username} not found"));
            }
            var removeCartItem = shoppingCart.Items.FirstOrDefault(i => i.ProductId == request.RemoveCartItem.ProductId);
            if (removeCartItem == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Shopping cart item with the product id: {request.RemoveCartItem.ProductId} not found"));

            }

            shoppingCart.Items.Remove(removeCartItem);

            var removeCount =await _shoppingCartDbContext.SaveChangesAsync();
            var response = new RemoveItemsFromShoppingCartResponse
            {
                Success = removeCount > 0
            };

            return response;
        }
    }
}
