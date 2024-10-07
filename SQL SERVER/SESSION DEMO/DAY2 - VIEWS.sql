SELECT * FROM production.products
SELECT * FROM production.brands

SELECT product_name, brand_name, list_price FROM production.products p
INNER JOIN production.brands b on b.brand_id = p.brand_id;

------------------ CREATING VIEWS ------------------

CREATE VIEW sales.productInfo
AS
	SELECT product_name, brand_name, list_price FROM production.products p INNER JOIN production.brands b on b.brand_id = p.brand_id;

SELECT * FROM sales.productInfo;

-- Complex select query in View

CREATE VIEW sales.dailySales
AS
	SELECT year(order_date) AS 'Year', month(order_date) AS 'Month', day(order_date) AS 'Day', p.product_id, product_name, quantity * i.list_price AS 'Sales' FROM sales.orders AS o
	INNER JOIN sales.order_items AS i ON o.order_id = i.order_id
	INNER JOIN production.products AS p ON p.product_id = i.product_id;

SELECT * FROM sales.dailySales;

------------------ CREATING INDEXES ------------------

------ CLUSTERED INDEX ------

CREATE TABLE production.part_prices(part_id INT NOT NULL, valid_from DATE, price DEC(18,4) NOT NULL)
ALTER TABLE production.part_prices ADD PRIMARY KEY(part_id)
ALTER TABLE production.part_prices DROP CONSTRAINT PK__part_pri__A0E3FAB8182E1B10;
-- OR
CREATE CLUSTERED INDEX ix_part_id ON production.part_prices(part_id)

------ NON-CLUSTERED INDEX ------
CREATE NONCLUSTERED INDEX ix_part_idn ON production.part_prices(part_id)

------ DISABLE INDEX ------
ALTER INDEX ix_part_id ON production.part_prices DISABLE
ALTER INDEX ALL ON production.part_prices DISABLE

------ ENABLE INDEX ------
ALTER INDEX ix_part_id ON production.part_prices REBUILD

------ DROP INDEX ------
DROP INDEX ix_part_id ON production.part_prices

------------------ CREATING TRIGGERS ------------------

SELECT * FROM production.products

-- INSERTED and DELETED - DML Triggers

CREATE TABLE production.product_audits(change_id INT IDENTITY PRIMARY KEY,
product_id INT NOT NULL, product_name VARCHAR(255) NOT NULL, brand_id INT NOT NULL,
category_id INT NOT NULL, model_year INT NOT NULL, list_price DEC(10,2) NOT NULL,
updated_at DATETIME NOT NULL, operation CHAR(3) NOT NULL CHECK(operation='INS' OR operation='DEL'));

CREATE TRIGGER production.trg_proaudit
ON production.products
AFTER INSERT,DELETE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO production.product_audits(product_id,product_name,brand_id,category_id,model_year,list_price,updated_at,operation)
	SELECT i.product_id, product_name, brand_id, category_id, model_year,i.list_price,GETDATE(),'INS' FROM inserted i
	UNION ALL
	SELECT d.product_id, product_name, brand_id, category_id, model_year,d.list_price,GETDATE(),'DEL' FROM deleted d
END;

INSERT INTO production.products(product_name, brand_id, category_id, model_year,d.list_price) VALUES
('Test Trigger',1,1,2018,599);

SELECT * FROM production.product_audits;

DELETE FROM production.products WHERE product_id = 322;

-- INSTEAD OF Trigger
-- (New Brand Name needs approval before inserting into table)
SELECT * FROM production.brands
CREATE TABLE production.brandapprovals(brand_id INT IDENTITY PRIMARY KEY, brand_name VARCHAR(255) NOT NULL)

CREATE TRIGGER production.trg_approvals
ON production.brands
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO production.brandapprovals(brand_name) SELECT i.brand_name FROM inserted i
	WHERE i.brand_name NOT IN (SELECT brand_name FROM production.brands); -- Checking for no duplicate insertion
END;

INSERT INTO production.brands(brand_name) VALUES('Test Brand')
SELECT * FROM production.brandapprovals;
