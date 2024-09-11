using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.DTOs.Responses;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using AsianMarketplace_WebAPI.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper
var config = new MapperConfiguration(cfg => 
    { 
        cfg.CreateMap<CartItem, CartItemDTO>().ReverseMap();
        cfg.CreateMap<CartItem, CartItemResponseDTO>().ReverseMap();
        cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
        cfg.CreateMap<Item, ItemDTO>().ReverseMap();
        cfg.CreateMap<Order, OrderDTO>().ReverseMap();
        cfg.CreateMap<Order, OrderResponseDTO>().ReverseMap();
        cfg.CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        cfg.CreateMap<Shopper, ShopperDTO>().ReverseMap();
        cfg.CreateMap<ShoppingList, ShoppingListDTO>().ReverseMap();
        cfg.CreateMap<ShoppingListItem, ShoppingListItemDTO>().ReverseMap();
        cfg.CreateMap<SubCategory, SubCategoryDTO>().ReverseMap();
    });
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddDbContext<AsianMarketplaceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
     .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
     });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICartItemRepo, CartItemRepository>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();
builder.Services.AddScoped<IItemRepo, ItemRepository>();
builder.Services.AddScoped<IOrderRepo, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepo, OrderItemRepository>();
builder.Services.AddScoped<IShopperRepo, ShopperRepository>();
builder.Services.AddScoped<IShoppingListRepo, ShoppingListRepository>();
builder.Services.AddScoped<ISubCategoryRepo, SubCategoryRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
