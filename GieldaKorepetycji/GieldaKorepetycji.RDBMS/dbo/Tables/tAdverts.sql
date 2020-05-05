CREATE TABLE dbo.tAdverts
(
AdvertID INT IDENTITY (1,1) NOT NULL,
Title NVARCHAR(50) NOT NULL,
Content NVARCHAR(250) NOT NULL,
StartDate DateTime2(0) NOT NULL,
DueDate DateTime2(0) NOT NULL, 
UserID INT NOT NULL,
LocationID int NOT NULL,
CONSTRAINT PK_tAdverts_AdvertID PRIMARY KEY CLUSTERED (AdvertID ASC),
CONSTRAINT FK_tAdverts_Users FOREIGN KEY (UserID) REFERENCES dbo.tUsers (UserID),
CONSTRAINT FK_tAdverts_Locations FOREIGN KEY (LocationID) REFERENCES dbo.tLocations (LocationID)   
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains all adverts.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tAdverts';
GO