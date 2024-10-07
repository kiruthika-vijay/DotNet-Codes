------------------ ERROR HANDLING ------------------

------ EXERCISE 1 ------

CREATE PROCEDURE usp_InsertCustomerWithExceptionHandling(@custID INT, @custName VARCHAR(max), @phone VARCHAR(25))
AS
BEGIN
	BEGIN TRY
		INSERT INTO sales.customers(customer_id, first_name,phone)
		VALUES(@custID, @custName, @phone);
		PRINT 'CUSTOMER INSERTED';
	END TRY
	BEGIN CATCH
		PRINT 'ERROR WHILE INSERTION';
		SELECT ERROR_MESSAGE() AS 'Runtime Error Message';
	END CATCH
END;

EXEC usp_InsertCustomerWithExceptionHandling 100000, 'Paul', '9029083939';

------ EXERCISE 2 ------

CREATE PROCEDURE usp_updateProductsWithExceptionHandling(@productID INT, @product_name VARCHAR(max), @brandID INT, @catID INT)
AS
BEGIN
	BEGIN TRY
		UPDATE production.products
		SET product_name = @product_name, brand_id = @brandID, category_id = @catID
		WHERE product_id = @productID;
		IF @@ROWCOUNT = 0
		BEGIN
			RAISERROR('No rows updated. Product ID might be invalid.',16,1);
		END
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS 'Runtime Error Message';
	END CATCH
END;

EXEC usp_updateProductsWithExceptionHandling @productID=1,@product_name='Nike Shoes', @brandID=29099, @catID=3;

------------------ TRANSACTIONS ------------------

CREATE TYPE dbo.OrderItemType AS TABLE (product_id INT, item_id INT, quantity INT, price DEC(10,2));

CREATE PROCEDURE usp_PlaceOrderWithTransaction(@custID INT, @orderStatus TINYINT, @orderDate DATE, @reqDate DATE, @shipDate DATE, @storeID INT, @staffID int, @OrderItems dbo.OrderItemType READONLY)
AS
BEGIN
	DECLARE @orderID INT;
	BEGIN TRANSACTION;
		BEGIN TRY
			INSERT INTO sales.orders(customer_id,order_status,order_date,required_date,shipped_date,store_id,staff_id)
			VALUES(@custID,@orderStatus,@orderDate,@reqDate,@shipDate,@storeID,@staffID);
		
			SET @orderID = SCOPE_IDENTITY();

			INSERT INTO sales.order_items(order_id,item_id,product_id,quantity,list_price)
			SELECT @orderID, product_id, item_id, quantity, price FROM @OrderItems;
			
			COMMIT TRANSACTION;
			PRINT 'TRANSACTION SUCCEEDED'
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;
			PRINT 'TRANSACTION FAILED'
			SELECT ERROR_MESSAGE() AS 'Runtime Error Message';
		END CATCH
END

DECLARE @OrderItems dbo.OrderItemType;
INSERT INTO @OrderItems(product_id, item_id, quantity, price)
VALUES(1,3,2,100.00),(2,5,1,50.00);

EXEC usp_PlaceOrderWithTransaction @custID=1, @orderStatus= 4, @orderDate='2024-08-21', @reqDate='2024-08-25', @shipDate='2024-08-22', @storeID=3, @staffID=4, @OrderItems = @OrderItems;