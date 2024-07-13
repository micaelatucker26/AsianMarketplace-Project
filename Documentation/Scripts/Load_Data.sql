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
insert into Shopper (Username, Password, Salt) values ('ALord35','dar2thV3ader', 'so-fake');

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

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Moshi Sparkling Juice Drink: Original Yuzu', 'Maker Moshi is here to bring the
taste of yuzu to you! Made with 100% yuzu juice sourced straight from Japan, each refreshing sip of this sparkling juice is bursting with the full fruity aroma and 
tartness of yuzu. There may be a “yuzu halo” or a ring of the all-natural yuzu essential oils at the top of the bottle, which are totally safe to 
consume - just give it a shake!', 150, 3.99, 
'https://www.bokksumarket.com/collections/juice/products/moshi-sparkling-drink-original-yuzu', 'Juice');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Koda Mochiko Sweet Rice Flour', 'Introduced in the 1940s, Koda Farms’ 
mochiko (sweet rice flour) is the longest-selling brand of sweet rice flour in America. Known for its sticky chewy texture, mochiko is very popular for making 
homemade mochi, manju, chi-chi-dango, senbei, and other Japanese snacks: both savory and sweet! You can also use it to add a unique Japanese twist to your 
everyday waffles, cakes, and muffins. Whatever you’re in the mood for, mochiko will surely make your life a little sweeter!', 75, 4.99, 
'https://www.bokksumarket.com/collections/flour/products/mochiko-sweet-rice-flour', 'Flour');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('J-Basket Arare Rice Crackers: Norimaki Seaweed', 'This snack
features tasty arare (rice crackers) tightly wrapped in nori, or seaweed. The combination of the crispy and savory crackers and the crunchy, umami﻿-laden 
seaweed is out of this world!', 50, 6.49, 
'https://www.bokksumarket.com/collections/crackers/products/arare-rice-crackers-norimaki-seaweed', 'Crackers');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Marufuji Makisu Sushi Bamboo Rolling Mat', 'Ready to make your 
own sushi? Then you’ll need the right equipment! In addition to being the material traditionally used for sushi rolling mats, bamboo is sustainable 
and elegant. Unlike wood, it absorbs less water and doesn’t scratch surfaces!', 100, 2.99, 
'https://www.bokksumarket.com/collections/all-household/products/makisu-sushi-bamboo-rolling-mat', 'Kitchenware');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Shirakiku Char Siu BBQ Pork Buns (3 pieces)', 'These yummy char siu BBQ 
pork buns will transport you to a dim sum restaurant with one bite! They feature a delicious soft pillowy exterior and a flavorful BBQ pork filling. Each pack 
comes with three delicious pork buns to enjoy. Steam them for a more authentic meal. Bonus - You can heat them in a microwave for a quick meal or snack!', 125, 8.99, 
'https://www.bokksumarket.com/collections/microwave-ready/products/shirakiku-char-siu-bbq-pork-buns-3-pieces', 'Microwave Ready');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Kubota Daifuku Mochi: Peach', 'In Japan, nothing says summertime 
vibes like peaches! Enjoy the sweet flavor of juicy peaches in the creamy paste of this soft and chewy daifuku mochi. It can’t be summer all the time, but 
it can taste like it! This mochi will surely take a peach of your heart.', 50, 6.99, 
'https://www.bokksumarket.com/collections/mochi/products/daifuku-mochi-peach', 'Mochi');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Fujii Niigata Iwafune Koshihikari Rice: 4.4 lb', 'Are you ready to 
expand your rice palate? This unique short-grain rice is harvested in Iwafune of Niigata prefecture. It’s known for having a more chewy texture and less 
stickiness than other short-grain rice. Easy to cook, easy to eat, and easy to love! But, don''t forget to wash it first!', 75, 29.99, 
'https://www.bokksumarket.com/collections/rice/products/fujii-niigata-iwafune-koshihikari-rice', 'Rice');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Boiled Sushi Shrimp (30 pieces)', 'When making your own sushi, you 
deserve shrimply the best ingredients! This butterfly shrimp is pre-cooked, peeled, deveined, and comes to you frozen to preserve freshness. Plus, the 
tails are left on, so it will look aesthetically pleasing in addition to being totally delicious. You can even fry them to make shrimp tempura.', 125, 14.99, 
'https://www.bokksumarket.com/collections/frozen-seafood/products/boiled-sushi-shrimp-30-pieces', 'Seafood');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Ajishima Furikake Rice Seasoning: Seto Fumi', 'Is your bowl of rice a little...bland? 
Put down that soy sauce! Meet furikake: a sweet-salty-umami blend of crunchy seaweed bits, sesame seeds, bonito flakes, salt, and tasty spices. Fish and seaweed are bringing 
the umami to this party, so invite your seafood-loving friends! Sprinkle it for presentation or taste. Just make sure it’s after your rice is plated.', 50, 4.99, 
'https://www.bokksumarket.com/collections/spices-seasonings/products/furikake-rice-seasoning-seto-fumi', 'Spices and Seasonings');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Yu Yee Bing Tang Yellow Rock Sugar', 'Since its origins in the 
Tang Dynasty of China in the seventh century A.D, rock sugar continues to play a big role in the irresistible taste and appearance of Chinese cuisine. 
In addition to the obvious sweet baked treats like Su-style moon cakes and Nian Gao, rock sugar shines in savory applications. It balances out the salty 
and sour flavors of savory cuisine to create a harmonious flavor in addition to a beautiful translucent shine. Try it in recipes for Shanghai pork belly, 
Hainanese chicken rice, oxtail, and braised roasted duck.', 75, 3.49, 
'https://www.bokksumarket.com/collections/sugar/products/yu-yee-bing-tang-yellow-rock-sugar', 'Sugar');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Evergreen Bubble Milk Tea with Tapioca: Brown Sugar', 'Satisfy your boba cravings 
wherever and whenever with this canned bubble tea! Picture this: real tapioca boba (with the chewy texture you know and love) in a deliciously brewed brown sugar milk tea. 
Sweet, creamy, and always fresh - you’ll be saying sip-sip hooray!', 150, 3.49, 
'https://www.bokksumarket.com/collections/tea/products/evergreen-bubble-milk-tea-with-tapioca-black-sugar', 'Tea');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Wang Tinned Napa Cabbage Kimchi', 'Kimchi began as a way 
to preserve vegetables using a fermentation process. Now, it''s a Korean staple in many households! This crunchy napa cabbage is a delicious way 
to add heat and a tangy taste to any dish. The best part? You can add kimchi to anything! Try it on rice, savory pancakes, and congee.', 75, 3.49, 
'https://www.bokksumarket.com/collections/canned/products/wang-napa-cabbage-kimchi-in-can', 'Tinned Foods');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryName) values ('Ocean Bomb Sailor Moon Sparkling Water: Pomelo', 'Not quite soda, not quite plain 
sparkling water - this delicious pomelo-flavored drink is the perfect summer beverage. It has less sugar than regular soda pop but a sweeter taste than other sparkling water 
options. Join Sailor Moon on her adventures with each sip of this yummy drink! Each flavor from this collaboration features a different Sailor Moon character. Try to collect 
all the characters by trying different flavors!', 150, 3.99, 
'https://www.bokksumarket.com/collections/water/products/ocean-bomb-sailor-moon-sparkling-water-pomelo', 'Water');

