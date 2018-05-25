CREATE TABLE [dbo].[Language]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] VARCHAR(50) NOT NULL, 
    [active] BIT NOT NULL
)

INSERT INTO Language (name, active) VALUES
('ASP', 1),
('Java', 1),
('C', 1),
('PHP', 1),
('Python', 1)
