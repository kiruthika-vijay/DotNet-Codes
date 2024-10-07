-------------- STORED PROCEDURES --------------

------ EXERCISE 1 ------

CREATE PROCEDURE usp_GetProductsByCategory(@categoryID AS int)
AS
BEGIN
	SELECT product_name, brand_name, list_price FROM production.products
	JOIN production.brands ON production.products.brand_id = production.brands.brand_id
	WHERE category_id = @categoryID;
END;

EXEC usp_GetProductsByCategory 1;
EXEC usp_GetProductsByCategory 6;

------ EXERCISE 2 ------

CREATE PROCEDURE usp_AddCustomer(@first_name AS VARCHAR(max), @last_name AS VARCHAR(max), @phone AS VARCHAR(15), @email AS VARCHAR(max), @street AS VARCHAR(max), @city AS VARCHAR(max), @state AS VARCHAR(max), @zipcode AS VARCHAR(max), @storeID AS INT)
AS
BEGIN
	INSERT INTO sales.customers VALUES(@first_name, @last_name, @phone, @email, @street, @city, @state, @zipcode);
END;

EXEC usp_AddCustomer @first_name='Deepika',@last_name='Roy',@Phone='9993337772',@email='deeproy@gmail.com',@street='XY Street',@city='Chennai',@state='TamilNadu',@zipcode='60001',@storeId=6;

------ EXERCISE 3 ------ 

CREATE PROCEDURE usp_UpdateProductStock(@storeID AS INT, @productID AS INT, @stockQuantity AS INT)
AS
BEGIN
	UPDATE production.stocks SET production.stocks.quantity = @stockQuantity
	WHERE production.stocks.store_id = @storeID;
END;

EXEC usp_UpdateProductStock @storeID = 1, @productID = 8, @stockQuantity = 15;

SELECT * FROM production.stocks WHERE product_id = 8;

------ EXERCISE 4 ------ 

CREATE PROCEDURE usp_GetOrderDetails(@orderID AS INT)
AS
BEGIN
	SELECT order_date, CONCAT(first_name,' ',last_name) AS customer_name, store_name, product_name, quantity, sales.order_items.list_price 
	FROM sales.orders JOIN sales.customers ON sales.customers.customer_id = sales.orders.customer_id
	JOIN sales.stores ON sales.stores.store_id = sales.orders.store_id
	JOIN sales.order_items ON sales.order_items.order_id = sales.orders.order_id
	JOIN production.products ON production.products.product_id = sales.order_items.product_id
	WHERE sales.orders.order_id = @orderID;
END;

EXEC usp_GetOrderDetails 3;

------ EXERCISE 5 ------

CREATE PROCEDURE usp_GetTotalSalesByStore(@start_date AS DATE, @end_date AS DATE)
AS
BEGIN
	SELECT store_name, SUM(list_price) AS total_sales FROM sales.orders
	JOIN sales.stores ON sales.orders.store_id = sales.stores.store_id
	JOIN sales.order_items ON sales.order_items.order_id = sales.orders.order_id
	WHERE order_date BETWEEN @start_date AND @end_date
	GROUP BY store_name ORDER BY total_sales DESC;
END;

EXEC usp_GetTotalSalesByStore @start_date = '2016-01-04', @end_date = '2016-01-06';

-------------- SCALAR FUNCTIONS --------------

------ EXERCISE 1 ------

CREATE FUNCTION sales.fn_CalculateDiscountedPrice(@list_price INT, @disPer INT)
RETURNS DEC(10,2)
AS
BEGIN
	RETURN @list_price * @disPer / 100
END;

SELECT sales.fn_CalculateDiscountedPrice(9500,25) AS DiscountedPrice;

------ EXERCISE 2 ------

CREATE FUNCTION production.fn_GetFullCustomerName(@first_name VARCHAR(max), @last_name VARCHAR(max))
RETURNS VARCHAR(max)
AS
BEGIN
	DECLARE @fullName VARCHAR(max);
	SET @fullName = @first_name + ' ' + @last_name;
	RETURN @fullName;
END;

SELECT production.fn_GetFullCustomerName('Lisha', 'John') AS FullCustomerName;

------ EXERCISE 3 ------

CREATE FUNCTION sales.fn_CalculateTotalOrderAmount(@orderID INT)
RETURNS DEC(10,2)
AS
BEGIN
	DECLARE @TotalAmount DEC(10,2);
	SELECT @TotalAmount = SUM(quantity * list_price) FROM sales.order_items
	WHERE order_id = @orderID
	RETURN @TotalAmount;
END;

SELECT sales.fn_CalculateTotalOrderAmount(1) AS TotalOrderAmount;

-------------- TABLE VALUED FUNCTIONS --------------

------ EXERCISE 4 ------

CREATE FUNCTION fn_GetProductsByBrand(@brandID INT)
RETURNS TABLE
AS
RETURN 
	SELECT product_id, product_name, category_id, list_price FROM production.products
	WHERE brand_id = @brandID;

SELECT * FROM fn_GetProductsByBrand(3);
SELECT * FROM fn_GetProductsByBrand(3) WHERE list_price < 2000;

------ EXERCISE 5 ------

CREATE FUNCTION fn_GetOrdersByCustomer(@custID INT)
RETURNS TABLE
AS
RETURN
	SELECT order_id, order_date, store_id, staff_id FROM sales.orders
	WHERE customer_id = @custID;

SELECT * FROM fn_GetOrdersByCustomer(5);
SELECT * FROM fn_GetOrdersByCustomer(5) WHERE order_date = '2016-06-10';

------ EXERCISE 6 ------

CREATE FUNCTION fn_GetStockByStore(@storeID INT)
RETURNS TABLE
AS
RETURN
	SELECT production.products.product_id, product_name, quantity FROM sales.stores
	JOIN sales.orders ON sales.orders.store_id =  sales.stores.store_id
	JOIN sales.order_items ON sales.order_items.order_id = sales.orders.order_id
	JOIN production.products ON sales.order_items.product_id = production.products.product_id
	WHERE sales.stores.store_id = @storeID;

SELECT * FROM fn_GetStockByStore(2);
SELECT * FROM fn_GetStockByStore(2) WHERE quantity = '1';