-- OrderItem table data
insert into OrderItem (Price, Quantity, ItemID, OrderID) VALUES (3.99, 3, 
'B3858A04-EAC2-4503-B255-566995032A83', 'C90CEF64-962B-4154-B47D-69DFB7E8B391');

-- ShoppingList table data
insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Weekend Shopping List', 'Y', GETDATE(), 'DBishop123');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Essentials', 'Y', GETDATE(), 'ALord35');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('My List', 'N', GETDATE(), 'ALord35');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Friday List', 'N', GETDATE(), 'ALord35');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Monday List', 'N', GETDATE(), 'ALord35');

-- ShoppingListItem table data
insert into ShoppingListItem (IsCrossedOff, Quantity, Title, UserID, ItemID) values
(NULL, 1, 'Weekend Shopping List', 'DBishop123', 'AA0C9F06-CDAC-40D3-AB3A-F26B953F00BC');

-- CartItem table data
insert into CartItem (Quantity, ItemID, UserID) 
values ( 1, 'AA0C9F06-CDAC-40D3-AB3A-F26B953F00BC', 'DBishop123');

insert into CartItem (Quantity, ItemID, UserID) 
values ( 2, 'AA0C9F06-CDAC-40D3-AB3A-F26B953F00BC', 'MTucker27');

insert into CartItem (Quantity, ItemID, UserID) 
values ( 6, '039E69F7-7326-4F29-AEDA-2F51EC3E29C3', 'DBishop123');

insert into CartItem (Quantity, ItemID, UserID)
values ( 3, 'B3858A04-EAC2-4503-B255-566995032A83', 'DBishop123');

 insert into CartItem (Quantity, ItemID, UserID)
 values (10, '3D9E50F2-7CD7-47E1-A144-90EDD25BFBF3' 'JHigginbotham26');

 insert into CartItem (Quantity, ItemID, UserID)
 values (5, 'AA0C9F06-CDAC-40D3-AB3A-F26B953F00BC', 'JHigginbotham26');
