using AsianMarketplace_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsianMarketplace_WebAPI.Tests.Services
{
    public class CartItemService
    {
        private AsianMarketplaceDbContext _context;
        public CartItemService(AsianMarketplaceDbContext context)
        {
            _context = context;
        }

        public List<CartItem> GetAllCartItems()
        {
            var query = from ci in _context.CartItems
                        select ci;

            return query.ToList();
        }
    }
}
