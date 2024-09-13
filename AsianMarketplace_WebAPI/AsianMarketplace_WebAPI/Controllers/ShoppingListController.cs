using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IShoppingListRepo _shoppingListRepo;
        private readonly IShopperRepo _shopperRepo;

        public ShoppingListController(IMapper mapper, IShoppingListRepo shoppingListRepo, IShopperRepo shopperRepo)
        {
            _mapper = mapper;
            _shoppingListRepo = shoppingListRepo;
            _shopperRepo = shopperRepo;
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> CreateShoppingList(string username, [FromBody] ShoppingListDTO shoppingListDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //Map the DTO to the entity
                var newShoppingList = _mapper.Map<ShoppingList>(shoppingListDTO);

                var user = await _shopperRepo.GetShopper(username);

                newShoppingList.User = user;
                newShoppingList.UserId = user.UserId;

                // Add the new shopping list to the context
                await _shoppingListRepo.CreateShoppingList(newShoppingList);

                //Return that shopping list's details (including title, isactive, userId, and date created)
                return
                    CreatedAtAction(nameof(GetShoppingList),
                    new { title = newShoppingList.Title, userId = newShoppingList.UserId }, shoppingListDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating a shopping list.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDTO>>> GetShoppingLists()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //Gather shopping lists into a list
                var lists = await _shoppingListRepo.GetShoppingLists();
                if (lists == null)
                {
                    return NotFound();
                }
                //Map the list of shopping lists to the DTO
               var shoppingListDTOs = _mapper.Map<List<ShoppingListDTO>>(lists);
                return Ok(shoppingListDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }


        [HttpGet("{title}/{username}")]
        public async Task<ActionResult<ShoppingListDTO>> GetShoppingList(string title, string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _shopperRepo.GetShopper(username);
            if(user == null)
            {
                return NotFound();
            }

            //Fetch the existing shopping list from the database
            var shoppingList = await _shoppingListRepo.GetShoppingList(title, user.UserId);
            if (shoppingList == null)
            {
                return NotFound();
            }
            try
            {
                //Map that shopping list to the DTO
               var shoppingListDTO = _mapper.Map<ShoppingListDTO>(shoppingList);
                return Ok(shoppingListDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{title}/{username}")]
        public async Task<IActionResult> UpdateShoppingList(string title, string username, [FromBody] ShoppingListDTO shoppingListDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _shopperRepo.GetShopper(username);
                if (user == null)
                {
                    return NotFound();
                }
                // Fetch the existing shopping list from the database
                var shoppingList = await _shoppingListRepo.GetShoppingList(title, user.UserId);
                if (shoppingList == null)
                {
                    return NotFound();
                }
                // Update the context with the new shopping list information
                var newShoppingList = await _shoppingListRepo.UpdateShoppingList(title, user.UserId, shoppingListDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{title}/{username}")]
        public async Task<IActionResult> DeleteShoppingList(string title, string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _shopperRepo.GetShopper(username);
                if (user == null)
                {
                    return NotFound();
                }
                // Try to delete the shopping list from the database
                var shoppingList = await _shoppingListRepo.DeleteShoppingList(title, user.UserId);
                if (shoppingList == null)
                {
                    return NotFound();
                }
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
