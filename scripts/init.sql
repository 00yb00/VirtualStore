-- Create the Northwind database
IF DB_ID('Northwind') IS NULL
BEGIN
    CREATE DATABASE Northwind;
END
GO

-- Use the Northwind database
USE Northwind;
GO
