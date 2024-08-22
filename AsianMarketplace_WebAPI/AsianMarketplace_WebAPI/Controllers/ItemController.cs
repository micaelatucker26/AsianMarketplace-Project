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
    public class ItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IItemRepo _itemRepo;

        public ItemController(IMapper mapper, IItemRepo itemRepo)
        {
            _mapper = mapper;
            _itemRepo = itemRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDTO itemDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newItem = _mapper.Map<Item>(itemDTO);

                // Validate the itemDTO.....
                if(itemDTO.Quantity < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The item quantity must be greater than or equal to zero." });
                }
                else if(itemDTO.Price < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The item price must be greater than or equal to zero." });
                }
                else if(itemDTO.ImageUrl == "")
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The item must have an Image URL." });
                }

                // Add the new item to the context
                await _itemRepo.CreateItem(newItem);

                // Return that item's details (including itemId, name, description, quantity, price, imageURL, and subcategory name)
                return
                    CreatedAtAction(nameof(GetItem),
                    new { itemId = newItem.ItemId }, itemDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating an item.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            try
            {
                // Gather items into a list
                var items = await _itemRepo.GetItems();
                if(items == null)
                {
                    return NotFound();
                }
                // Map the list of items to the DTO
                var itemDTOs = _mapper.Map<List<ItemDTO>>(items);
                return Ok(itemDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid itemId)
        {
            // Fetch the existing item from the database
            var item = await _itemRepo.GetItem(itemId);
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                // Map that item to the DTO
                var itemDTO = _mapper.Map<ItemDTO>(item);
                return Ok(itemDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem( Guid itemId, [FromBody] ItemDTO itemDTO)
        {
            try
            {
                // Validate the itemDTO.....
                if (itemDTO.Quantity < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The item quantity must be greater than or equal to zero." });
                }
                else if (itemDTO.Price < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The item price must be greater than or equal to zero." });
                }
                else if (itemDTO.ImageUrl == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The item must have an Image URL." });
                }
                // Fetch the existing item from the database
                var item = await _itemRepo.UpdateItem(itemId, itemDTO);
                if (item == null)
                {
                    return NotFound();
                }
                // Return a response
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            try
            {
                // Fetch the existing item from the database
                var item = await _itemRepo.DeleteItem(itemId);
                if (item == null)
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
