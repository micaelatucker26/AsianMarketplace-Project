using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopperController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<IdentityUser> passwordHasher;

        public ShopperController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShopper([FromBody] ShopperDTO shopperDTO)
        {
            var user = new IdentityUser { UserName = shopperDTO.Username };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            // Hash the password
            string hashedPassword = passwordHasher.HashPassword(user, shopperDTO.Password);

            // Map the DTO to the entity
            var newUser = _mapper.Map<Shopper>(shopperDTO);

            // Assign the hashed password to the new shopper
            newUser.Password = hashedPassword;

            // Add the new shopper to the context
            _marketplaceDbContext.Shoppers.Add(newUser);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return
                CreatedAtAction(nameof(GetShopper),
                new { username = newUser.Username }, "Success");

            // Verify the password
            //PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, hashedPassword, password);

            //if (result == PasswordVerificationResult.Success)
            //{
            //    Console.WriteLine("Password verification succeeded.");
            //}
            //else
            //{
            //    Console.WriteLine("Password verification failed.");
            //}
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopperDTO>>> GetShoppers()
        {
            var users = await _marketplaceDbContext.Shoppers.ToListAsync();
            var shopperDTOs = _mapper.Map<List<ShopperDTO>>(users);
            return Ok(shopperDTOs);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ShopperDTO>> GetShopper(string username)
        {
            var shopper = await _marketplaceDbContext.Shoppers
                .FirstOrDefaultAsync(s => s.Username == username);
            if (shopper == null)
            {
                return NotFound();
            }

            var shopperDTO = _mapper.Map<ShopperDTO>(shopper);
            return Ok(shopperDTO);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateShopper(string username, [FromBody] ShopperDTO shopperDTO)
        {
            // Fetch the existing user from the database
            var user =  await _marketplaceDbContext.Shoppers.FindAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            //user.Username = shopperDTO.Username;

            if (!string.IsNullOrEmpty(shopperDTO.Password))
            {
                var shopper = new IdentityUser { UserName = user.Username };
                var passwordHasher = new PasswordHasher<IdentityUser>();
                // Hash the new password
                string hashedPassword = passwordHasher.HashPassword(shopper, shopperDTO.Password);
                // Assign the hashed password to the new shopper
                user.Password = hashedPassword;
            }
            _marketplaceDbContext.Shoppers.Update(user);
            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync(); 
           
            // return a response
            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteShopper(string username)
        {
            // Fetch the existing user from the database
            var shopper = await _marketplaceDbContext.Shoppers.FindAsync(username);
            if (shopper == null)
            {
                return NotFound();
            }
            _marketplaceDbContext.Shoppers.Remove(shopper);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
