using Dreamer.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dreamer.Services.CartService
{
    public interface IPurchaseReturnService
    {
        event Action OnChange;
        Task AddToCart(CartItem item);
        Task<List<CartItem>> GetCartItems();
        Task DeleteItem(CartItem item);
        Task EmptyCart();
    }
}
