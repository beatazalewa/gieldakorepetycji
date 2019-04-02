CREATE TABLE dbo.tCategories
(
CategoryID INT IDENTITY(1,1) NOT NULL,
CategoryName NVARCHAR(50) NOT NULL,
ParentID INT NULL,
CONSTRAINT PK_tCategories_CategoryID PRIMARY KEY CLUSTERED (CategoryID ASC)
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains all categories and subcategories.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tCategories';
GO