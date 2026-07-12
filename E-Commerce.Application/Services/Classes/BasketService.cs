using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var basketResult = await basketRepository.CreateOrUpdateBasketAsync(customerBasket, ct:ct);
            return basketResult != null ? Result<BasketDto>.Ok(mapper.Map<BasketDto>(basketResult)) : Result<BasketDto>.Fail(Error.Failure("Can't create or update basket"));
        }
        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken ct = default)
        {
            var result = await basketRepository.DeleteBasketAsync(id, ct:ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Can't delete basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string userId, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(userId, ct:ct);
            if(basket == null) return Result<BasketDto>.Fail(Error.NotFound("Basket not found"));

            return mapper.Map<BasketDto>(basket);

        }
    }
}
