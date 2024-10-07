SELECT * FROM production.products
SELECT * FROM sales.order_items

---------------- ERROR HANDLING ----------------

-- WITH TRY....CATCH

BEGIN TRY
	SELECT 12/0 AS 'Data';
END TRY
BEGIN CATCH
	PRINT 'Runtime Error';
	SELECT ERROR_MESSAGE() AS 'Message from Runtime Error';
END CATCH

---------------- TRANSACTIONS ----------------
CREATE TABLE Person (
    PersonID int PRIMARY KEY IDENTITY(1,1),
    LastName varchar(255),
    FirstName varchar(255),
    Address varchar(255),
    City varchar(255),
	Age INT
) 
GO 
INSERT INTO Person VALUES('Hayes', 'Corey','123  Wern Ddu Lane','LUSTLEIGH',23)
INSERT INTO Person VALUES('Macdonald','Charlie','23  Peachfield Road','CEFN EINION',45)
INSERT INTO Person VALUES('Frost','Emma','85  Kingsway North','HOLTON',26)
INSERT INTO Person VALUES('Thomas', 'Tom','59  Dover Road', 'WESTER GRUINARDS',51)
INSERT INTO Person VALUES('Baxter','Cameron','106  Newmarket Road','HAWTHORPE',46)
INSERT INTO Person VALUES('Townsend','Imogen ','100  Shannon Way','CHIPPENHAM',20)
INSERT INTO Person VALUES('Preston','Taylor','14  Pendwyallt Road','BURTON',19)
INSERT INTO Person VALUES('Townsend','Imogen ','100  Shannon Way','CHIPPENHAM',18)
INSERT INTO Person VALUES('Khan','Jacob','72  Ballifeary Road','BANCFFOSFELEN',11)

SELECT * FROM Person
BEGIN TRAN
	UPDATE Person SET LastName = 'Devi', FirstName = 'Kamtachi' WHERE PersonID = 9
	SELECT @@TRANCOUNT AS 'OpenOperation'
COMMIT TRAN
SELECT @@TRANCOUNT AS 'OpenOperation'

BEGIN TRAN
	UPDATE Person SET LastName = 'Smith', FirstName = 'Will' WHERE PersonID = 7

SELECT * FROM Person WHERE PersonID = 7
ROLLBACK TRAN
SELECT * FROM Person WHERE PersonID = 7

