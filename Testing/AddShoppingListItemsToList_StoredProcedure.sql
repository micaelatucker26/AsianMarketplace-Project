USE [AsianMarketplaceDb]

/****** Object:  StoredProcedure [dbo].[AddShoppingListItemsToList] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[AddShoppingListItemsToList]
	@UserID varchar(25)
AS
BEGIN
	SET NOCOUNT ON;

	-- Add shopping list items to list under certain criteria below
	INSERT INTO ShoppingListItem (IsCrossedOff, Quantity, Title, UserID, ItemID)
	SELECT
		-- The boolean value for 'IsCrossedOff' will be random, based on either a value
		-- of zero or one checked under two conditions (if greater than zero, 'Y' is 
		-- the value for 'IsCrossedOff', otherwise the value will be 'N')
		CASE	
			WHEN (ABS(CHECKSUM(NewId())) % 2) > 0 THEN 'Y'
			ELSE 'N'
		END as 'IsCrossedOff',

		-- A random number generated between 1 and 15 will be set as the value
		-- for 'Quantity'
		(ABS(CHECKSUM(NewId())) % 14) + 1 as 'Quantity',
		
		List.Title,
		UserID,
		ItemID
	FROM
		Shopper
	JOIN
		ShoppingList List
	ON
		Shopper.Username = List.UserID
		-- The Shopping List title will be the first shopping list a shopper has
		-- that is also currently active
		AND List.Title = 
		(
			SELECT TOP 1
				SL.Title
			FROM
				ShoppingList SL
			WHERE
				SL.IsActive = 'Y'
				AND List.UserID = SL.UserID		
		)
	JOIN
		Item
	ON
	-- The top 10 items will be added and will not contain items from the current
	-- shopper's shopping list, so new items can be added to a unique 
	-- shopper's shopping list
		Item.ItemID IN 
		(
			SELECT TOP 10 
				ItemID 
			FROM 
				Item
			WHERE 
				Item.ItemID NOT IN 
				(
					SELECT 
						ItemID
					FROM 
						ShoppingListItem
					WHERE 
						UserID = @UserID
				)
		)
	WHERE
		Shopper.Username = @UserID
END;
GO

