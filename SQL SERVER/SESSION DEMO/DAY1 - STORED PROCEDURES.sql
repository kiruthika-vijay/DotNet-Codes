SELECT * FROM production.products
GO
------ CREATING STORED PROCEDURES ------

-- Create Stored Procedures
CREATE PROCEDURE getProductList
AS
BEGIN
	SELECT product_name, list_price FROM production.products ORDER BY product_name;
END;

-- Alter Stored Procedures
ALTER PROCEDURE getProductList
AS
BEGIN
	SELECT product_name, list_price FROM production.products ORDER BY list_price;
END;

-- Drop/Delete Stored Procedures
DROP PROCEDURE getProductList

-- Execute Stored Procedures
EXEC getProductList;

------ CREATING PARAMETERIZED STORED PROCEDURES ------

-- Create Procedures with Parameters and Conditions
CREATE PROCEDURE ListProducts(@set_price AS DECIMAL)
AS
BEGIN
	SELECT product_name, list_price FROM production.products WHERE list_price > @set_price ORDER BY list_price
END;

EXEC ListProducts 300;
EXEC ListProducts 500;
EXEC ListProducts 1000;

-- Alter Procedures with Parameters and Conditions
ALTER PROCEDURE ListProducts(@min_price AS DECIMAL, @max_price AS DECIMAL)
AS
BEGIN
	SELECT product_name, list_price FROM production.products 
	WHERE list_price BETWEEN @min_price AND @max_price 
	ORDER BY list_price
END;

EXEC ListProducts 300,500;
EXEC ListProducts 500,1000;
EXEC ListProducts @min_price = 1000; -- Try to set optional values to solve error

-- Alter Procedures with Parameters and Conditions
ALTER PROCEDURE ListProducts(@min_price AS DECIMAL, @max_price AS DECIMAL, @name AS VARCHAR(max))
AS
BEGIN
	SELECT product_name, list_price FROM production.products 
	WHERE product_name LIKE '%' + @name + '%' AND
	list_price BETWEEN @min_price AND @max_price 
	ORDER BY list_price
END;

EXEC ListProducts 300,500,'Trek';
EXEC ListProducts @min_price=500, @name='Trek', @max_price=1000;

-- Alter Procedures with setting optional values for Parameters
ALTER PROCEDURE ListProducts(@min_price AS DECIMAL = 0, @max_price AS DECIMAL = 1000, @name AS VARCHAR(max))
AS
BEGIN
	SELECT product_name, list_price FROM production.products 
	WHERE product_name LIKE '%' + @name + '%' AND
	list_price BETWEEN @min_price AND @max_price 
	ORDER BY list_price
END;

EXEC ListProducts @name='Trek';
EXEC ListProducts @min_price=500, @name='Trek';

------ RETURNING VALUES FROM STORED PROCEDURES ------

-- Using OUTPUT 
CREATE PROCEDURE ProductModel(@modelyear smallint, @count int OUTPUT)
AS
BEGIN
	SELECT product_name, list_price FROM production.products WHERE model_year = @modelyear;
	SELECT @count = @@ROWCOUNT;
END;
DECLARE @cnt int;
EXEC ProductModel @modelyear=2018, @count=@cnt OUTPUT;
SELECT @cnt AS 'No Of Products';
-- OR
DECLARE @cnt1 int;
EXEC ProductModel 2018, @cnt1 OUTPUT;
SELECT @cnt1 AS 'No Of Products'

------ USER DEFINED FUNCTIONS ------

-- Scalar Function
CREATE FUNCTION sales.netSales(@qty INT, @list_price DEC(10,2), @dis DEC(4,2))
RETURNS DEC(10,2)
AS
BEGIN
	RETURN @qty * @list_price * (1 - @dis);
END;

SELECT sales.netSales(10,100,0.1);
SELECT order_id, sales.netSales(quantity,list_price,discount) FROM sales.order_items;
SELECT order_id, SUM(sales.netSales(quantity,list_price,discount)) AS 'Net Amount for Order' 
FROM sales.order_items
GROUP BY order_id;

-- Table Valued Function
CREATE FUNCTION ProductYear(@year INT)
RETURNS TABLE
AS
RETURN 
SELECT product_name, model_year, list_price FROM production.products WHERE model_year = @year

SELECT * FROM ProductYear(2018);
SELECT product_name, list_price FROM ProductYear(2018);
SELECT product_name, list_price FROM ProductYear(2018) WHERE list_price < 500;