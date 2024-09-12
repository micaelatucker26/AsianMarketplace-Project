-- Category table data
insert into Category (Name) values ('Drinks');
insert into Category (Name) values ('Frozen Foods');
insert into Category (Name) values ('Household Items');
insert into Category (Name) values ('Pantry');
insert into Category (Name) values ('Snacks');
insert into Category (Name) values ('Sweets');

-- SubCategory table data
insert into SubCategory (Name, CategoryID) values ('Coffee', '3A158FBA-F2B2-4451-A08E-728583DC6614');
insert into SubCategory (Name, CategoryID) values ('Juice', '3A158FBA-F2B2-4451-A08E-728583DC6614');
insert into SubCategory (Name, CategoryID) values ('Soda', '3A158FBA-F2B2-4451-A08E-728583DC6614');
insert into SubCategory (Name, CategoryID) values ('Tea', '3A158FBA-F2B2-4451-A08E-728583DC6614');
insert into SubCategory (Name, CategoryID) values ('Water', '3A158FBA-F2B2-4451-A08E-728583DC6614');

insert into SubCategory (Name, CategoryID) values ('Microwave Ready', 'E41B4ED8-83B3-4E07-B444-66623B5F37E0');
insert into SubCategory (Name, CategoryID) values ('Seafood', 'E41B4ED8-83B3-4E07-B444-66623B5F37E0');
insert into SubCategory (Name, CategoryID) values ('Beef', 'E41B4ED8-83B3-4E07-B444-66623B5F37E0');
insert into SubCategory (Name, CategoryID) values ('Chicken', 'E41B4ED8-83B3-4E07-B444-66623B5F37E0');

insert into SubCategory (Name, CategoryID) values ('Kitchenware', '576D277F-C99E-42D3-81B9-4BBC093876B5');
insert into SubCategory (Name, CategoryID) values ('Merchandise', '576D277F-C99E-42D3-81B9-4BBC093876B5');

insert into SubCategory (Name, CategoryID) values ('Rice', '6F04475F-CBCC-4D37-95AC-35764BCA30A8');
insert into SubCategory (Name, CategoryID) values ('Noodles', '6F04475F-CBCC-4D37-95AC-35764BCA30A8');
insert into SubCategory (Name, CategoryID) values ('Bases and Starters', '6F04475F-CBCC-4D37-95AC-35764BCA30A8');
insert into SubCategory (Name, CategoryID) values ('Flour', '6F04475F-CBCC-4D37-95AC-35764BCA30A8');
insert into SubCategory (Name, CategoryID) values ('Tinned Foods', '6F04475F-CBCC-4D37-95AC-35764BCA30A8');
insert into SubCategory (Name, CategoryID) values ('Spices and Seasonings', '6F04475F-CBCC-4D37-95AC-35764BCA30A8');
insert into SubCategory (Name, CategoryID) values ('Sugar', '6F04475F-CBCC-4D37-95AC-35764BCA30A8');

insert into SubCategory (Name, CategoryID) values ('Crackers', 'A7950456-1275-4849-8489-F588ADBF8C68');
insert into SubCategory (Name, CategoryID) values ('Chips', 'A7950456-1275-4849-8489-F588ADBF8C68');
insert into SubCategory (Name, CategoryID) values ('Nuts', 'A7950456-1275-4849-8489-F588ADBF8C68');

insert into SubCategory (Name, CategoryID) values ('Mochi', '24872633-F1AF-479B-A417-B796FE4298F4');
insert into SubCategory (Name, CategoryID) values ('Chocolates', '24872633-F1AF-479B-A417-B796FE4298F4');
insert into SubCategory (Name, CategoryID) values ('Candies', '24872633-F1AF-479B-A417-B796FE4298F4');

-- Shopper table data
insert into Shopper (Username, Password) values ('MTucker27', 'sq@kmk_12l');
insert into Shopper (Username, Password) values ('JHigginbotham26', 'peanuts');
insert into Shopper (Username, Password) values ('DBishop123', 'mushroomKingdom');
insert into Shopper (Username, Password) values ('ALord35','dar2thV3ader');

-- Order table data
insert into [Order] (OrderDate, UserID) values (GETDATE(), '3E693409-04AC-4C44-9D27-0B75507FCBD3');
insert into [Order] (OrderDate, UserID) values (GETDATE(), '7B21098D-1612-4258-B020-DD20C0A5E973');
insert into [Order] (OrderDate, UserID) values (GETDATE(), 'D48694D0-CE4A-4639-B709-1950843FCAB8');

