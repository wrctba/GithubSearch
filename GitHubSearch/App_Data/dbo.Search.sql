﻿CREATE TABLE [dbo].[Search]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [date] DATETIME NOT NULL, 
    [asp] BIT NOT NULL DEFAULT 0, 
    [php] BIT NOT NULL DEFAULT 0, 
    [java] BIT NOT NULL DEFAULT 0, 
    [c] BIT NOT NULL DEFAULT 0, 
    [python] BIT NOT NULL DEFAULT 0
)
