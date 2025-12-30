# 📚 TPN CALCULATOR - COMPLETE PROJECT DOCUMENTATION

---

## 📋 TABLE OF CONTENTS

1. [Introduction](#introduction)
2. [What is TPN?](#what-is-tpn)
3. [Problem Statement](#problem-statement)
4. [Objectives](#objectives)
5. [System Architecture](#system-architecture)
6. [Technology Stack](#technology-stack)
7. [Application Features](#application-features)
8. [User Workflows](#user-workflows)
9. [Security & Privacy](#security-privacy)
10. [Limitations](#limitations)
11. [Future Enhancements](#future-enhancements)

---

## 1. INTRODUCTION

### **Project Name:** TPN Calculator

### **Purpose:**
A web-based healthcare application designed to calculate Total Parenteral Nutrition (TPN) requirements for patients who cannot receive nutrition through oral or enteral routes.

### **Target Users:**
- 👨‍⚕️ Doctors (Intensivists, Pediatricians)
- 💊 Clinical Pharmacists
- 👩‍⚕️ Registered Nurses (ICU, Neonatal)
- 🏥 Nutrition Support Teams

### **Version:** 1.0.0
### **Last Updated:** December 30, 2025

---

## 2. WHAT IS TPN?

### **Definition:**
Total Parenteral Nutrition (TPN) is intravenous feeding that provides complete nutrition directly into the bloodstream, bypassing the digestive system.

### **When is TPN Used?**
- 🔴 **Critical illness** (severe burns, trauma)
- 🔴 **Gastrointestinal disorders** (Crohn's disease, bowel obstruction)
- 🔴 **Post-surgical recovery** (major abdominal surgery)
- 🔴 **Premature infants** (underdeveloped digestive systems)
- 🔴 **Cancer treatment** (severe malnutrition)

### **Why Calculations Matter:**
Incorrect TPN calculations can lead to:
- ⚠️ Malnutrition or overfeeding
- ⚠️ Electrolyte imbalances (life-threatening)
- ⚠️ Hyperglycemia or hypoglycemia
- ⚠️ Liver dysfunction
- ⚠️ Refeeding syndrome

**👉 Accuracy is CRITICAL in healthcare.**

---

## 3. PROBLEM STATEMENT

### **Before This Application:**

#### ❌ **Manual Calculations:**
- Prone to human error (decimal mistakes, unit conversions)
- Time-consuming (15-30 minutes per patient)
- No standardization across different wards

#### ❌ **Excel Spreadsheets:**
- Formula errors go unnoticed
- No validation of input ranges
- Lost files, version conflicts
- No audit trail

#### ❌ **Paper Charts:**
- Illegible handwriting
- Calculation errors common
- No backup copies
- Time wasted searching for records

### **Impact:**
- Delayed patient care
- Increased risk of medication errors
- Reduced efficiency for clinical staff

---

## 4. OBJECTIVES

### **Primary Goals:**

✅ **Accuracy:** Eliminate calculation errors through automated, validated formulas

✅ **Speed:** Reduce calculation time from 20 minutes to < 2 minutes

✅ **Standardization:** Ensure consistent TPN prescriptions across all departments

✅ **Safety:** Implement validation rules to prevent dangerous dosing

✅ **Accessibility:** Web-based, accessible from any device in the hospital

✅ **Record-Keeping:** Store calculation history for audit and review

### **Secondary Goals:**

✅ Easy-to-use interface (minimal training required)  
✅ Print-ready output for medical charts  
✅ Compatible with hospital IT infrastructure  
✅ Compliant with healthcare data privacy regulations  

---

## 5. SYSTEM ARCHITECTURE

### **High-Level Architecture:**

```
┌───────────────────────────────────────────────────┐
│                  USER LAYER                       │
│  (Doctors, Pharmacists, Nurses)                   │
│  Devices: Desktop, Tablets, Mobile                │
└────────────────┬──────────────────────────────────┘
                 │
                 │ HTTPS
                 ▼
┌───────────────────────────────────────────────────┐
│           PRESENTATION LAYER                      │
│  Framework: Angular 15                            │
│  - Reactive Forms for input                       │
│  - Material UI components                         │
│  - Client-side validation                         │
│  - Responsive design (Bootstrap 5)                │
└────────────────┬──────────────────────────────────┘
                 │
                 │ HTTP/AJAX
                 ▼
┌───────────────────────────────────────────────────┐
│          APPLICATION LAYER                        │
│  Framework: ASP.NET Web Forms                     │
│  - Business logic (calculation algorithms)        │
│  - Input validation                               │
│  - Exception handling                             │
│  - Data formatting                                │
└────────────────┬──────────────────────────────────┘
                 │
                 │ ADO.NET
                 ▼
┌───────────────────────────────────────────────────┐
│            DATA LAYER                             │
│  Database: SQL Server                             │
│  - Patient data storage                           │
│  - Calculation history                            │
│  - User authentication                            │
│  - Audit logs                                     │
└───────────────────────────────────────────────────┘
```

### **Component Details:**

#### **Frontend (Angular):**
- **Location:** `TPN-Calculator-Angular/src/app/`
- **Key Files:**
  - `tpn-form.component.ts` - Main calculation form
  - `tpn-form.component.html` - User interface
  - `tpn.service.ts` - API communication
  - `calculation.model.ts` - Data models

#### **Backend (ASP.NET):**
- **Location:** `TPN_Calculations/`
- **Key Files:**
  - `TPNMAIN.aspx.cs` - Core calculation logic
  - `Web.config` - Configuration, connection strings
  - `Utility.cs` - Helper functions

#### **Database (SQL Server):**
- **Tables:** 9 tables (see Database Schema section)
- **Stored Procedures:** 10 procedures
- **Backup:** `database/schema.sql`

---

## 6. TECHNOLOGY STACK

### **Frontend Technologies:**

| Technology | Version | Purpose |
|------------|---------|---------|
| Angular | 15.2.10 | SPA framework |
| TypeScript | 4.8.4 | Type-safe JavaScript |
| Angular Material | 15.2.9 | UI components |
| Bootstrap | 5.3.3 | Responsive layout |
| RxJS | 7.8.0 | Reactive programming |

### **Backend Technologies:**

| Technology | Version | Purpose |
|------------|---------|---------|
| ASP.NET Web Forms | .NET Framework 4.x | Server-side logic |
| C# | 7.3+ | Programming language |
| ADO.NET | - | Database access |
| IIS Express | 10.0 | Development server |

### **Database:**

| Technology | Version | Purpose |
|------------|---------|---------|
| SQL Server | 2019+ | Relational database |
| T-SQL | - | Query language |

### **Development Tools:**

- **IDE:** Visual Studio 2022 (Backend), VS Code (Frontend)
- **Version Control:** Git, GitHub
- **Package Managers:** npm (Frontend), NuGet (Backend)
- **Database Tool:** SQL Server Management Studio (SSMS)

---

## 7. APPLICATION FEATURES

### **7.1 Patient Information Entry**

**Inputs:**
- 👤 Patient Name
- 🆔 Hospital ID / MRN
- 📅 Date of Birth / Age
- ⚖️ Current Weight (kg)
- 📏 Height (cm)
- 🚹 Gender
- 🏥 Ward / Department
- 📋 Diagnosis

**Validation:**
- Weight: 0.5 kg - 300 kg
- Height: 40 cm - 250 cm
- Age: 0 - 120 years

---

### **7.2 Clinical Parameters**

**Inputs:**
- 🌡️ Stress Factor (1.0 - 2.0)
  - Normal: 1.0
  - Mild stress: 1.2
  - Moderate stress: 1.5
  - Severe stress: 1.8-2.0

- 🎯 Goal (Maintenance / Repletion / Weight Loss)

- 💉 Fluid Requirement (ml/day)

- 🩺 Special Conditions:
  - Renal impairment (adjust protein)
  - Hepatic impairment (adjust amino acids)
  - Diabetes (adjust dextrose)

---

### **7.3 Automated Calculations**

#### **Energy Requirements (Calories):**

**Formula:** Harris-Benedict Equation

**For Males:**
```
BEE = 66.5 + (13.75 × Weight_kg) + (5.003 × Height_cm) - (6.755 × Age)
Total Calories = BEE × Activity Factor × Stress Factor
```

**For Females:**
```
BEE = 655.1 + (9.563 × Weight_kg) + (1.850 × Height_cm) - (4.676 × Age)
Total Calories = BEE × Activity Factor × Stress Factor
```

**Activity Factor:**
- Bedbound: 1.2
- Ambulatory: 1.3

---

#### **Protein Requirements:**

**Formula:**
```
Protein (g/day) = Weight (kg) × Protein Factor

Protein Factor by Condition:
- Normal: 0.8 - 1.0 g/kg
- Mild stress: 1.2 g/kg
- Moderate stress: 1.5 g/kg
- Severe stress / Burns: 1.8 - 2.5 g/kg
- Renal failure (non-dialysis): 0.6 g/kg
```

---

#### **Carbohydrate (Dextrose) Requirements:**

**Formula:**
```
Dextrose Calories = 50-60% of Total Calories
Dextrose (g/day) = Dextrose Calories / 3.4

(Note: Dextrose provides 3.4 kcal/g in IV form)

Maximum Rate: 4-5 mg/kg/min (prevent hyperglycemia)
```

---

#### **Lipid (Fat Emulsion) Requirements:**

**Formula:**
```
Lipid Calories = 25-30% of Total Calories
Lipid (g/day) = Lipid Calories / 9

(Note: Lipids provide 9 kcal/g)

Maximum: 2.5 g/kg/day (adults), 3 g/kg/day (neonates)
```

---

#### **Electrolytes:**

**Standard Daily Requirements:**
- Sodium: 1-2 mEq/kg (60-150 mEq/day)
- Potassium: 1-2 mEq/kg (40-100 mEq/day)
- Calcium: 10-15 mEq/day
- Magnesium: 8-20 mEq/day
- Phosphorus: 20-40 mmol/day

*(Adjusted based on patient labs and condition)*

---

### **7.4 Output Display**

**Calculation Results:**
```
╔══════════════════════════════════════════════╗
║     TPN PRESCRIPTION SUMMARY                 ║
╠══════════════════════════════════════════════╣
║ Patient: John Doe (ID: 12345)                ║
║ Date: 2025-12-30                             ║
╠══════════════════════════════════════════════╣
║ TOTAL CALORIES:         2,100 kcal/day       ║
║ PROTEIN:                105 g/day (20%)      ║
║ DEXTROSE:               315 g/day (60%)      ║
║ LIPIDS:                 58 g/day (25%)       ║
╠══════════════════════════════════════════════╣
║ ELECTROLYTES:                                ║
║ - Sodium:               80 mEq/day           ║
║ - Potassium:            60 mEq/day           ║
║ - Calcium:              12 mEq/day           ║
║ - Magnesium:            10 mEq/day           ║
╠══════════════════════════════════════════════╣
║ TOTAL VOLUME:           2,000 ml/day         ║
║ INFUSION RATE:          83 ml/hr             ║
╚══════════════════════════════════════════════╝
```

**Actions:**
- 🖨️ Print prescription
- 💾 Save to database
- 📧 Email to pharmacy
- 📋 Copy to clipboard

See `CALCULATION-LOGIC.md` for detailed formulas and medical references.

---

## 8. USER WORKFLOWS

### **Workflow 1: New Patient Calculation**

```
1. User logs in
   ↓
2. Clicks "New Calculation"
   ↓
3. Enters patient demographics
   ↓
4. Enters clinical parameters
   ↓
5. Clicks "Calculate"
   ↓
6. System validates inputs
   ↓
7. System performs calculations
   ↓
8. Results displayed
   ↓
9. User reviews results
   ↓
10. User saves to database
    ↓
11. Confirmation message shown
```

### **Workflow 2: Modify Existing Prescription**

```
1. Search patient by ID
   ↓
2. Select recent calculation
   ↓
3. Edit parameters
   ↓
4. Recalculate
   ↓
5. Compare old vs new
   ↓
6. Save if approved
```

---

## 9. SECURITY & PRIVACY

### **Authentication:**
- ✅ User login required
- ✅ Role-based access (Doctor/Pharmacist/Nurse)
- ✅ Session timeout (15 minutes inactivity)

### **Data Privacy:**
- ✅ No PHI (Protected Health Information) stored in cookies
- ✅ HTTPS encryption in production
- ✅ Database connection encrypted

### **Medical Disclaimer:**
```
⚠️ MEDICAL DISCLAIMER

This calculator is a clinical decision support tool.
It should NOT replace clinical judgment.

- Verify all calculations manually
- Adjust based on patient response
- Monitor labs closely
- Use in conjunction with clinical guidelines

The developers assume no liability for clinical outcomes.
```

---

## 10. LIMITATIONS

### **Current Limitations:**

❌ **No Mobile App** (web-only)  
❌ **No Offline Mode** (requires internet)  
❌ **No Integration** with EMR/HIS systems  
❌ **No Real-Time Lab Integration**  
❌ **Single Language** (English only)  
❌ **No Pediatric-Specific Formulas** (uses scaled adult formulas)  

---

## 11. FUTURE ENHANCEMENTS

### **Phase 2 (Q2 2026):**
- ✨ Mobile app (iOS/Android)
- ✨ Offline calculation mode
- ✨ Barcode scanning for patient ID
- ✨ Multi-language support

### **Phase 3 (Q3 2026):**
- ✨ HL7/FHIR integration with EMR
- ✨ Automatic lab value import
- ✨ AI-based dosing recommendations
- ✨ Pediatric calculation modules

### **Phase 4 (2027):**
- ✨ FDA approval for clinical use
- ✨ HIPAA compliance certification
- ✨ Cloud-based hospital deployment
- ✨ Real-time monitoring dashboards

---

## 📞 SUPPORT & CONTACT

**For Technical Issues:**
- GitHub Issues: `https://github.com/YOUR_USERNAME/TPN-Calculator/issues`

**For Clinical Questions:**
- Consult your hospital's nutrition support team
- Reference: ASPEN Clinical Guidelines

---

**📚 Related Documentation:**
- `CALCULATION-LOGIC.md` - Detailed formulas and medical references
- `API-DOCUMENTATION.md` - Backend API endpoints
- `USER-GUIDE.md` - End-user manual
- `DEPLOYMENT-GUIDE.md` - Production deployment steps

---

**Last Updated:** December 30, 2025  
**Version:** 1.0.0  
**Maintained By:** Development Team
