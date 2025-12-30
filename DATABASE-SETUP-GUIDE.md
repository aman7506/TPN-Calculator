# =============================================
# TPN Calculations - Local Database Setup Guide
# =============================================
# Complete step-by-step guide to setup database locally
# =============================================

## Database Information
- **Remote Server:** 172.1.3.201  
- **Local Server:** localhost/MSSQLSERVER
- **Database Name:** TPNCalculations
- **Tables:** 9 tables
- **Stored Procedures:** 10 procedures

## Tables Found:
1. Authenticate
2. Dosing_Wt
3. Syringe_1
4. Syringe_2
5. Tbl_ExceptionLoggingToDataBase
6. Total_Calculations
7. Trigerred_Data
8. Usermaster
9. Users

## Stored Procedures Found:
1. ExceptionLoggingToDataBase
2. GetTriggeredPatientDataByWard
3. InsertTriggeredData
4. InsertUser
5. sp_GetAllTransactionsByDate
6. sp_Preview_TPN_Inserts
7. usp_AutoFillDataDetails
8. USP_insert_Dosing_Wt
9. usp_UpdateDischargeDate
10. usp_UpdatePatientNamesFromRemote

=============================================
## OPTION 1: Using SQL Server Management Studio (SSMS) - EASIEST
=============================================

### Step 1: Open SSMS
- Connect to **172.1.3.201**
- Username: sa
- Password: sql123

### Step 2: Generate Script
1. Right-click on "TPNCalculations" database
2. Tasks → Generate Scripts
3. Select "Select specific database objects"
4. Check "Tables" and "Stored Procedures"
5. Click "Next"
6. Choose "Save to file": `e:\TNS_Calculations\TPNCalculations-CompleteScript.sql`
7. Click "Advanced" button
8. Set "Types of data to script" → **Schema and data**
9. Click "OK" → "Next" → "Finish"

### Step 3: Execute on Local Server
1. Connect to **localhost** in SSMS
2. Open the saved script: `TPNCalculations-CompleteScript.sql`
3. Press F5 to execute
4. ✅ Done! Database created locally with all data

=============================================
## OPTION 2: Using Backup/Restore - FASTEST
=============================================

### Step 1: Create Backup on Remote Server
```sql
-- Run this on remote server (172.1.3.201)
BACKUP DATABASE [TPNCalculations] 
TO DISK = N'C:\Backup\TPNCalculations.bak' 
WITH FORMAT, INIT, COMPRESSION, STATS = 10
GO
```

### Step 2: Copy Backup File
- Copy from: `\\172.1.3.201\C$\Backup\TPNCalculations.bak`
- To: `e:\TNS_Calculations\TPNCalculations.bak`

### Step 3: Restore on Local Server
```powershell
sqlcmd -S localhost -E -Q "RESTORE DATABASE [TPNCalculations] FROM DISK = 'e:\TNS_Calculations\TPNCalculations.bak' WITH REPLACE, MOVE 'TPNCalculations' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TPNCalculations.mdf', MOVE 'TPNCalculations_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TPNCalculations_log.ldf'"
```

=============================================
## OPTION 3: Using PowerShell Script - AUTOMATED
=============================================

### Step 1: Run the extraction script
```powershell
# Script already created: Extract-CompleteDatabase.ps1
powershell.exe -ExecutionPolicy Bypass -File "e:\TNS_Calculations\Extract-CompleteDatabase.ps1"
```

This will:
- Connect to 172.1.3.201
- Extract all table schemas
- Extract all stored procedures  
- Extract sample data
- Save to: `TPNCalculations-FullBackup.sql`

### Step 2: Import on local server
```powershell
sqlcmd -S localhost -E -i "e:\TNS_Calculations\TPNCalculations-FullBackup.sql"
```

=============================================
## UPDATE WEB.CONFIG (IMPORTANT!)
=============================================

After database is setup locally, update the connection string:

### Current (Remote):
```xml
<connectionStrings>
    <add name="TPNCalculations"
         connectionString="Server=172.1.3.201;Initial Catalog=TPNCalculations;User ID=sa;Password=sql123;"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

### New (Local with Windows Authentication):
```xml
<connectionStrings>
    <add name="TPNCalculations"
         connectionString="Server=localhost;Initial Catalog=TPNCalculations;Integrated Security=True;"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

### OR New (Local with SQL Authentication):
```xml
<connectionStrings>
    <add name="TPNCalculations"
         connectionString="Server=localhost;Initial Catalog=TPNCalculations;User ID=sa;Password=YourLocalPassword;"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

=============================================
## VERIFY DATABASE IS WORKING
=============================================

### Test Connection:
```powershell
sqlcmd -S localhost -E -d TPNCalculations -Q "SELECT COUNT(*) as TableCount FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
```

Expected output: **9 tables**

### Test Stored Procedure:
```powershell
sqlcmd -S localhost -E -d TPNCalculations -Q "SELECT name FROM sys.procedures"
```

Expected output: **10 procedures**

=============================================
## RESTART APPLICATION
=============================================

After updating Web.config:

1. Stop IIS Express:
```powershell
Get-Process iisexpress | Stop-Process -Force
```

2. Restart IIS Express:
```powershell
& "C:\Program Files\IIS Express\iisexpress.exe" /path:"e:\TNS_Calculations\TNS_Calculations" /port:44399
```

3. Test: http://localhost:44399/UserLogin.aspx

=============================================
## RECOMMENDED APPROACH
=============================================

**I recommend OPTION 1 (SSMS)** because:
✅ Most reliable
✅ Includes schema + data
✅ GUI-based, easy to verify
✅ Can preview before execution
✅ No manual scripting needed

=============================================
## NEED HELP?
=============================================

If you don't have SSMS installed, download from:
https://aka.ms/ssmsfullsetup

Or let me know and I'll create a PowerShell automation script!

=============================================
