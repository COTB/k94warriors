DECLARE @userTypes TABLE ( ID int, Name varchar(100) );

INSERT @userTypes VALUES (0, 'Unknown'), (1, 'Administrator'), (2, 'Trainer'), (3, 'Volunteer');

MERGE INTO UserTypes AS target
USING (SELECT * FROM @userTypes) AS source ON target.ID = source.ID
WHEN NOT MATCHED THEN INSERT VALUES (source.ID, source.Name);