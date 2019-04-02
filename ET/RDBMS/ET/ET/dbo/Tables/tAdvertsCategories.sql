CREATE TABLE dbo.tAdvertsCategories
(
	AdvertID INT NOT NULL,
	CategoryID INT NOT NULL,
	CONSTRAINT PK_tAdvertsCategories_tAdverts_tCategories PRIMARY KEY CLUSTERED (AdvertID ASC, CategoryID ASC),
	CONSTRAINT FK_tAtdvertsCategories_tAdverts FOREIGN KEY (AdvertID) REFERENCES dbo.tAdverts (AdvertID),
	CONSTRAINT FK_tAdvertsCategories_tCategories FOREIGN KEY (CategoryID) REFERENCES dbo.tCategories (CategoryID)
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about adverts and categories.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tAdvertsCategories';
GO