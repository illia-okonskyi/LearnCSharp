-- Db structure:
-- - Shoes - This table will be the centerpiece of the database and will contain details of the
--           products produced by the company. This table has relationships with all of the other
--           tables.
-- - Categories - This table contains the set of categories used to describe the company’s running
--                shoes. It has a many-to-many relationship with the Shoes table through the
--                ShoeCategoryJunction junction table.
-- - ShoeCategoryJunction - This is the junction table for the many-to-many relationship between the
--                          Shoes and Categories tables.
-- - Colors - This table contains the set of color combinations in which shoes are available and has
--            a one-to-many relationship with the Shoes table.
-- - SalesCampaigns - This table contains details of the sales campaign for each shoe and has a
--                    one-to-one relationship with the Shoes table.

USE master

-- 1) Recreate database
DROP DATABASE IF EXISTS ModelFirstDb
GO
CREATE DATABASE ModelFirstDb
GO
USE ModelFirstDb
GO

-- 2) Colors table
CREATE TABLE Colors (
	Id bigint IDENTITY(1,1) NOT NULL,
	Name nvarchar(max) NOT NULL,
	MainColor nvarchar(max) NOT NULL,
	HighlightColor nvarchar(max) NOT NULL,
	CONSTRAINT PK_Colors PRIMARY KEY (Id));

SET IDENTITY_INSERT dbo.Colors ON
INSERT dbo.Colors (Id, Name, MainColor, HighlightColor) VALUES
	(1, N'Red Flash', N'Red', N'Yellow'),
	(2, N'Cool Blue', N'Dark Blue', N'Light Blue'),
	(3, N'Midnight', N'Black', N'Black'),
	(4, N'Beacon', N'Yellow', N'Green')
SET IDENTITY_INSERT dbo.Colors OFF
GO

-- 3) Shoes table
CREATE TABLE Shoes (
	Id bigint IDENTITY(1,1) NOT NULL,
	Name nvarchar(max) NOT NULL,
	ColorId bigint NOT NULL,
	Price decimal(18, 2) NOT NULL,
	CONSTRAINT PK_Shoes PRIMARY KEY (Id ),
	CONSTRAINT FK_Shoes_Colors FOREIGN KEY(ColorId) REFERENCES dbo.Colors (Id))

SET IDENTITY_INSERT dbo.Shoes ON
INSERT dbo.Shoes (Id, Name, ColorId, Price) VALUES
	(1, N'Road Rocket', 2, 145.0000),
	(2, N'Trail Blazer', 4, 150.0000),
	(3, N'All Terrain Monster', 3, 250.0000),
	(4, N'Track Star', 1, 120.0000)
SET IDENTITY_INSERT dbo.Shoes OFF
GO

-- 4) SalesCampaigns table
CREATE TABLE SalesCampaigns(
	Id bigint IDENTITY(1,1) NOT NULL,
	Slogan nvarchar(max) NULL,
	MaxDiscount int NULL,
	LaunchDate date NULL,
	ShoeId bigint NOT NULL,
	CONSTRAINT PK_SalesCampaigns PRIMARY KEY (Id),
	CONSTRAINT FK_SalesCampaigns_Shoes FOREIGN KEY(ShoeId) REFERENCES dbo.Shoes (Id),
	INDEX IX_SalesCampaigns_ShoeId UNIQUE (ShoeId))

SET IDENTITY_INSERT dbo.SalesCampaigns ON
INSERT dbo.SalesCampaigns (Id, Slogan, MaxDiscount, LaunchDate, ShoeId) VALUES
	(1, N'Jet-Powered Shoes for the Win!', 20, CAST(N'2019-01-01' AS Date), 1),
	(2, N'"Blaze" a Trail with Side-Mounted Flame Throwers ', 15, CAST(N'2019-05-03' AS Date), 2),
	(3, N'All Surfaces. All Weathers. Victory Guaranteed.', 5, CAST(N'2020-01-01' AS Date), 3),
	(4, N'Contains an Actual Star to Dazzle Competitors', 25, CAST(N'2020-01-01' AS Date), 4)
SET IDENTITY_INSERT dbo.SalesCampaigns OFF
GO

-- 5) Categories table
CREATE TABLE Categories(
	Id bigint IDENTITY(1,1) NOT NULL,
	Name nvarchar(max) NOT NULL,
	CONSTRAINT PK_Categories PRIMARY KEY (id));

SET IDENTITY_INSERT dbo.Categories ON
INSERT dbo.Categories (Id, Name) VALUES
	(1, N'Road/Tarmac'),
	(2, N'Track'),
	(3, N'Trail'),
	(4, N'Road to Trail')
SET IDENTITY_INSERT dbo.Categories OFF
GO

-- 6) ShoeCategoryJunction table
CREATE TABLE ShoeCategoryJunction(
	Id bigint IDENTITY(1,1) NOT NULL,
	ShoeId bigint NOT NULL,
	CategoryId bigint NOT NULL,
	CONSTRAINT PK_ShoeCategoryJunction PRIMARY KEY (Id),
	CONSTRAINT FK_ShoeCategoryJunction_Categories FOREIGN KEY(CategoryId) REFERENCES dbo.Categories (Id),
	CONSTRAINT FK_ShoeCategoryJunction_Shoes FOREIGN KEY(ShoeId) REFERENCES dbo.Shoes (Id))

SET IDENTITY_INSERT dbo.ShoeCategoryJunction ON
INSERT dbo.ShoeCategoryJunction (Id, ShoeId, CategoryId) VALUES
	(1, 1, 1),
	(2, 2, 3),
	(3, 2, 4),
	(4, 3, 1),
	(5, 3, 2),
	(6, 3, 3),
	(7, 3, 4),
	(8, 4, 2)
SET IDENTITY_INSERT dbo.ShoeCategoryJunction OFF
GO