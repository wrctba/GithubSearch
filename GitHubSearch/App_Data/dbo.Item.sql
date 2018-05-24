CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [name] VARCHAR(128) NOT NULL, 
    [owner] VARCHAR(128) NOT NULL, 
    [description] VARCHAR(254) NOT NULL, 
    [language] VARCHAR(50) NOT NULL, 
    [details] NVARCHAR(MAX) NOT NULL
)
