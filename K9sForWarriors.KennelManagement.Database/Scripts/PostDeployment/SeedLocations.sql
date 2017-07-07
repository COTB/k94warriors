IF NOT EXISTS (SELECT 1 FROM Locations)
BEGIN

SET IDENTITY_INSERT dbo.Locations ON;

INSERT INTO Locations (ID, [Name]) VALUES (1, 'Not on Premise')

SET IDENTITY_INSERT dbo.Locations OFF;

END