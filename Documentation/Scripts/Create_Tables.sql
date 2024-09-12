--drop database AsianMarketplaceDb;
--create database AsianMarketplaceDb;
--use AsianMarketplaceDb;


-- Create a table named Category to hold the main category names for groups of various goods/products
-- in the marketplace. A category can have one or many subcategories.
create table Category (
	CategoryID uniqueidentifier primary key default newid(),
	Name varchar(25) not null
);


-- Create a table named SubCategory to hold the main subcategories under each category of goods/products
-- in the marketplace. A subcategory belongs to only one category.
create table SubCategory (
	SubCategoryID uniqueidentifier primary key default newid(),
	Name varchar(25) not null UNIQUE,
	CategoryID uniqueidentifier not null,
	constraint Category_FK foreign key (CategoryID) 
		references Category(CategoryID)
			on delete cascade
			on update cascade
);


-- Create a table named Shopper to hold the data stored for a user that logs into the online
-- marketplace. A shopper can create zero or more online orders and have zero or more shopping
-- lists. A shopper can also add zero or more items to their cart. Note: The password and salt 
-- are combined and that value is hashed and stored in the Password field.
create table Shopper (
	UserID uniqueidentifier primary key default newid(),
	Username varchar(25) not null UNIQUE,
	Password varchar(255) not null
);


-- Create a table named Order to hold the data for multiple online orders that have been placed
-- through the online marketplace. An order can have 1 or many items tied to the order that 
-- represent items purchased through the online marketplace. Each order is tied to a 
-- unique user.
create table [Order] (
	OrderID uniqueidentifier primary key default newid(),
	OrderDate datetime not null,
	UserID uniqueidentifier not null,
    constraint Shopper_FK foreign key (UserID) 
		references Shopper(UserID)
			on delete cascade
			on update cascade
);


-- Create a table named Item to hold the information on a product that is available for sale
-- in the online marketplace. An item exists under one subcategory and can exist in zero or more 
-- shopping lists. An item can exist in 1 or more online orders and can exist in one or many carts.
create table Item (
	ItemID uniqueidentifier primary key default newid(),
	Name varchar(50) not null,
	Description varchar(1000) not null,
	Quantity int not null default 0,
	Price money not null check (Price >= 0),
	ImageURL varchar(255) not null,
	SubCategoryID uniqueidentifier not null,
	constraint SubCategory_FK foreign key (SubCategoryID) 
		references SubCategory(SubCategoryID)
			on delete cascade
);


-- Create a table named OrderItem to hold the items added to an online order. This table can store 
-- the price of an item (or unit price) at the time a person placed their order 
-- (in this case, how much they spent on that item in the order). An order item is tied to a unique 
-- order and an order item references an ItemID in the Item table.
create table OrderItem (
	Price money not null CHECK (Price >= 0),
	Quantity int not null CHECK (Quantity >= 1),
	ItemID uniqueidentifier not null,
	OrderID uniqueidentifier not null,
	primary key (ItemID, OrderID),
	constraint OrderItem_ItemID_FK foreign key (ItemID) 
		references Item(ItemID)
			on delete cascade,
	constraint OrderItem_OrderID_FK foreign key (OrderID) 
		references [Order](OrderID)
			on delete cascade
);


-- Create a table named ShoppingList that stores information on a shopping list that has been created by
-- a shopper. A shopping list belongs to a unique shopper/user and a shopping list can contain zero
-- or more items in it. Note: The Title and UserID are unique to each shopper so they need to be a 
-- composite key. The IsActive field is used when a user has one or more shopping lists and the
-- list becomes active once items start to be added to the list and are viewed in the most
-- recent shopping list.
create table ShoppingList (
	 ShoppingListID uniqueidentifier primary key default newid(),
	 Title varchar(50) not null,
	 IsActive char(1) not null default 'N',
	 DateCreated datetime not null,
	 UserID uniqueidentifier not null,
	 constraint ShoppingList_UserID_FK foreign key (UserID) 
		references Shopper(UserID)
			on delete cascade
			on update cascade
);

-- Create a table named ShoppingListItem that holds the data on items that are in a unique shopping list.
-- A shopping list item belongs to a shopping list under a unique shopper/user and represents 
-- a specific item from the Item table. A shopping list item can use the IsCrossedOff field to cross off
-- an item from the shopping list; when the item is crossed off, it moves to another list of completed
-- items under the current shopping list.
create table ShoppingListItem (
	ShoppingListItemID uniqueidentifier primary key default newid(),
	IsCrossedOff char(1) default 'N',
	Quantity int not null check (Quantity >= 1),
	Title varchar(50) not null,
	ItemID uniqueidentifier not null,
    ShoppingListID uniqueidentifier not null,
	constraint ShoppingListItem_ShoppingList_FK foreign key (ShoppingListID)
        references ShoppingList (ShoppingListID)
			on delete cascade,
    constraint ShoppingListItem_Item_FK foreign key (ItemID)
        references Item (ItemID)
			on delete cascade
);

-- Create a table named CartItem that holds the data on items that are in a unique cart.
-- A cart item belongs to a cart that is tied to a unique shopper/user and represents 
-- a specific item from the Item table.
create table CartItem (
	CartItemID uniqueidentifier primary key default newid(),
	Quantity int not null CHECK (Quantity >= 1),
	ItemID uniqueidentifier not null,
	UserID uniqueidentifier not null,
	constraint CartItem_ItemID_FK foreign key (ItemID) 
		references Item(ItemID)
			on delete cascade,
	constraint CartItem_UserID_FK foreign key (UserID) 
		references Shopper(UserID)
			on delete cascade
			on update cascade,
	constraint UQ_CartItem_ItemID_UserID unique (ItemID, UserID)
);