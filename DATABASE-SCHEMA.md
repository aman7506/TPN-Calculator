# 🗄️ DATABASE SCHEMA DOCUMENTATION

## TPN Calculator Database Structure

---

## 📊 DATABASE OVERVIEW

**Database Name:** `TPNCalculations`  
**Database Type:** SQL Server 2019+  
**Total Tables:** 9  
**Total Stored Procedures:** 10  
**File Location:** `database/schema.sql`

---

## 📋 TABLE OF CONTENTS

1. [Tables](#tables)
2. [Stored Procedures](#stored-procedures)
3. [Relationships](#relationships)
4. [Sample Queries](#sample-queries)

---

## 1. TABLES

### **1.1 Users** (Authentication)

Stores user login credentials and roles.

```sql
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL, -- Hashed
    Role NVARCHAR(20) NOT NULL, -- Doctor, Pharmacist, Nurse
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastLogin DATETIME,
    IsActive BIT DEFAULT 1
);
```

**Sample Data:**
```sql
INSERT INTO Users (Username, Password, Role) VALUES
('dr.smith', 'hashed_password', 'Doctor'),
('pharm.jones', 'hashed_password', 'Pharmacist');
```

---

### **1.2 Total_Calculations** (Main TPN Calculations)

Stores complete TPN calculation results.

```sql
CREATE TABLE Total_Calculations (
    CalculationId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    UserId INT NOT NULL,
    CalculationDate DATETIME DEFAULT GETDATE(),
    
    -- Patient Parameters
    Weight DECIMAL(5,2) NOT NULL, -- kg
    Height DECIMAL(5,2) NOT NULL, -- cm
    Age INT NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    
    -- Calculation Inputs
    StressFactor DECIMAL(3,2) NOT NULL,
    ActivityFactor DECIMAL(3,2) NOT NULL,
    ProteinFactor DECIMAL(3,2) NOT NULL,
    
    -- Calculation Results
    TotalCalories DECIMAL(7,2) NOT NULL,
    ProteinGrams DECIMAL(6,2) NOT NULL,
    DextroseGrams DECIMAL(6,2) NOT NULL,
    LipidGrams DECIMAL(6,2) NOT NULL,
    
    -- Electrolytes
    SodiumMeq DECIMAL(5,2),
    PotassiumMeq DECIMAL(5,2),
    CalciumMeq DECIMAL(5,2),
    MagnesiumMeq DECIMAL(5,2),
    PhosphorusMmol DECIMAL(5,2),
    
    -- Fluid
    TotalVolumeML DECIMAL(6,2),
    InfusionRateML DECIMAL(5,2),
    
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```

---

### **1.3 Trigerred_Data** (Patient Data from HIS/EMR)

Stores patient demographics fetched from hospital information system.

```sql
CREATE TABLE Trigerred_Data (
    TriggerId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL UNIQUE,
    PatientName NVARCHAR(100) NOT NULL,
    HospitalId NVARCHAR(50) NOT NULL,
    DateOfBirth DATE,
    Ward NVARCHAR(50),
    AdmissionDate DATETIME,
    Diagnosis NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE()
);
```

---

### **1.4 Dosing_Wt** (Dosing Weight Calculations)

Stores ideal/adjusted body weight calculations.

```sql
CREATE TABLE Dosing_Wt (
    DosingId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    ActualWeight DECIMAL(5,2) NOT NULL,
    Height DECIMAL(5,2) NOT NULL,
    IdealBodyWeight DECIMAL(5,2),
    AdjustedBodyWeight DECIMAL(5,2),
    DosingWeight DECIMAL(5,2), -- Used for calculations
    BMI DECIMAL(4,2),
    CalculationDate DATETIME DEFAULT GETDATE()
);
```

**Formulas:**
```sql
-- IBW (Male) = 50 + 2.3 × (Height_inches - 60)
-- IBW (Female) = 45.5 + 2.3 × (Height_inches - 60)
-- BMI = Weight_kg / (Height_m)²
```

---

### **1.5 Syringe_1** (TPN Syringe Composition - Macronutrients)

Stores composition of TPN syringe #1 (dextrose, amino acids, lipids).

```sql
CREATE TABLE Syringe_1 (
    SyringeId INT IDENTITY(1,1) PRIMARY KEY,
    CalculationId INT NOT NULL,
    
    -- Dextrose
    DextroseConcentration DECIMAL(4,2), -- %
    DextroseVolume DECIMAL(6,2), -- ml
    
    -- Amino Acids
    AminoAcidConcentration DECIMAL(4,2), -- %
    AminoAcidVolume DECIMAL(6,2), -- ml
    
    -- Lipids
    LipidConcentration DECIMAL(4,2), -- % (usually 20%)
    LipidVolume DECIMAL(6,2), -- ml
    
    TotalVolumeML DECIMAL(6,2),
    InfusionDurationHours INT,
    
    FOREIGN KEY (CalculationId) REFERENCES Total_Calculations(CalculationId)
);
```

---

### **1.6 Syringe_2** (TPN Syringe Composition - Electrolytes)

Stores electrolyte additions to TPN.

```sql
CREATE TABLE Syringe_2 (
    SyringeId INT IDENTITY(1,1) PRIMARY KEY,
    CalculationId INT NOT NULL,
    
    -- Electrolytes (mEq or mmol)
    SodiumChloride DECIMAL(5,2),
    PotassiumChloride DECIMAL(5,2),
    CalciumGluconate DECIMAL(5,2),
    MagnesiumSulfate DECIMAL(5,2),
    SodiumPhosphate DECIMAL(5,2),
    
    -- Vitamins
    Multivitamins NVARCHAR(50),
    TraceElements NVARCHAR(50),
    
    FOREIGN KEY (CalculationId) REFERENCES Total_Calculations(CalculationId)
);
```

---

### **1.7 Usermaster** (Extended User Profiles)

Extended user information beyond authentication.

```sql
CREATE TABLE Usermaster (
    UserMasterId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL UNIQUE,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(15),
    Department NVARCHAR(50),
    LicenseNumber NVARCHAR(50),
    CreatedDate DATETIME DEFAULT GETDATE(),
    
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```

---

### **1.8 Authenticate** (Login Sessions)

Tracks user login sessions for security auditing.

```sql
CREATE TABLE Authenticate (
    AuthId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    LoginTime DATETIME DEFAULT GETDATE(),
    LogoutTime DATETIME,
    IPAddress NVARCHAR(45),
    SessionToken NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```

---

### **1.9 Tbl_ExceptionLoggingToDataBase** (Error Logs)

Logs application errors for debugging.

```sql
CREATE TABLE Tbl_ExceptionLoggingToDataBase (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    ExceptionMessage NVARCHAR(MAX),
    StackTrace NVARCHAR(MAX),
    ErrorSource NVARCHAR(255),
    ErrorPage NVARCHAR(255),
    UserId INT,
    ErrorDate DATETIME DEFAULT GETDATE()
);
```

---

## 2. STORED PROCEDURES

### **2.1 sp_Preview_TPN_Inserts**

Calculates and returns TPN prescription without saving.

```sql
CREATE PROCEDURE sp_Preview_TPN_Inserts
    @Weight DECIMAL(5,2),
    @Height DECIMAL(5,2),
    @Age INT,
    @Gender NVARCHAR(10),
    @StressFactor DECIMAL(3,2),
    @ProteinFactor DECIMAL(3,2)
AS
BEGIN
    -- Calculate BEE
    DECLARE @BEE DECIMAL(7,2);
    
    IF @Gender = 'Male'
        SET @BEE = 66.5 + (13.75 * @Weight) + (5.003 * @Height) - (6.755 * @Age);
    ELSE
        SET @BEE = 655.1 + (9.563 * @Weight) + (1.850 * @Height) - (4.676 * @Age);
    
    -- Calculate TEE
    DECLARE @TotalCalories DECIMAL(7,2);
    SET @TotalCalories = @BEE * 1.2 * @StressFactor;
    
    -- Calculate Protein
    DECLARE @ProteinGrams DECIMAL(6,2);
    SET @ProteinGrams = @Weight * @ProteinFactor;
    
    -- Calculate Dextrose (60% of calories)
    DECLARE @DextroseGrams DECIMAL(6,2);
    SET @DextroseGrams = (@TotalCalories * 0.60) / 3.4;
    
    -- Calculate Lipids (30% of calories)
    DECLARE @LipidGrams DECIMAL(6,2);
    SET @LipidGrams = (@TotalCalories * 0.30) / 9;
    
    -- Return results
    SELECT 
        @TotalCalories AS TotalCalories,
        @ProteinGrams AS ProteinGrams,
        @DextroseGrams AS DextroseGrams,
        @LipidGrams AS LipidGrams;
END;
```

---

### **2.2 sp_GetAllTransactionsByDate**

Retrieves all TPN calculations for a specific date.

```sql
CREATE PROCEDURE sp_GetAllTransactionsByDate
    @CalculationDate DATE
AS
BEGIN
    SELECT 
        tc.CalculationId,
        td.PatientName,
        td.HospitalId,
        tc.TotalCalories,
        tc.ProteinGrams,
        tc.DextroseGrams,
        tc.LipidGrams,
        tc.CalculationDate,
        u.Username AS CalculatedBy
    FROM Total_Calculations tc
    INNER JOIN Trigerred_Data td ON tc.PatientId = td.PatientId
    INNER JOIN Users u ON tc.UserId = u.UserId
    WHERE CAST(tc.CalculationDate AS DATE) = @CalculationDate
    ORDER BY tc.CalculationDate DESC;
END;
```

---

### **2.3 USP_insert_Dosing_Wt**

Calculates and inserts ideal/adjusted body weight.

```sql
CREATE PROCEDURE USP_insert_Dosing_Wt
    @PatientId INT,
    @ActualWeight DECIMAL(5,2),
    @Height DECIMAL(5,2),
    @Gender NVARCHAR(10)
AS
BEGIN
    DECLARE @HeightInches DECIMAL(5,2) = @Height / 2.54;
    DECLARE @IBW DECIMAL(5,2);
    DECLARE @AdjBW DECIMAL(5,2);
    DECLARE @BMI DECIMAL(4,2) = @ActualWeight / POWER(@Height/100, 2);
    
    -- Calculate IBW
    IF @Gender = 'Male'
        SET @IBW = 50 + (2.3 * (@HeightInches - 60));
    ELSE
        SET @IBW = 45.5 + (2.3 * (@HeightInches - 60));
    
    -- Calculate Adjusted BW if obese (ABW > IBW)
    IF @ActualWeight > (@IBW * 1.2)
        SET @AdjBW = @IBW + ((@ActualWeight - @IBW) * 0.4);
    ELSE
        SET @AdjBW = @ActualWeight;
    
    INSERT INTO Dosing_Wt (PatientId, ActualWeight, Height, IdealBodyWeight, AdjustedBodyWeight, DosingWeight, BMI)
    VALUES (@PatientId, @ActualWeight, @Height, @IBW, @AdjBW, @AdjBW, @BMI);
END;
```

---

### **2.4 GetTriggeredPatientDataByWard**

Retrieves patient list by ward for TPN calculations.

```sql
CREATE PROCEDURE GetTriggeredPatientDataByWard
    @Ward NVARCHAR(50)
AS
BEGIN
    SELECT 
        PatientId,
        PatientName,
        HospitalId,
        DateOfBirth,
        Ward,
        Diagnosis,
        AdmissionDate
    FROM Trigerred_Data
    WHERE Ward = @Ward AND PatientId IN (
        SELECT DISTINCT PatientId 
        FROM Total_Calculations 
        WHERE CalculationDate >= DATEADD(DAY, -7, GETDATE())
    )
    ORDER BY PatientName;
END;
```

---

### **2.5 ExceptionLoggingToDataBase**

Logs application exceptions.

```sql
CREATE PROCEDURE ExceptionLoggingToDataBase
    @ExceptionMessage NVARCHAR(MAX),
    @StackTrace NVARCHAR(MAX),
    @ErrorSource NVARCHAR(255),
    @ErrorPage NVARCHAR(255),
    @UserId INT = NULL
AS
BEGIN
    INSERT INTO Tbl_ExceptionLoggingToDataBase (ExceptionMessage, StackTrace, ErrorSource, ErrorPage, UserId)
    VALUES (@ExceptionMessage, @StackTrace, @ErrorSource, @ErrorPage, @UserId);
END;
```

---

## 3. RELATIONSHIPS

### **Entity Relationship Diagram (ERD)**

```
Users (1) ─────< (M) Total_Calculations
                      │
                      ├───< (1) Syringe_1
                      └───< (1) Syringe_2

Users (1) ───── (1) Usermaster
Users (1) ─────< (M) Authenticate

Trigerred_Data (1) ────< (M) Total_Calculations
Trigerred_Data (1) ────< (M) Dosing_Wt
```

---

## 4. SAMPLE QUERIES

### **4.1 Get Patient's Latest TPN Calculation**

```sql
SELECT TOP 1
    td.PatientName,
    td.HospitalId,
    tc.TotalCalories,
    tc.ProteinGrams,
    tc.DextroseGrams,
    tc.LipidGrams,
    tc.CalculationDate,
    u.Username AS CalculatedBy
FROM Total_Calculations tc
INNER JOIN Trigerred_Data td ON tc.PatientId = td.PatientId
INNER JOIN Users u ON tc.UserId = u.UserId
WHERE td.PatientId = 12345
ORDER BY tc.CalculationDate DESC;
```

---

### **4.2 Get All TPN Calculations for Today**

```sql
SELECT 
    td.PatientName,
    tc.TotalCalories,
    tc.ProteinGrams,
    u.Username
FROM Total_Calculations tc
INNER JOIN Trigerred_Data td ON tc.PatientId = td.PatientId
INNER JOIN Users u ON tc.UserId = u.UserId
WHERE CAST(tc.CalculationDate AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY tc.CalculationDate DESC;
```

---

### **4.3 Get User Activity Report**

```sql
SELECT 
    u.Username,
    COUNT(tc.CalculationId) AS TotalCalculations,
    MIN(tc.CalculationDate) AS FirstCalculation,
    MAX(tc.CalculationDate) AS LastCalculation
FROM Users u
LEFT JOIN Total_Calculations tc ON u.UserId = tc.UserId
GROUP BY u.Username
ORDER BY TotalCalculations DESC;
```

---

### **4.4 Find High-Risk Calculations (High Calories)**

```sql
SELECT 
    td.PatientName,
    tc.Weight,
    tc.TotalCalories,
    (tc.TotalCalories / tc.Weight) AS CaloriesPerKg
FROM Total_Calculations tc
INNER JOIN Trigerred_Data td ON tc.PatientId = td.PatientId
WHERE (tc.TotalCalories / tc.Weight) > 40 -- Alert if >40 kcal/kg
ORDER BY CaloriesPerKg DESC;
```

---

## 📦 DATABASE SETUP

### **Create Database:**

```sql
CREATE DATABASE TPNCalculations;
GO

USE TPNCalculations;
GO
```

### **Run Schema:**

```bash
# Using SSMS
1. Open SQL Server Management Studio
2. Connect to localhost
3. File → Open → database/schema.sql
4. Execute (F5)

# Using Command Line
sqlcmd -S localhost -i database\schema.sql
```

---

## 🔒 SECURITY CONSIDERATIONS

### **1. Password Hashing:**
```csharp
// Never store plain text passwords
// Use bcrypt or PBKDF2
string hashedPassword = BCrypt.HashPassword(plainPassword);
```

### **2. SQL Injection Prevention:**
```csharp
// Always use parameterized queries
SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @username", conn);
cmd.Parameters.AddWithValue("@username", username);
```

### **3. Connection String Encryption:**
```xml
<!-- Encrypt connection string in Web.config -->
aspnet_regiis -pef "connectionStrings" "path\to\app"
```

---

**Last Updated:** December 30, 2025  
**Database Version:** 1.0.0  
**Compatible with:** SQL Server 2019+
