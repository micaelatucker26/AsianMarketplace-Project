-- Create a function that returns the total amount that someone spent on a specific order and when
-- they placed the order, given a specific OrderID
CREATE FUNCTION dbo.CalculateOrderTotal( @orderID uniqueidentifier)
RETURNS TABLE
AS 
RETURN
(
	SELECT SUM(oi.Price * oi.Quantity) AS Total_Price, o.OrderDate
	FROM [Order] o
	JOIN OrderItem oi ON o.OrderID = oi.OrderID
	WHERE o.OrderID = @orderID
	GROUP BY o.OrderDate
);
GO
--Testing the CalculateOrderTotal function
SELECT * FROM dbo.CalculateOrderTotal ('186F66DC-0BCB-43D4-8CDD-EFB9D500A3ED');