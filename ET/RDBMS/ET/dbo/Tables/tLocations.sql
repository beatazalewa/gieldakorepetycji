CREATE TABLE dbo.tLocations
(
LocationID INT IDENTITY (1,1) NOT NULL,
Country NVARCHAR(50) NOT NULL,
Region NVARCHAR(50) NOT NULL,
County NVARCHAR(50) NULL,
City NVARCHAR(50) NOT NULL,
CONSTRAINT PK_tLocations_LocationID PRIMARY KEY CLUSTERED (LocationID ASC) 
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains possible locations.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tLocations';
GO

