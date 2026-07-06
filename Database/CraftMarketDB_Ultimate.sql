/*
===========================================================
 Local Artisan & Craft Market Platform
 Ultimate SQL Server Setup Script
 Safe to run multiple times
===========================================================
*/

USE master;
GO

IF DB_ID('CraftMarketDB') IS NOT NULL
BEGIN
    ALTER DATABASE CraftMarketDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CraftMarketDB;
END
GO

CREATE DATABASE CraftMarketDB;
GO

USE CraftMarketDB;
GO

CREATE TABLE Admins(
 AdminID INT IDENTITY PRIMARY KEY,
 FullName NVARCHAR(100) NOT NULL,
 Username NVARCHAR(50) NOT NULL UNIQUE,
 PasswordHash NVARCHAR(255) NOT NULL,
 Email NVARCHAR(100),
 Phone NVARCHAR(20),
 CreatedAt DATETIME2 DEFAULT SYSDATETIME()
);
GO

CREATE TABLE Artisans(
 ArtisanID INT IDENTITY PRIMARY KEY,
 FullName NVARCHAR(100) NOT NULL,
 Email NVARCHAR(100),
 Phone NVARCHAR(20),
 Address NVARCHAR(200),
 Status NVARCHAR(20) NOT NULL DEFAULT 'Pending'
 CHECK (Status IN ('Pending','Approved','Rejected')),
 CreatedAt DATETIME2 DEFAULT SYSDATETIME()
);
GO

CREATE TABLE Categories(
 CategoryID INT IDENTITY PRIMARY KEY,
 CategoryName NVARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE Products(
 ProductID INT IDENTITY PRIMARY KEY,
 ArtisanID INT NOT NULL,
 CategoryID INT NOT NULL,
 ProductName NVARCHAR(150) NOT NULL,
 Description NVARCHAR(500),
 Price DECIMAL(10,2) NOT NULL CHECK(Price>=0),
 Stock INT NOT NULL DEFAULT 0 CHECK(Stock>=0),
 CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
 CONSTRAINT FK_Product_Artisan FOREIGN KEY(ArtisanID) REFERENCES Artisans(ArtisanID),
 CONSTRAINT FK_Product_Category FOREIGN KEY(CategoryID) REFERENCES Categories(CategoryID)
);
GO

CREATE TABLE Orders(
 OrderID INT IDENTITY PRIMARY KEY,
 CustomerName NVARCHAR(100) NOT NULL,
 CustomerPhone NVARCHAR(20),
 OrderDate DATETIME2 DEFAULT SYSDATETIME(),
 TotalAmount DECIMAL(10,2) DEFAULT 0 CHECK(TotalAmount>=0),
 Status NVARCHAR(20) DEFAULT 'Pending'
);
GO

CREATE TABLE OrderItems(
 OrderItemID INT IDENTITY PRIMARY KEY,
 OrderID INT NOT NULL,
 ProductID INT NOT NULL,
 Quantity INT NOT NULL CHECK(Quantity>0),
 UnitPrice DECIMAL(10,2) NOT NULL CHECK(UnitPrice>=0),
 CONSTRAINT FK_OrderItems_Order FOREIGN KEY(OrderID)
   REFERENCES Orders(OrderID) ON DELETE CASCADE,
 CONSTRAINT FK_OrderItems_Product FOREIGN KEY(ProductID)
   REFERENCES Products(ProductID)
);
GO

CREATE INDEX IX_Product_Name ON Products(ProductName);
CREATE INDEX IX_Product_Category ON Products(CategoryID);
CREATE INDEX IX_Artisan_Status ON Artisans(Status);
GO

INSERT INTO Admins(FullName,Username,PasswordHash,Email,Phone)
VALUES('System Administrator','admin','admin123','admin@craft.local','0771234567');

INSERT INTO Categories(CategoryName)
VALUES('Wood'),('Clay'),('Jewellery'),('Painting'),('Textile');

INSERT INTO Artisans(FullName,Email,Phone,Address,Status)
VALUES
('Nimal Perera','nimal@gmail.com','0711111111','Colombo','Approved'),
('Kamal Silva','kamal@gmail.com','0722222222','Kandy','Pending'),
('Saman Kumara','saman@gmail.com','0733333333','Galle','Approved');

INSERT INTO Products(ArtisanID,CategoryID,ProductName,Description,Price,Stock)
VALUES
(1,1,'Wooden Elephant','Hand carved elephant',2500,10),
(2,2,'Clay Pot','Traditional clay pot',1200,25),
(3,4,'Village Painting','Landscape painting',5000,5);

INSERT INTO Orders(CustomerName,CustomerPhone,TotalAmount,Status)
VALUES
('John','0700000001',2500,'Completed'),
('Mary','0700000002',6200,'Pending');

INSERT INTO OrderItems(OrderID,ProductID,Quantity,UnitPrice)
VALUES
(1,1,1,2500),
(2,2,1,1200),
(2,3,1,5000);
GO

CREATE VIEW DashboardView
AS
SELECT
 (SELECT COUNT(*) FROM Products) AS TotalProducts,
 (SELECT COUNT(*) FROM Artisans) AS TotalArtisans,
 (SELECT COUNT(*) FROM Orders) AS TotalOrders;
GO

CREATE VIEW SalesReportView
AS
SELECT
 O.OrderID,
 O.CustomerName,
 O.OrderDate,
 P.ProductName,
 OI.Quantity,
 OI.UnitPrice,
 OI.Quantity*OI.UnitPrice AS LineTotal
FROM OrderItems OI
JOIN Orders O ON O.OrderID=OI.OrderID
JOIN Products P ON P.ProductID=OI.ProductID;
GO

CREATE OR ALTER PROCEDURE sp_AdminLogin
 @Username NVARCHAR(50),
 @Password NVARCHAR(255)
AS
BEGIN
 SET NOCOUNT ON;
 SELECT * FROM Admins
 WHERE Username=@Username
   AND PasswordHash=@Password;
END;
GO

CREATE OR ALTER PROCEDURE sp_SearchProducts
 @Name NVARCHAR(100)
AS
BEGIN
 SET NOCOUNT ON;
 SELECT P.ProductID,P.ProductName,C.CategoryName,P.Price,P.Stock
 FROM Products P
 JOIN Categories C ON C.CategoryID=P.CategoryID
 WHERE P.ProductName LIKE '%'+@Name+'%';
END;
GO

SELECT * FROM DashboardView;
SELECT * FROM SalesReportView;
SELECT * FROM Admins;
SELECT * FROM Artisans;
SELECT * FROM Products;

PRINT '====================================';
PRINT 'CraftMarketDB Created Successfully';
PRINT 'Database Ready For C# WinForms';
PRINT '====================================';
GO
