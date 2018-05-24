CREATE TABLE [dbo].[SearchItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SearchId] INT NOT NULL, 
    [ItemId] INT NOT NULL, 
    CONSTRAINT [FK_SearchItem_ToSearch] FOREIGN KEY ([SearchId]) REFERENCES [Search]([id]),
	CONSTRAINT [FK_SearchItem_ToItem] FOREIGN KEY ([ItemId]) REFERENCES [Item]([id])
)
