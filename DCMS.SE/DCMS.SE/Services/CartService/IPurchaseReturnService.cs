using DCMS.SE.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.SE.Services.CartService
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
