-- =====================================================
-- TPN Calculations - Local Database Setup Script
-- =====================================================
-- This script will help you setup the database locally
-- Author: Generated for TNS_Calculations Project
-- Date: 2025-12-25
-- =====================================================

-- Step 1: Create Database
USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TPNCalculations')
BEGIN
    CREATE DATABASE TPNCalculations;
    PRINT 'Database TPNCalculations created successfully!';
END
ELSE
BEGIN
    PRINT 'Database TPNCalculations already exists.';
END
GO

USE TPNCalculations;
GO

-- =====================================================
-- Step 2: Create Sample Tables (Basic Structure)
-- =====================================================
-- Note: You should replace this with actual backup restore
-- This is just a template structure

-- Users Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserId INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) NOT NULL UNIQUE,
        Password NVARCHAR(255) NOT NULL,
        FullName NVARCHAR(100),
        Email NVARCHAR(100),
        Role NVARCHAR(50),
        CreatedDate DATETIME DEFAULT GETDATE(),
        IsActive BIT DEFAULT 1
    );
    PRINT 'Users table created successfully!';
END
GO

-- TPN Calculations Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TPNCalculations_Data')
BEGIN
    CREATE TABLE TPNCalculations_Data (
        CalculationId INT PRIMARY KEY IDENTITY(1,1),
        UserId INT,
        BabyName NVARCHAR(100),
        BabyWeight DECIMAL(10,2),
        Syringe1_Aminoven_Per DECIMAL(10,2),
        Syringe1_Aminoven_50ml DECIMAL(10,2),
        Syringe1_NaCl_Per DECIMAL(10,2),
        Syringe1_NaCl_50ml DECIMAL(10,2),
        Syringe2_Aminoven_Per DECIMAL(10,2),
        Syringe2_Aminoven_50ml DECIMAL(10,2),
        Syringe2_NaCl_Per DECIMAL(10,2),
        Syringe2_NaCl_50ml DECIMAL(10,2),
        TotalVolume DECIMAL(10,2),
        TotalPer50ml DECIMAL(10,2),
        CreatedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
    PRINT 'TPNCalculations_Data table created successfully!';
END
GO

-- =====================================================
-- Step 3: Insert Sample Admin User
-- =====================================================
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    INSERT INTO Users (Username, Password, FullName, Email, Role, IsActive)
    VALUES ('admin', 'admin123', 'Administrator', 'admin@tpn.com', 'Admin', 1);
    PRINT 'Sample admin user created successfully!';
    PRINT 'Username: admin, Password: admin123';
END
GO

-- =====================================================
-- Step 4: Verification
-- =====================================================
PRINT '=====================================================';
PRINT 'Database Setup Complete!';
PRINT '=====================================================';
PRINT 'Database Name: TPNCalculations';
PRINT 'Tables Created: Users, TPNCalculations_Data';
PRINT 'Sample User: admin / admin123';
PRINT '=====================================================';
PRINT '';
PRINT 'IMPORTANT: This is a basic structure.';
PRINT 'To get actual data from remote server:';
PRINT '1. Take backup from 172.1.3.201';
PRINT '2. Restore it on localhost';
PRINT '3. Update connection string in Web.config';
PRINT '=====================================================';

SELECT 'Database' as Component, name as Name, create_date as CreatedDate 
FROM sys.databases 
WHERE name = 'TPNCalculations';

SELECT 'Tables' as Component, name as Name, create_date as CreatedDate 
FROM sys.tables 
ORDER BY name;
