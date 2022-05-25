using DCMS.SE.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.SE.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItem item);
        Task AddToWishCart(CartItem item);
        Task<List<CartItem>> GetCartItems();
        Task<List<CartItem>> GetCartWishItems();
        Task DeleteItem(CartItem item);
        Task DeleteWishItem(CartItem item);
        Task EmptyCart();
        Task EmptyWishCart();
    }
}
