use AsianMarketplaceDb;
-- Store an order with an order item associated with that order
BEGIN TRANSACTION;

BEGIN TRY
	-- Declare a table variable to store the output
	DECLARE @OutputTable TABLE (OrderID UNIQUEIDENTIFIER);

	-- Create a new Order, storing the inserted OrderID into a temporary
	-- table
	insert into [Order] (OrderDate, Username)
	OUTPUT inserted.OrderID INTO @OutputTable
	values (GETDATE(), 'MTucker27');

	-- Retrieve the generated OrderID from the table variable
	DECLARE @NewOrderID UNIQUEIDENTIFIER;
	SELECT @NewOrderID = OrderID FROM @OutputTable;

	-- Create a new Order Item with the generated OrderID
	insert into OrderItem (Price, Quantity, ItemID, OrderID)
	values (4.99, 3, '4948CD46-2E19-49BF-AA2C-021D427E1377', @NewOrderID);

	-- Commit the transaction if all statements succeed
	COMMIT;
END TRY

BEGIN CATCH
	-- Rollback the transaction if any statement fails
    ROLLBACK;
    PRINT 'Transaction rolled back due to an error.';
END CATCH;