-- Item table data
insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Nongshim Shin Spicy Ramen', 'Here 
are the most popular instant ramyun noodles in South Korea and Japan. Ramyun is the Korean take on ramen and is, most remarkably, 
stronger and bolder tasting than Japanese-style ramen. As to be expected, these Korean spicy noodles are very hot and flavorful, 
with a beef and vegetable-based broth. Add water and cook for 4-7 minutes on the stove or in the microwave. Here are the most 
popular instant ramyun noodles in South Korea and Japan. Ramyun is the Korean take on ramen and is, most remarkably, stronger 
and bolder tasting than Japanese-style ramen. As to be expected, these Korean spicy noodles are very hot and flavorful, with a 
beef and vegetable-based broth. Add water and cook for 4-7 minutes on the stove or in the microwave. Common Allergens: Fish, Wheat, 
Soy.', 100, 2.49, 'https://www.bokksumarket.com/products/shin-ramyun-spicy-ramen', 'F78D3FD9-A6D5-46A5-9753-54B3D710952D');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Hikari White Miso with Bonito Dashi', 'This 
white miso features an irresistible bonito dashi (broth) flavor! Expect a delicious salty, crispy, umami flavor whenever you add it 
to recipes. It is made with kelp and bonito stock, which provides a bold taste. Use this versatile miso for soups, stews, sauces, 
marinades, salad dressings, and more. Bonus - This white miso is gluten-free! Common Allergens: Fish, Soy.)', 50, 9.99, 
'https://www.bokksumarket.com/products/hikari-white-miso-with-bonito-dashi', 'D9976A1D-AA89-4BFB-A3D3-A95B54A6BFFA');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Sangaria Ramune Soda: Lychee', 'Ramune is a 
favorite soda in Japan for a reason: the drink is bursting with fruity flavor, is made with real sugar, and the packaging cannot be 
beaten. To drink this marble soda, use the stopper to push down on the marble, which shoots into the neck of the bottle. It bounces 
up and down as you drink, making a fun tinkling sound. We hope the taste of lychee has you feeling tropical vibes!', 150, 3.99, 
'https://www.bokksumarket.com/products/sangaria-ramune-soda-lychee', 'C8477A1C-FF50-43C4-951E-E014CC6C5BF4');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Moshi Sparkling Juice Drink: Original Yuzu', 'Maker Moshi is here to bring the
taste of yuzu to you! Made with 100% yuzu juice sourced straight from Japan, each refreshing sip of this sparkling juice is bursting with the full fruity aroma and 
tartness of yuzu. There may be a “yuzu halo” or a ring of the all-natural yuzu essential oils at the top of the bottle, which are totally safe to 
consume - just give it a shake!', 150, 3.99, 
'https://www.bokksumarket.com/collections/juice/products/moshi-sparkling-drink-original-yuzu', '72A4EB16-6CFD-4165-B14F-DF220EFC1618');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Koda Mochiko Sweet Rice Flour', 'Introduced in the 1940s, Koda Farms’ 
mochiko (sweet rice flour) is the longest-selling brand of sweet rice flour in America. Known for its sticky chewy texture, mochiko is very popular for making 
homemade mochi, manju, chi-chi-dango, senbei, and other Japanese snacks: both savory and sweet! You can also use it to add a unique Japanese twist to your 
everyday waffles, cakes, and muffins. Whatever you’re in the mood for, mochiko will surely make your life a little sweeter!', 75, 4.99, 
'https://www.bokksumarket.com/collections/flour/products/mochiko-sweet-rice-flour', 'A0199090-0162-4CD7-B53C-22346FD98A9F');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('J-Basket Arare Rice Crackers: Norimaki Seaweed', 'This snack
features tasty arare (rice crackers) tightly wrapped in nori, or seaweed. The combination of the crispy and savory crackers and the crunchy, umami﻿-laden 
seaweed is out of this world!', 50, 6.49, 
'https://www.bokksumarket.com/collections/crackers/products/arare-rice-crackers-norimaki-seaweed', 'DAE6D21F-986E-494D-B996-40127E6C66E1');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Marufuji Makisu Sushi Bamboo Rolling Mat', 'Ready to make your 
own sushi? Then you’ll need the right equipment! In addition to being the material traditionally used for sushi rolling mats, bamboo is sustainable 
and elegant. Unlike wood, it absorbs less water and doesn’t scratch surfaces!', 100, 2.99, 
'https://www.bokksumarket.com/collections/all-household/products/makisu-sushi-bamboo-rolling-mat', '14D4CFB4-9EE9-4C06-BC1A-592073B744C8');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Shirakiku Char Siu BBQ Pork Buns (3 pieces)', 'These yummy char siu BBQ 
pork buns will transport you to a dim sum restaurant with one bite! They feature a delicious soft pillowy exterior and a flavorful BBQ pork filling. Each pack 
comes with three delicious pork buns to enjoy. Steam them for a more authentic meal. Bonus - You can heat them in a microwave for a quick meal or snack!', 125, 8.99, 
'https://www.bokksumarket.com/collections/microwave-ready/products/shirakiku-char-siu-bbq-pork-buns-3-pieces', 'D665ABDB-4899-4FCB-92AC-171D97964F68');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Kubota Daifuku Mochi: Peach', 'In Japan, nothing says summertime 
vibes like peaches! Enjoy the sweet flavor of juicy peaches in the creamy paste of this soft and chewy daifuku mochi. It can’t be summer all the time, but 
it can taste like it! This mochi will surely take a peach of your heart.', 50, 6.99, 
'https://www.bokksumarket.com/collections/mochi/products/daifuku-mochi-peach', '46B6CAC2-66E3-4E72-BCAF-99E64882F30F');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Fujii Niigata Iwafune Koshihikari Rice: 4.4 lb', 'Are you ready to 
expand your rice palate? This unique short-grain rice is harvested in Iwafune of Niigata prefecture. It’s known for having a more chewy texture and less 
stickiness than other short-grain rice. Easy to cook, easy to eat, and easy to love! But, don''t forget to wash it first!', 75, 29.99, 
'https://www.bokksumarket.com/collections/rice/products/fujii-niigata-iwafune-koshihikari-rice', '8325169C-28BB-4C5B-81CE-D13F1CFA6FA2');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Boiled Sushi Shrimp (30 pieces)', 'When making your own sushi, you 
deserve shrimply the best ingredients! This butterfly shrimp is pre-cooked, peeled, deveined, and comes to you frozen to preserve freshness. Plus, the 
tails are left on, so it will look aesthetically pleasing in addition to being totally delicious. You can even fry them to make shrimp tempura.', 125, 14.99, 
'https://www.bokksumarket.com/collections/frozen-seafood/products/boiled-sushi-shrimp-30-pieces', '35243BCF-CF45-49C4-8E6C-2F09A661CD72');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Ajishima Furikake Rice Seasoning: Seto Fumi', 'Is your bowl of rice a little...bland? 
Put down that soy sauce! Meet furikake: a sweet-salty-umami blend of crunchy seaweed bits, sesame seeds, bonito flakes, salt, and tasty spices. Fish and seaweed are bringing 
the umami to this party, so invite your seafood-loving friends! Sprinkle it for presentation or taste. Just make sure it’s after your rice is plated.', 50, 4.99, 
'https://www.bokksumarket.com/collections/spices-seasonings/products/furikake-rice-seasoning-seto-fumi', '433FE019-5EDC-4223-BEBC-32985DD5430B');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Yu Yee Bing Tang Yellow Rock Sugar', 'Since its origins in the 
Tang Dynasty of China in the seventh century A.D, rock sugar continues to play a big role in the irresistible taste and appearance of Chinese cuisine. 
In addition to the obvious sweet baked treats like Su-style moon cakes and Nian Gao, rock sugar shines in savory applications. It balances out the salty 
and sour flavors of savory cuisine to create a harmonious flavor in addition to a beautiful translucent shine. Try it in recipes for Shanghai pork belly, 
Hainanese chicken rice, oxtail, and braised roasted duck.', 75, 3.49, 
'https://www.bokksumarket.com/collections/sugar/products/yu-yee-bing-tang-yellow-rock-sugar', 'BB512F7E-90D7-43A5-9E01-CF60D7562F98');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Evergreen Bubble Milk Tea w/ Tapioca: Brown Sugar', 'Satisfy your boba cravings 
wherever and whenever with this canned bubble tea! Picture this: real tapioca boba (with the chewy texture you know and love) in a deliciously brewed brown sugar milk tea. 
Sweet, creamy, and always fresh - you’ll be saying sip-sip hooray!', 150, 3.49, 
'https://www.bokksumarket.com/collections/tea/products/evergreen-bubble-milk-tea-with-tapioca-black-sugar', '85E91B93-9D78-4752-ACEE-A491186C3305');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Wang Tinned Napa Cabbage Kimchi', 'Kimchi began as a way 
to preserve vegetables using a fermentation process. Now, it''s a Korean staple in many households! This crunchy napa cabbage is a delicious way 
to add heat and a tangy taste to any dish. The best part? You can add kimchi to anything! Try it on rice, savory pancakes, and congee.', 75, 3.49, 
'https://www.bokksumarket.com/collections/canned/products/wang-napa-cabbage-kimchi-in-can', '044D3D86-7048-40E4-AFCC-9A9BB332F05F');

