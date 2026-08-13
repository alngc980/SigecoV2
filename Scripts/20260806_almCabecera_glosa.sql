IF COL_LENGTH('dbo.almCabecera', 'glosa') IS NULL
BEGIN
    ALTER TABLE dbo.almCabecera
    ADD glosa VARCHAR(250) NULL;
END;
GO

