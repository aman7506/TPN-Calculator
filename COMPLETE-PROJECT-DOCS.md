# TPN Calculator - Complete Project Documentation

**Version:** 1.0.0  
**Author:** Aman Mishra  
**Repository:** https://github.com/aman7506/TPN-Calculator  
**License:** MIT  
**Last Updated:** December 31, 2025

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Project Structure](#project-structure)
3. [Technology Stack](#technology-stack)
4. [System Architecture](#system-architecture)
5. [Database Schema](#database-schema)
6. [Calculation Logic](#calculation-logic)
7. [API Documentation](#api-documentation)
8. [Installation Guide](#installation-guide)
9. [Deployment Guide](#deployment-guide)
10. [Testing Procedures](#testing-procedures)
11. [Medical Disclaimer](#medical-disclaimer)

---

## 1. Project Overview

### What is TPN?

Total Parenteral Nutrition (TPN) is intravenous feeding that provides complete nutrition directly into the bloodstream for patients who cannot receive nutrition through normal digestive routes.

### Project Purpose

This web application automates complex TPN calculations to:
- Eliminate manual calculation errors
- Reduce calculation time from 20 minutes to under 2 minutes
- Standardize TPN prescriptions across medical facilities
- Provide safety validations and warnings
- Maintain audit trail for regulatory compliance

### Target Users

- Doctors (Intensivists, Pediatricians)
- Clinical Pharmacists
- Registered Nurses (ICU, Neonatal)
- Nutrition Support Teams

### Key Features

- Automated calorie calculations (Harris-Benedict equation)
- Protein requirements based on stress level (0.8 - 2.5 g/kg)
- Carbohydrate (dextrose) with glucose infusion rate monitoring
- Lipid (fat emulsion) with maximum dose checking
- Electrolyte requirements (Na, K, Ca, Mg, PO4)
- Input validation and safety warnings
- Print-ready prescription output
- Calculation history and database storage

---

## 2. Project Structure

```
TPN-Calculator/
│
├── TPN-Calculator-Angular/              # Frontend Application
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   │   ├── tpn-form/
│   │   │   │   │   ├── tpn-form.component.ts
│   │   │   │   │   ├── tpn-form.component.html
│   │   │   │   │   └── tpn-form.component.css
│   │   │   │   ├── results/
│   │   │   │   │   ├── results.component.ts
│   │   │   │   │   ├── results.component.html
│   │   │   │   │   └── results.component.css
│   │   │   │   └── history/
│   │   │   │       ├── history.component.ts
│   │   │   │       ├── history.component.html
│   │   │   │       └── history.component.css
│   │   │   ├── services/
│   │   │   │   ├── tpn.service.ts
│   │   │   │   ├── calculation.service.ts
│   │   │   │   └── api.service.ts
│   │   │   ├── models/
│   │   │   │   ├── patient.model.ts
│   │   │   │   ├── calculation.model.ts
│   │   │   │   └── result.model.ts
│   │   │   └── app.module.ts
│   │   ├── assets/
│   │   ├── environments/
│   │   │   ├── environment.ts
│   │   │   └── environment.prod.ts
│   │   ├── index.html
│   │   ├── main.ts
│   │   └── styles.css
│   ├── angular.json
│   ├── package.json
│   ├── tsconfig.json
│   └── README.md
│
├── TPN_Calculations/                    # Backend Application
│   ├── App_Start/
│   │   ├── BundleConfig.cs
│   │   └── RouteConfig.cs
│   ├── Content/
│   │   └── (CSS files)
│   ├── Scripts/
│   │   └── (JavaScript files)
│   ├── TPNMAIN.aspx                    # Main calculation page
│   ├── TPNMAIN.aspx.cs                 # Core calculation logic
│   ├── TransactionPage.aspx            # Transaction history
│   ├── UserLogin.aspx                   # Authentication
│   ├── Utility.cs                       # Helper functions
│   ├── ExceptionLogging.cs             # Error logging
│   ├── Web.config                       # Configuration (not in Git)
│   ├── Web.config.example              # Configuration template
│   └── bin/                            # Compiled assemblies
│
├── database/                            # Database Files
│   ├── schema.sql                      # Table structures
│   ├── stored-procedures.sql           # All procedures
│   └── sample-data.sql                 # Test data
│
├── docs/                                # Documentation
│   ├── CALCULATION-LOGIC.md
│   ├── DATABASE-SCHEMA.md
│   ├── DEPLOYMENT-GUIDE.md
│   ├── GITHUB-SETUP-GUIDE.md
│   ├── PROJECT-DOCUMENTATION.md
│   ├── QUICK-START-DEPLOYMENT.md
│   ├── START-HERE.md
│   ├── STEP-BY-STEP-UPLOAD-GUIDE.md
│   ├── TESTING-GUIDE.md
│   └── UPLOAD-CHECKLIST.md
│
├── .gitignore                          # Git ignore rules
├── LICENSE                             # MIT License
├── README.md                           # Main documentation
└── netlify.toml                        # Netlify configuration

Total Files: ~870
Total Documentation: 14 comprehensive guides
```

---

## 3. Technology Stack

### Frontend Technologies

| Technology | Version | Purpose |
|------------|---------|---------|
| Angular | 15.2.10 | SPA framework |
| TypeScript | 4.8.4 | Type-safe JavaScript |
| Angular Material | 15.2.9 | UI components |
| Bootstrap | 5.3.3 | Responsive layout |
| RxJS | 7.8.0 | Reactive programming |

### Backend Technologies

| Technology | Version | Purpose |
|------------|---------|---------|
| ASP.NET Web Forms | .NET Framework 4.x | Server-side logic |
| C# | 7.3+ | Programming language |
| ADO.NET | - | Database access |
| IIS Express | 10.0 | Development server |

### Database

| Technology | Version | Purpose |
|------------|---------|---------|
| SQL Server | 2019+ | Relational database |
| T-SQL | - | Query language |

### Development Tools

- Visual Studio 2022 (Backend)
- VS Code (Frontend)
- Git (Version control)
- npm (Package manager)
- SQL Server Management Studio (Database)

---

## 4. System Architecture

### High-Level Architecture

```
┌─────────────────────────────────────┐
│         USER LAYER                  │
│  (Doctors, Nurses, Pharmacists)     │
└──────────────┬──────────────────────┘
               │ HTTPS
               ▼
┌─────────────────────────────────────┐
│    PRESENTATION LAYER               │
│    Angular 15 (SPA)                 │
│    - Reactive Forms                 │
│    - Material UI                    │
│    - Client Validation              │
└──────────────┬──────────────────────┘
               │ HTTP/AJAX
               ▼
┌─────────────────────────────────────┐
│    APPLICATION LAYER                │
│    ASP.NET Web Forms                │
│    - Business Logic                 │
│    - Calculation Algorithms         │
│    - Server Validation              │
└──────────────┬──────────────────────┘
               │ ADO.NET
               ▼
┌─────────────────────────────────────┐
│    DATA LAYER                       │
│    SQL Server                       │
│    - Patient Data                   │
│    - Calculation History            │
│    - User Management                │
└─────────────────────────────────────┘
```

### Component Flow

1. **User Input** → Angular Form
2. **Client Validation** → TypeScript
3. **API Request** → HTTP Service
4. **Server Processing** → ASP.NET
5. **Database Operation** → SQL Server
6. **Response** → JSON
7. **Display Results** → Angular Component

---

## 5. Database Schema

### Tables (9 Total)

#### 5.1 Users
```sql
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastLogin DATETIME,
    IsActive BIT DEFAULT 1
);
```

#### 5.2 Total_Calculations
```sql
CREATE TABLE Total_Calculations (
    CalculationId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    UserId INT NOT NULL,
    CalculationDate DATETIME DEFAULT GETDATE(),
    Weight DECIMAL(5,2) NOT NULL,
    Height DECIMAL(5,2) NOT NULL,
    Age INT NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    StressFactor DECIMAL(3,2) NOT NULL,
    ActivityFactor DECIMAL(3,2) NOT NULL,
    ProteinFactor DECIMAL(3,2) NOT NULL,
    TotalCalories DECIMAL(7,2) NOT NULL,
    ProteinGrams DECIMAL(6,2) NOT NULL,
    DextroseGrams DECIMAL(6,2) NOT NULL,
    LipidGrams DECIMAL(6,2) NOT NULL,
    SodiumMeq DECIMAL(5,2),
    PotassiumMeq DECIMAL(5,2),
    CalciumMeq DECIMAL(5,2),
    MagnesiumMeq DECIMAL(5,2),
    PhosphorusMmol DECIMAL(5,2),
    TotalVolumeML DECIMAL(6,2),
    InfusionRateML DECIMAL(5,2),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```

#### 5.3 Trigerred_Data
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

#### 5.4 Dosing_Wt
```sql
CREATE TABLE Dosing_Wt (
    DosingId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    ActualWeight DECIMAL(5,2) NOT NULL,
    Height DECIMAL(5,2) NOT NULL,
    IdealBodyWeight DECIMAL(5,2),
    AdjustedBodyWeight DECIMAL(5,2),
    DosingWeight DECIMAL(5,2),
    BMI DECIMAL(4,2),
    CalculationDate DATETIME DEFAULT GETDATE()
);
```

### Stored Procedures (10 Total)

1. **sp_Preview_TPN_Inserts** - Calculate TPN without saving
2. **sp_GetAllTransactionsByDate** - Retrieve calculations by date
3. **USP_insert_Dosing_Wt** - Calculate ideal/adjusted weight
4. **GetTriggeredPatientDataByWard** - Get patient list by ward
5. **ExceptionLoggingToDataBase** - Log application errors
6. **InsertTriggeredData** - Add patient demographics
7. **InsertUser** - Create new user
8. **usp_AutoFillDataDetails** - Retrieve patient data
9. **usp_UpdateDischargeDate** - Update patient discharge
10. **usp_UpdatePatientNamesFromRemote** - Sync patient data

---

## 6. Calculation Logic

### 6.1 Energy Requirements (Calories)

**Harris-Benedict Equation:**

**Males:**
```
BEE = 66.5 + (13.75 × Weight_kg) + (5.003 × Height_cm) - (6.755 × Age)
```

**Females:**
```
BEE = 655.1 + (9.563 × Weight_kg) + (1.850 × Height_cm) - (4.676 × Age)
```

**Total Energy:**
```
TEE = BEE × Activity Factor × Stress Factor
```

**Activity Factors:**
- Bedbound: 1.2
- Light activity: 1.375
- Moderate: 1.55

**Stress Factors:**
- Normal: 1.0
- Mild stress: 1.2
- Moderate: 1.5
- Severe: 2.0

### 6.2 Protein Requirements

```
Protein (g/day) = Weight (kg) × Protein Factor
```

**Protein Factors by Condition:**
- Normal: 0.8-1.0 g/kg
- Mild stress: 1.2 g/kg
- Moderate stress: 1.5 g/kg
- Severe stress: 1.8-2.0 g/kg
- Burns (>30% BSA): 2.0-2.5 g/kg
- Renal failure: 0.6-0.8 g/kg

### 6.3 Carbohydrate (Dextrose)

```
Dextrose Calories = Total Calories × 0.60
Dextrose (g/day) = Dextrose Calories / 3.4
```

**Maximum Glucose Infusion Rate:**
```
GIR (mg/kg/min) = [Dextrose (g/day) / Weight (kg)] / 1440 × 1000
```

**Safe Limits:**
- Adults: 4-5 mg/kg/min
- Neonates: 10-12 mg/kg/min

### 6.4 Lipids (Fat Emulsion)

```
Lipid Calories = Total Calories × 0.30
Lipid (g/day) = Lipid Calories / 9
```

**Maximum Doses:**
- Adults: 2.5 g/kg/day
- Neonates: 3.0 g/kg/day

### 6.5 Electrolytes

**Standard Daily Requirements:**
- Sodium: 1-2 mEq/kg (60-150 mEq/day)
- Potassium: 1-2 mEq/kg (40-100 mEq/day)
- Calcium: 10-15 mEq/day
- Magnesium: 8-20 mEq/day
- Phosphorus: 20-40 mmol/day

---

## 7. API Documentation

### Base URLs

**Development:** `http://localhost:44399/`  
**Production:** `https://api.tpn-calculator.com/` (when deployed)

### Endpoints

#### 7.1 Calculate TPN

**Endpoint:** `POST /api/calculate`

**Request Body:**
```json
{
  "patientId": 12345,
  "weight": 70,
  "height": 175,
  "age": 35,
  "gender": "Male",
  "stressFactor": 1.5,
  "activityFactor": 1.2,
  "proteinFactor": 1.5
}
```

**Response:**
```json
{
  "calculationId": 567,
  "totalCalories": 2100,
  "proteinGrams": 105,
  "dextroseGrams": 353,
  "lipidGrams": 67,
  "electrolytes": {
    "sodium": 80,
    "potassium": 60,
    "calcium": 12,
    "magnesium": 10,
    "phosphorus": 30
  },
  "totalVolumeML": 2000,
  "infusionRateML": 83,
  "warnings": []
}
```

#### 7.2 Get Calculation History

**Endpoint:** `GET /api/history/{patientId}`

**Response:**
```json
[
  {
    "calculationId": 567,
    "calculationDate": "2025-12-30T10:30:00",
    "totalCalories": 2100,
    "calculatedBy": "Aman Mishra"
  }
]
```

---

## 8. Installation Guide

### Prerequisites

- Node.js 18+
- SQL Server 2019+
- Visual Studio 2022 or IIS Express
- Git

### Installation Steps

#### Step 1: Clone Repository
```bash
git clone https://github.com/aman7506/TPN-Calculator.git
cd TPN-Calculator
```

#### Step 2: Database Setup
```sql
-- Open SQL Server Management Studio
-- Connect to localhost
-- Open database/schema.sql
-- Execute (F5)
```

#### Step 3: Backend Configuration
```bash
cd TPN_Calculations
copy Web.config.example Web.config
# Edit Web.config - update connection string
```

**Connection String:**
```xml
<connectionStrings>
  <add name="TPNConnection" 
       connectionString="Server=localhost;Database=TPNCalculations;Integrated Security=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

#### Step 4: Frontend Setup
```bash
cd TPN-Calculator-Angular
npm install
```

#### Step 5: Start Backend
```bash
# Open in Visual Studio and press F5
# Or use IIS Express:
"C:\Program Files\IIS Express\iisexpress.exe" /path:"PATH\TPN_Calculations" /port:44399
```

#### Step 6: Start Frontend
```bash
cd TPN-Calculator-Angular
npm start
# Opens at http://localhost:4200
```

---

## 9. Deployment Guide

### Frontend Deployment (Netlify)

#### Step 1: Build Production
```bash
cd TPN-Calculator-Angular
npm run build -- --configuration production
```

#### Step 2: Deploy to Netlify
1. Go to https://www.netlify.com
2. Sign up with GitHub
3. Import TPN-Calculator repository
4. Build settings:
   - Base directory: `TPN-Calculator-Angular`
   - Build command: `npm run build -- --configuration production`
   - Publish directory: `TPN-Calculator-Angular/dist/tpn-calculator`
5. Deploy

**Result:** `https://tpn-calculator.netlify.app`

### Backend Deployment

**Option 1: Azure App Service**
- Supports .NET Framework
- Cost: ~$13/month
- Best for production

**Option 2: Migrate to .NET Core 8**
- Free hosting on Render
- Requires code refactoring
- Future-proof solution

---

## 10. Testing Procedures

### Test Case 1: Normal Adult

**Input:**
- Gender: Male
- Age: 35 years
- Weight: 70 kg
- Height: 175 cm
- Stress: Normal (1.0)

**Expected Output:**
- Calories: 2,010-2,100 kcal/day
- Protein: 70-84 g/day
- Dextrose: 300-350 g/day
- Lipids: 55-70 g/day

### Test Case 2: Burns Patient

**Input:**
- Gender: Male
- Age: 45 years
- Weight: 80 kg
- Height: 180 cm
- Stress: Severe (2.0)
- Protein: 2.0 g/kg

**Expected Output:**
- Calories: 3,800-4,200 kcal/day
- Protein: 160 g/day
- Warning: High calorie load

### Validation Tests

- ✅ Negative weight rejected
- ✅ Age > 120 rejected
- ✅ GIR > 5 mg/kg/min warning
- ✅ Lipid > 2.5 g/kg warning

---

## 11. Medical Disclaimer

**IMPORTANT NOTICE:**

This calculator is a **clinical decision support tool** only. It should **NOT** replace professional medical judgment.

### User Responsibilities:

✅ **Do:**
- Verify all calculations manually
- Adjust based on patient labs and clinical response
- Follow institutional protocols
- Consult clinical pharmacist for final prescription

❌ **Do Not:**
- Use as sole basis for clinical decisions
- Ignore abnormal lab values
- Apply without clinical context
- Use without proper medical training

### Legal Notice:

This software is provided "AS IS" without warranty. The developers assume **NO LIABILITY** for:
- Clinical outcomes
- Calculation errors
- Improper use
- Patient harm

This tool is **NOT FDA-approved** as a medical device.

### Clinical References:

- ASPEN Clinical Guidelines 2023
- ESPEN Guidelines 2020
- Harris-Benedict Equation (1918)
- Mifflin-St Jeor Equation (1990)

---

## Project Statistics

**Lines of Code:** ~50,000  
**Total Files:** 870+  
**Documentation Pages:** 14  
**Test Cases:** 5  
**Database Tables:** 9  
**Stored Procedures:** 10  
**Languages:** TypeScript 45%, C# 30%, HTML 15%, CSS 5%, SQL 3%

---

## Contact & Support

**Developer:** Aman Mishra  
**Email:** amaannn71@gmail.com  
**GitHub:** https://github.com/aman7506  
**Repository:** https://github.com/aman7506/TPN-Calculator  
**Issues:** https://github.com/aman7506/TPN-Calculator/issues

---

**Last Updated:** December 31, 2025  
**Version:** 1.0.0  
**License:** MIT  

---

*Built with dedication to improve patient safety and reduce medical errors.*