insert into Item (Name, Description, Quantity, Price, ImageURL, SubCategoryID) values ('Ocean Bomb Sailor Moon Sparkling Water: Pomelo', 'Not quite soda, not quite plain 
sparkling water - this delicious pomelo-flavored drink is the perfect summer beverage. It has less sugar than regular soda pop but a sweeter taste than other sparkling water 
options. Join Sailor Moon on her adventures with each sip of this yummy drink! Each flavor from this collaboration features a different Sailor Moon character. Try to collect 
all the characters by trying different flavors!', 150, 3.99, 
'https://www.bokksumarket.com/collections/water/products/ocean-bomb-sailor-moon-sparkling-water-pomelo', 'C56A6FCE-0FCA-4E68-8FF1-350DAC6BDA00');

-- OrderItem table data (Must contain a valid ItemID and OrderID from the related tables)
insert into OrderItem (Price, Quantity, ItemID, OrderID) VALUES (3.99, 3, 
'1D69A750-74D9-48FF-A081-0CD558C1E786', 'FA9E6718-1000-452F-B755-092C2BCBF010');

-- ShoppingList table data
insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Weekend Shopping List', 'Y', GETDATE(), 'D48694D0-CE4A-4639-B709-1950843FCAB8');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Essentials', 'Y', GETDATE(), 'B5FBE202-FAA1-4C96-9E1A-9309E974BB04');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('My List', 'N', GETDATE(), 'B5FBE202-FAA1-4C96-9E1A-9309E974BB04');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Friday List', 'N', GETDATE(), 'B5FBE202-FAA1-4C96-9E1A-9309E974BB04');

