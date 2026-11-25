IF OBJECT_ID('dbo.Farmers') IS NULL
CREATE TABLE dbo.Farmers (FarmerID int IDENTITY PRIMARY KEY, Name nvarchar(100) NOT NULL, Email nvarchar(254) NOT NULL, Contact nvarchar(20) NOT NULL UNIQUE, Address nvarchar(500) NOT NULL, Password nvarchar(256) NOT NULL);
IF OBJECT_ID('dbo.Customers') IS NULL
CREATE TABLE dbo.Customers (CustomerID int IDENTITY PRIMARY KEY, Name nvarchar(100) NOT NULL, Email nvarchar(254) NOT NULL, Contact nvarchar(20) NOT NULL UNIQUE, Address nvarchar(500) NOT NULL, Password nvarchar(256) NOT NULL);
IF OBJECT_ID('dbo.Admin') IS NULL
CREATE TABLE dbo.Admin (AdminID int IDENTITY PRIMARY KEY, Name nvarchar(100) NOT NULL, Contact nvarchar(20) NOT NULL UNIQUE, Password nvarchar(256) NOT NULL);
IF OBJECT_ID('dbo.Cattle') IS NULL
CREATE TABLE dbo.Cattle (CattleID int IDENTITY PRIMARY KEY, Breed nvarchar(100) NOT NULL, Weight decimal(12,2) NOT NULL CHECK(Weight>0), Age decimal(8,2) NOT NULL CHECK(Age>0), Price decimal(14,2) NOT NULL CHECK(Price>0), TypeAnimal nvarchar(20) NOT NULL, ImageURL varbinary(max) NULL, FarmerContact nvarchar(20) NULL, IsSold bit NOT NULL DEFAULT 0);
IF COL_LENGTH('dbo.Cattle','FarmerContact') IS NULL ALTER TABLE dbo.Cattle ADD FarmerContact nvarchar(20) NULL;
IF COL_LENGTH('dbo.Cattle','IsSold') IS NULL ALTER TABLE dbo.Cattle ADD IsSold bit NOT NULL CONSTRAINT DF_Cattle_IsSold DEFAULT 0;
IF OBJECT_ID('dbo.Reports') IS NULL
CREATE TABLE dbo.Reports (ReportID int IDENTITY PRIMARY KEY, ReportText nvarchar(max) NOT NULL, CreatedAt datetime2 NOT NULL DEFAULT SYSUTCDATETIME());
IF OBJECT_ID('dbo.Orders') IS NULL
CREATE TABLE dbo.Orders (OrderID int IDENTITY PRIMARY KEY, CattleID int NOT NULL, CustomerContact nvarchar(20) NOT NULL, FarmerContact nvarchar(20) NULL, Breed nvarchar(100) NOT NULL, AnimalPrice decimal(14,2) NOT NULL, DeliveryCharge decimal(12,2) NOT NULL, PaymentMethod nvarchar(30) NOT NULL, Status nvarchar(30) NOT NULL, CreatedAt datetime2 NOT NULL DEFAULT SYSUTCDATETIME(), CONSTRAINT UQ_Order_Cattle UNIQUE(CattleID));
