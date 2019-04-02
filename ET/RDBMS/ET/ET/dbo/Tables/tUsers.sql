﻿CREATE TABLE dbo.tUsers
(
	UserID INT IDENTITY (1,1) NOT NULL,
	Nickname NVARCHAR(8) NOT NULL,
	Pass NVARCHAR(12) NOT NULL,
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(25) NOT NULL,
	Age INT NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	PhoneNumber NVARCHAR(11),
	ImageColumn VARBINARY (MAX),
	CONSTRAINT PK_tUsers_UserID PRIMARY KEY CLUSTERED (UserID ASC),
	CONSTRAINT AK_tUsers_Nickname UNIQUE (Nickname ASC)
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about user.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tUsers';
GO
