-- Create a function that returns the totals for each order that someone has placed and when
-- they placed each order, given a shopper's username
CREATE FUNCTION dbo.ShowUserOrders (@username varchar(25))
RETURNS TABLE
AS
RETURN
(
	SELECT SUM(oi.Price * oi.Quantity) AS Total_Price, 
			o.OrderDate, o.OrderID
	FROM [Order] o
	JOIN OrderItem oi ON o.OrderID = oi.OrderID
	WHERE o.Username = @username
	GROUP BY o.OrderDate, o.OrderID
);
GO
--Testing the ShowUserOrders function
SELECT * FROM dbo.ShowUserOrders('DBishop123');