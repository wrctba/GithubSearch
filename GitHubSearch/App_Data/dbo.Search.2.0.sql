DECLARE @ConstraintName nvarchar(200)
SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS WHERE PARENT_OBJECT_ID = OBJECT_ID('Search') AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns WHERE NAME = N'asp' AND object_id = OBJECT_ID(N'Search'))
IF @ConstraintName IS NOT NULL
EXEC('ALTER TABLE Search DROP CONSTRAINT ' + @ConstraintName)
IF EXISTS (SELECT * FROM syscolumns WHERE id=object_id('Search') AND name='asp')
EXEC('ALTER TABLE Search DROP COLUMN asp');

SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS WHERE PARENT_OBJECT_ID = OBJECT_ID('Search') AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns WHERE NAME = N'java' AND object_id = OBJECT_ID(N'Search'))
IF @ConstraintName IS NOT NULL
EXEC('ALTER TABLE Search DROP CONSTRAINT ' + @ConstraintName)
IF EXISTS (SELECT * FROM syscolumns WHERE id=object_id('Search') AND name='java')
EXEC('ALTER TABLE Search DROP COLUMN java');

SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS WHERE PARENT_OBJECT_ID = OBJECT_ID('Search') AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns WHERE NAME = N'php' AND object_id = OBJECT_ID(N'Search'))
IF @ConstraintName IS NOT NULL
EXEC('ALTER TABLE Search DROP CONSTRAINT ' + @ConstraintName)
IF EXISTS (SELECT * FROM syscolumns WHERE id=object_id('Search') AND name='php')
EXEC('ALTER TABLE Search DROP COLUMN php');

SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS WHERE PARENT_OBJECT_ID = OBJECT_ID('Search') AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns WHERE NAME = N'python' AND object_id = OBJECT_ID(N'Search'))
IF @ConstraintName IS NOT NULL
EXEC('ALTER TABLE Search DROP CONSTRAINT ' + @ConstraintName)
IF EXISTS (SELECT * FROM syscolumns WHERE id=object_id('Search') AND name='python')
EXEC('ALTER TABLE Search DROP COLUMN python');

SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS WHERE PARENT_OBJECT_ID = OBJECT_ID('Search') AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns WHERE NAME = N'c' AND object_id = OBJECT_ID(N'Search'))
IF @ConstraintName IS NOT NULL
EXEC('ALTER TABLE Search DROP CONSTRAINT ' + @ConstraintName)
IF EXISTS (SELECT * FROM syscolumns WHERE id=object_id('Search') AND name='c')
EXEC('ALTER TABLE Search DROP COLUMN c');