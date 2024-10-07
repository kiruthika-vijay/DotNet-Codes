-------------- CREATING VIEWS --------------

------ EXERCISE 1 ------

CREATE VIEW production.vw_ProductDetails
AS
	SELECT product_name, brand_name, category_name, list_price FROM production.products AS p
	INNER JOIN production.brands AS b ON p.brand_id = b.brand_id
	INNER JOIN production.categories AS c ON p.category_id = c.category_id;

SELECT * FROM production.vw_ProductDetails;

------ EXERCISE 2 ------

CREATE VIEW sales.vw_CustomerOrders
AS
	SELECT o.order_id, order_date, CONCAT(first_name,' ',last_name) AS customer_name, store_name, SUM(quantity) AS total_quantity FROM sales.orders AS o
	INNER JOIN sales.customers AS c ON c.customer_id = o.customer_id
	INNER JOIN sales.stores AS s ON s.store_id = o.store_id
	INNER JOIN sales.order_items AS oi ON oi.order_id = o.order_id
	GROUP BY o.order_id, first_name, last_name, order_date, store_name;

SELECT * FROM sales.vw_CustomerOrders;

------ EXERCISE 3 ------

CREATE VIEW sales.vw_StoreStockLevels
AS
	SELECT store_name, product_name, quantity
	FROM production.stocks AS s
	INNER JOIN production.products AS p ON p.product_id = s.product_id
	INNER JOIN sales.stores AS st ON st.store_id = s.store_id;

SELECT * FROM sales.vw_StoreStockLevels;

------ EXERCISE 4 ------

CREATE VIEW sales.vw_TopSellingProducts
AS
	SELECT product_name, SUM(quantity) AS total_quantity_sold, SUM(quantity * oi.list_price) AS total_sales_amount
	FROM sales.order_items As oi
	INNER JOIN production.products AS p ON p.product_id = oi.product_id
	GROUP BY product_name;

SELECT * FROM sales.vw_TopSellingProducts;

------ EXERCISE 5 ------

CREATE VIEW sales.vw_OrdersSummary
AS
	SELECT order_date, COUNT(o.order_id) AS total_orders, SUM(quantity) AS total_quantity, SUM(quantity * list_price) AS total_sales_amount
	FROM sales.orders o
	INNER JOIN sales.order_items oi ON o.order_id = oi.order_id
	GROUP BY order_date;

SELECT * FROM sales.vw_OrdersSummary;

---------------- CLUSTERED INDEX ----------------
------ EXERCISE 1 ------

CREATE CLUSTERED INDEX idx_list_price ON production.products(list_price);

SELECT product_id, product_name, list_price FROM production.products
ORDER BY list_price;

------ EXERCISE 2 ------

CREATE CLUSTERED INDEX idx_order_date ON sales.orders(order_date)

SELECT * FROM sales.orders WHERE order_date BETWEEN '2016-01-01' AND '2016-02-05';

---------------- NON-CLUSTERED INDEX ----------------

------ EXERCISE 1 ------

CREATE NONCLUSTERED INDEX idx_product_id ON sales.order_items(product_id);

SELECT order_id, p.product_id, product_name, quantity, oi.list_price, discount FROM sales.order_items oi
INNER JOIN production.products p ON oi.product_id = p.product_id;

---------------- TRIGGERS ----------------

------ EXERCISE 1 ------

CREATE TABLE sales.order_log(log_id INT IDENTITY PRIMARY KEY, order_id INT NOT NULL, order_date DATE NOT NULL, customer_id INT NOT NULL, log_timestamp DATETIME2 DEFAULT GETDATE());

CREATE TRIGGER trg_LogNewUser
ON sales.orders
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO sales.order_log(order_id,order_date,customer_id)
	SELECT i.order_id,order_date,customer_id FROM inserted i
END;

SELECT * FROM sales.orders;
INSERT INTO sales.order_log(order_id,order_date,customer_id) VALUES(1,'2024-08-20',2);
SELECT * FROM sales.order_log;

------ EXERCISE 2 ------

CREATE TABLE production.price_change_log(log_id INT IDENTITY PRIMARY KEY, product_id INT NOT NULL, old_price DEC(18,2) NOT NULL,  new_price DEC(18,2) NOT NULL, change_date DATETIME DEFAULT GETDATE());

CREATE TRIGGER trg_TrackPriceChanges
ON production.products
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	IF UPDATE(list_price)
	BEGIN
		INSERT INTO production.price_change_log(product_id, old_price, new_price, change_date)
		SELECT i.product_id, d.list_price, i.list_price, GETDATE() FROM inserted i
		INNER JOIN deleted d ON i.product_id = d.product_id;
	END;
END;

UPDATE production.products 
SET list_price = 97.99
WHERE product_id = 2;

SELECT * FROM production.products;
SELECT * FROM production.price_change_log;

------ EXERCISE 3 ------

CREATE TRIGGER trg_PreventDeleteCustomersWithOrders
ON sales.customers
INSTEAD OF DELETE
AS
BEGIN
	IF EXISTS(
		SELECT 1 FROM sales.orders o
		WHERE o.customer_id IN (
			SELECT customer_id FROM deleted)
		)
		BEGIN
			RAISERROR('Cannot delete customer(s) with existing orders',16,1);
			ROLLBACK TRANSACTION;
		END
	ELSE
		BEGIN
			DELETE FROM sales.customers WHERE customer_id IN (
			SELECT customer_id FROM deleted);
		END
END;

DELETE FROM sales.customers WHERE customer_id = 1;

------ EXERCISE 4 ------

CREATE TRIGGER trg_HandleStockUpdates
ON production.stocks
AFTER UPDATE
AS
BEGIN
	DECLARE @newQuantity INT;
	SELECT @newQuantity = i.quantity FROM inserted i;
	IF @newQuantity < 0
	BEGIN
		RAISERROR('Cannot insert a negative index value for stock quantity. Enter a valid count for stock quantity.',16,1);
		ROLLBACK TRANSACTION;
	END
	ELSE
	BEGIN
		UPDATE s SET s.quantity = i.quantity
		FROM production.stocks s
		INNER JOIN inserted i ON i.product_id = s.product_id;
	END
END;

UPDATE production.stocks
SET quantity = 65
WHERE product_id = 3;

SELECT * FROM production.stocks;