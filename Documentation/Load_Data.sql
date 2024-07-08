-- Category table data
insert into Category (Name) values ('Drinks');
insert into Category (Name) values ('Frozen Foods');
insert into Category (Name) values ('Household Items');
insert into Category (Name) values ('Pantry');
insert into Category (Name) values ('Snacks');
insert into Category (Name) values ('Sweets');

-- SubCategory table data
insert into SubCategory (Name, CategoryName) values ('Coffee', 'Drinks');
insert into SubCategory (Name, CategoryName) values ('Juice', 'Drinks');
insert into SubCategory (Name, CategoryName) values ('Soda', 'Drinks');
insert into SubCategory (Name, CategoryName) values ('Tea', 'Drinks');
insert into SubCategory (Name, CategoryName) values ('Water', 'Drinks');

insert into SubCategory (Name, CategoryName) values ('Microwave Ready', 'Frozen Foods');
insert into SubCategory (Name, CategoryName) values ('Seafood', 'Frozen Foods');
insert into SubCategory (Name, CategoryName) values ('Beef', 'Frozen Foods');
insert into SubCategory (Name, CategoryName) values ('Chicken', 'Frozen Foods');

insert into SubCategory (Name, CategoryName) values ('Kitchenware', 'Household Items');
insert into SubCategory (Name, CategoryName) values ('Merchandise', 'Household Items');

insert into SubCategory (Name, CategoryName) values ('Rice', 'Pantry');
insert into SubCategory (Name, CategoryName) values ('Noodles', 'Pantry');
insert into SubCategory (Name, CategoryName) values ('Bases and Starters', 'Pantry');
insert into SubCategory (Name, CategoryName) values ('Flour', 'Pantry');
insert into SubCategory (Name, CategoryName) values ('Tinned Foods', 'Pantry');
insert into SubCategory (Name, CategoryName) values ('Spices and Seasonings', 'Pantry');
insert into SubCategory (Name, CategoryName) values ('Sugar', 'Pantry');

insert into SubCategory (Name, CategoryName) values ('Crackers', 'Snacks');
insert into SubCategory (Name, CategoryName) values ('Chips', 'Snacks');
insert into SubCategory (Name, CategoryName) values ('Nuts', 'Snacks');

insert into SubCategory (Name, CategoryName) values ('Mochi', 'Sweets');
insert into SubCategory (Name, CategoryName) values ('Chocolates', 'Sweets');
insert into SubCategory (Name, CategoryName) values ('Candies', 'Sweets');

-- Shopper table data
insert into Shopper (Username, Password, Salt) values ('MTucker27', 'sq@kmk_12l','fake_salt');
insert into Shopper (Username, Password, Salt) values ('JHigginbotham26', 'peanuts','another_fake');
insert into Shopper (Username, Password, Salt) values ('DBishop123', 'mushroomKingdom', 'faker');

-- Order table data
insert into [Order] (OrderDate, Username) values (GETDATE(), 'MTucker27');
insert into [Order] (OrderDate, Username) values (GETDATE(), 'JHigginbotham26');
insert into [Order] (OrderDate, Username) values (GETDATE(), 'DBishop123');

-- Item table data
insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Nongshim Shin Spicy Ramen', 'Here 
are the most popular instant ramyun noodles in South Korea and Japan. Ramyun is the Korean take on ramen and is, most remarkably, 
stronger and bolder tasting than Japanese-style ramen. As to be expected, these Korean spicy noodles are very hot and flavorful, 
with a beef and vegetable-based broth. Add water and cook for 4-7 minutes on the stove or in the microwave. Here are the most 
popular instant ramyun noodles in South Korea and Japan. Ramyun is the Korean take on ramen and is, most remarkably, stronger 
and bolder tasting than Japanese-style ramen. As to be expected, these Korean spicy noodles are very hot and flavorful, with a 
beef and vegetable-based broth. Add water and cook for 4-7 minutes on the stove or in the microwave. Common Allergens: Fish, Wheat, 
Soy.', 100, 2.49, 'https://www.bokksumarket.com/products/shin-ramyun-spicy-ramen', 'Noodles');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Hikari White Miso with Bonito Dashi', 'This 
white miso features an irresistible bonito dashi (broth) flavor! Expect a delicious salty, crispy, umami flavor whenever you add it 
to recipes. It is made with kelp and bonito stock, which provides a bold taste. Use this versatile miso for soups, stews, sauces, 
marinades, salad dressings, and more. Bonus - This white miso is gluten-free! Common Allergens: Fish, Soy.)', 50, 9.99, 
'https://www.bokksumarket.com/products/hikari-white-miso-with-bonito-dashi', 'Bases and Starters');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Sangaria Ramune Soda: Lychee', 'Ramune is a 
favorite soda in Japan for a reason: the drink is bursting with fruity flavor, is made with real sugar, and the packaging cannot be 
beaten. To drink this marble soda, use the stopper to push down on the marble, which shoots into the neck of the bottle. It bounces 
up and down as you drink, making a fun tinkling sound. We hope the taste of lychee has you feeling tropical vibes!', 150, 3.99, 
'https://www.bokksumarket.com/products/sangaria-ramune-soda-lychee', 'Soda');

-- OrderItem table data
insert into OrderItem (Price, Quantity, ItemID, OrderID) VALUES (3.99, 3, 
'B3858A04-EAC2-4503-B255-566995032A83', 'C90CEF64-962B-4154-B47D-69DFB7E8B391');

-- ShoppingList table data
insert into ShoppingList (Title, IsActive, DateCreated, UserID) VALUES 
('Weekend Shopping List', 'Y', GETDATE(), 'DBishop123');

-- ShoppingListItem table data
insert into ShoppingListItem (IsCrossedOff, Quantity, Title, UserID, ItemID) values
(NULL, 1, 'Weekend Shopping List', 'DBishop123', 'AA0C9F06-CDAC-40D3-AB3A-F26B953F00BC');

-- CartItem table data
insert into CartItem (Quantity, ItemID, UserID) 
values ( 5, 'B3858A04-EAC2-4503-B255-566995032A83', 'DBishop123');

insert into CartItem (Quantity, ItemID, UserID) 
values ( 2, 'AA0C9F06-CDAC-40D3-AB3A-F26B953F00BC', 'MTucker27');
