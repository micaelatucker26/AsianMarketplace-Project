--Creates a ShoppingListView with the following criteria:

	-- Finds all current shopping list items.

	-- Shows the item name, item price, item description, item imageURL, shopping list quantity, 
	-- IsCrossedOff, subcategory for that item, username associated with the list and shopping 
	-- list title.

create or alter view ShoppingListView as
	select	
		i.Name as ItemName, i.Price, i.Description, i.ImageURL, 
		sli.Quantity, sli.IsCrossedOff, sc.Name as SubCategoryName, s.Username, sli.Title
	from Item i
	join ShoppingListItem sli on i.ItemID = sli.ItemID
	join Shopper s on sli.UserID = s.Username
	join SubCategory sc on i.SubCategoryName = sc.Name;


--Testing ShoppingListView (given a username and their shopping list name)
select * from ShoppingListView
where Username = 'MTucker27' and Title = 'Essentials';