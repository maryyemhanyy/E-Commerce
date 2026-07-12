using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    
    public class BasketController(IBasketService basketService) : APIBaseController
    {
        #region Get
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasketDto>>GetBasket(string id , CancellationToken ct)
        {
            var basket = await basketService.GetBasketAsync(id , ct);
           
            return ToActionResult(basket);
        }
        #endregion

        #region Create or Update
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto , CancellationToken ct)
        {
            var basket = await basketService.CreateOrUpdateBasketAsync(basketDto , ct);
            return ToActionResult(basket);
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id , CancellationToken ct)
        {
            var result = await basketService.DeleteBasketAsync(id , ct);
            return ToActionResult(result);
        }
        #endregion
    }
}
