--Creates a CartView with the following criteria:

	-- Finds all current cart items.

	-- Shows the item name, item price, cart item quantity, item imageURL, username 
	-- associated with the cart and subcategory for that item.

create or alter view CartView as
	select i.name as ItemName, i.Price, ci.Quantity, i.ImageURL, s.Username, sc.Name as SubCategory
	from Item i
	join CartItem ci on i.ItemID = ci.ItemID
	join Shopper s on ci.UserID = s.Username
	join SubCategory sc on i.SubCategoryName = sc.Name;

--Testing CartView (given a username)
select * from CartView
where Username = 'ALord35';