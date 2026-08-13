IF COL_LENGTH('dbo.almCabecera', 'glosa') IS NOT NULL
BEGIN
    ALTER TABLE dbo.almCabecera
    DROP COLUMN glosa;
END;
GO