insert into ShoppingList (Title, IsActive, DateCreated, UserID) values 
('Monday List', 'N', GETDATE(), 'B5FBE202-FAA1-4C96-9E1A-9309E974BB04');

-- ShoppingListItem table data (Must contain a valid ItemID from the related table)
insert into ShoppingListItem(IsCrossedOff, Quantity, Title, ItemID, ShoppingListID) values
('Y', 1, 'Friday List', '1D69A750-74D9-48FF-A081-0CD558C1E786', '8097F488-9579-4488-819F-071DA53304DD');

-- CartItem table data
insert into CartItem (Quantity, ItemID, UserID) 
values ( 1, '1D69A750-74D9-48FF-A081-0CD558C1E786', 'D48694D0-CE4A-4639-B709-1950843FCAB8');

insert into CartItem (Quantity, ItemID, UserID) 
values ( 2, '9DB36188-C7C6-446A-B258-11DF395DA99A', '3E693409-04AC-4C44-9D27-0B75507FCBD3');

insert into CartItem (Quantity, ItemID, UserID) 
values ( 6, '9DB36188-C7C6-446A-B258-11DF395DA99A', 'D48694D0-CE4A-4639-B709-1950843FCAB8');

insert into CartItem (Quantity, ItemID, UserID)
values ( 3, '5B86B824-2674-42BC-9270-13BB832598E4', 'D48694D0-CE4A-4639-B709-1950843FCAB8');

 insert into CartItem (Quantity, ItemID, UserID)
 values (10, '1D69A750-74D9-48FF-A081-0CD558C1E786', '7B21098D-1612-4258-B020-DD20C0A5E973');

 insert into CartItem (Quantity, ItemID, UserID)
 values (5, '9DB36188-C7C6-446A-B258-11DF395DA99A', '7B21098D-1612-4258-B020-DD20C0A5E973');
