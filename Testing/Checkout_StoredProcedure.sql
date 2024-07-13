USE [AsianMarketplaceDb]
GO

/****** Object:  StoredProcedure [dbo].[Checkout]    Script Date: 7/12/2024 3:31:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Creates a stored procedure that takes items from a unique shopper's cart
-- and creates an order from their cart items, adding those items to the OrderItem table
CREATE PROCEDURE [dbo].[Checkout]
	@UserID varchar(25)
AS
BEGIN
	SET NOCOUNT ON;

	-- Variable to hold the new OrderID
	DECLARE @NewOrderID uniqueidentifier;
	
	--Table variable to hold the output OrderID
	DECLARE @OutputTable TABLE (OrderID uniqueidentifier);

	-- Create an Order
	INSERT INTO [Order] (OrderID, OrderDate, Username) 
	OUTPUT inserted.OrderID INTO @OutputTable
	VALUES (NEWID(), GETDATE(), @UserID); 

	 -- Set the variable from the table variable
    SELECT @NewOrderID = OrderID FROM @OutputTable;

	-- Add cart items to the OrderItem table, given the Item information and
	-- the Order information
	INSERT INTO OrderItem (Price, Quantity, ItemID, OrderID) 
			SELECT i.Price, ci.Quantity, ci.ItemID, @NewOrderID
			FROM Item i
			JOIN CartItem ci on ci.ItemID = i.ItemID
			WHERE ci.UserID = @UserID;
END;
GO