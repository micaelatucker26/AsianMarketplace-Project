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
            try
            {
                // Create a new Identity User that will identify the user whose password is hashed
                var user = new IdentityUser { UserName = shopperDTO.Username };

                // Create a password hasher
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

                // Return a success message for successful creation of shopper/user
                return
                    CreatedAtAction(nameof(GetShopper),
                    new { username = newUser.Username }, "Success");
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating a user.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
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
            try
            {
                // Gather shoppers into a list
                var users = await _marketplaceDbContext.Shoppers.ToListAsync();
                if(users == null)
                {
                    return NotFound();
                }
                // Map the list of shoppers/users to the DTO
                var shopperDTOs = _mapper.Map<List<ShopperDTO>>(users);
                return Ok(shopperDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ShopperDTO>> GetShopper(string username)
        {
            // Fetch the existing shopper from the database
            var shopper = await _marketplaceDbContext.Shoppers
                .FirstOrDefaultAsync(s => s.Username == username);
            if (shopper == null)
            {
                return NotFound();
            }
            try
            {
                // Map that shopper to the DTO
                var shopperDTO = _mapper.Map<ShopperDTO>(shopper);
                return Ok(shopperDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
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

            if (!string.IsNullOrEmpty(shopperDTO.Password))
            {
                var shopper = new IdentityUser { UserName = user.Username };
                var passwordHasher = new PasswordHasher<IdentityUser>();
                // Hash the new password
                string hashedPassword = passwordHasher.HashPassword(shopper, shopperDTO.Password);
                // Assign the hashed password to the new shopper
                user.Password = hashedPassword;
            }
            try
            {
                // Update the context with the new user information
                _marketplaceDbContext.Shoppers.Update(user);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return a response
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
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
            try
            {
                // Remove that shopper from the database
                _marketplaceDbContext.Shoppers.Remove(shopper);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
