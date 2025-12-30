# TPN CALCULATIONS PROJECT

## ✅ COMPLETE LOCAL SETUP

---

## 🚀 QUICK START (Already Done!)

**Current Status:**
- ✅ Angular Frontend: Running on http://localhost:4200/
- ✅ ASP.NET Backend: Running on http://localhost:44399/
- ⚠️ Database: Using remote server (172.1.3.201)

**Next Step:** Migrate database to localhost (see below)

---

## 📦 PROJECT STRUCTURE

```
e:\TNS_Calculations\
├── TPN-Calculator-Angular\    # Angular Frontend
├── TNS_Calculations\           # ASP.NET Backend
├── START-ALL.bat               # ⭐ Start everything
├── STOP-ALL.bat                # Stop everything
└── COMPLETE-SETUP-GUIDE.txt    # Full setup guide
```

---

## 🎯 HOW TO USE

### Start Project:
**Double-click**: `START-ALL.bat`

This will:
1. Start Angular (port 4200)
2. Start ASP.NET Backend (port 44399)
3. Open both in browser

### Stop Project:
**Double-click**: `STOP-ALL.bat`

---

## 💾 DATABASE MIGRATION TO LOCALHOST

### Current: Using Remote Database
**Server:** 172.1.3.201  
**Database:** TPNCalculations

### Goal: Use Local Database
**Server:** localhost  
**Database:** TPNCalculations

### Steps:

#### 1. Generate Database Script (Using SSMS):
1. Open SQL Server Management Studio
2. Connect to `172.1.3.201` (Login: sa/sql123)
3. Right-click `TPNCalculations` database → Tasks → **Generate Scripts**
4. Click "Next"
5. Select "Select specific database objects"
6. Check **Tables** and **Stored Procedures** → Click "Next"
7. Click "Advanced" button
8. Find "Types of data to script" → Select **"Schema and data"**
9. Click "OK" → "Next"
10. Choose "Save to file" → Browse to: `e:\TNS_Calculations\DB-Complete.sql`
11. Click "Finish"

#### 2. Create Local Database:
1. Connect to `localhost` in SSMS
2. Open file: `e:\TNS_Calculations\DB-Complete.sql`
3. Press **F5** to execute
4. ✅ Database created with all data!

#### 3. Update Web.config:
**File:** `e:\TNS_Calculations\TNS_Calculations\Web.config`

**Find (line ~10):**
```xml
connectionString="Server=172.1.3.201;Initial Catalog=TPNCalculations;User ID=sa;Password=sql123;"
```

**Replace with:**
```xml
connectionString="Server=localhost;Initial Catalog=TPNCalculations;Integrated Security=True;"
```

**Save** the file.

#### 4. Restart Backend:
1. Run: `STOP-ALL.bat`
2. Run: `START-ALL.bat`

#### 5. Test:
1. Open: http://localhost:4200/
2. Fill some data
3. Click Save
4. Open SSMS → localhost → TPNCalculations → Tables
5. Right-click any table → "Select Top 1000 Rows"
6. ✅ Your data should be there!

---

## 🔧 MANUAL CONTROL

### Start Angular Only:
```cmd
cd e:\TNS_Calculations\TPN-Calculator-Angular
npm start
```

### Start Backend Only:
```cmd
"C:\Program Files\IIS Express\iisexpress.exe" /path:"e:\TNS_Calculations\TNS_Calculations" /port:44399
```

---

## 📊 DATABASE INFO

**Tables (9):**
- Authenticate
- Dosing_Wt
- Syringe_1
- Syringe_2
- Tbl_ExceptionLoggingToDataBase
- Total_Calculations
- Trigerred_Data
- Usermaster
- Users

**Stored Procedures (10):**
- ExceptionLoggingToDataBase
- GetTriggeredPatientDataByWard
- InsertTriggeredData
- InsertUser
- sp_GetAllTransactionsByDate
- sp_Preview_TPN_Inserts
- usp_AutoFillDataDetails
- USP_insert_Dosing_Wt
- usp_UpdateDischargeDate
- usp_UpdatePatientNamesFromRemote

---

## ❓ FAQ

### Q: IIS Express vs IIS Server?
**A:** IIS Express is lightweight developer tool. You're already using it! No complex setup needed.

### Q: Can I run without IIS Express?
**A:** No. ASP.NET Web Forms (.NET Framework) requires IIS/IIS Express. No alternative exists.

### Q: Where is data saved?
**A:** Currently on remote server (172.1.3.201). After migration, it will save to localhost.

### Q: How to verify everything is working?
**A:** 
1. Check ports: `netstat -ano | findstr ":4200 :44399"`
2. Should show both ports in use
3. Open URLs in browser and test

---

## 📞 SUPPORT

For issues, check:
1. **COMPLETE-SETUP-GUIDE.txt** - Detailed guide
2. **DATABASE-SETUP-GUIDE.md** - Database migration options

---

**Happy Coding! 🚀**
