# 🧮 TPN CALCULATION LOGIC - MEDICAL FORMULAS & REFERENCES

---

## 📋 TABLE OF CONTENTS

1. [Energy Requirements](#1-energy-requirements)
2. [Protein Requirements](#2-protein-requirements)
3. [Carbohydrate Requirements](#3-carbohydrate-requirements)
4. [Lipid Requirements](#4-lipid-requirements)
5. [Electrolyte Requirements](#5-electrolyte-requirements)
6. [Fluid Requirements](#6-fluid-requirements)
7. [Validation Rules](#7-validation-rules)
8. [Medical References](#8-medical-references)

---

## 1. ENERGY REQUIREMENTS

### **1.1 Harris-Benedict Equation (Revised)**

**Purpose:** Calculate Basal Energy Expenditure (BEE)

#### **For Males:**
```
BEE = 66.5 + (13.75 × Weight in kg) + (5.003 × Height in cm) - (6.755 × Age in years)
```

#### **For Females:**
```
BEE = 655.1 + (9.563 × Weight in kg) + (1.850 × Height in cm) - (4.676 × Age in years)
```

### **1.2 Total Energy Expenditure (TEE)**

```
TEE = BEE × Activity Factor × Stress Factor
```

**Activity Factors:**
- Bedbound / Sedentary: 1.2
- Light activity: 1.375
- Moderate activity: 1.55
- Active: 1.725
- Very active: 1.9

**Stress Factors:**
- Normal / Maintenance: 1.0
- Minor surgery: 1.1
- Skeletal trauma: 1.2 - 1.35
- Major surgery: 1.3 - 1.5
- Severe infection: 1.3 - 1.5
- Burns (up to 50% BSA): 1.5 - 2.0
- Burns (>50% BSA): 2.0 - 2.5

### **1.3 Alternative: Mifflin-St Jeor Equation** (More accurate for obese)

#### **For Males:**
```
BEE = (10 × Weight in kg) + (6.25 × Height in cm) - (5 × Age) + 5
```

#### **For Females:**
```
BEE = (10 × Weight in kg) + (6.25 × Height in cm) - (5 × Age) - 161
```

### **1.4 Quick Estimation Method** (ICU)

```
Total Calories = 25-30 kcal/kg/day
```

**Example:**
- Patient: 70 kg
- Calculation: 70 kg × 25 kcal/kg = 1,750 kcal/day
- Calculation: 70 kg × 30 kcal/kg = 2,100 kcal/day
- **Target Range:** 1,750 - 2,100 kcal/day

---

## 2. PROTEIN REQUIREMENTS

### **2.1 Standard Calculation**

```
Protein (g/day) = Weight (kg) × Protein Factor (g/kg)
```

### **2.2 Protein Factors by Clinical Condition**

| Condition | Protein Requirement | Notes |
|-----------|---------------------|-------|
| **Normal adult** | 0.8 - 1.0 g/kg | RDA minimum |
| **Hospitalized (stable)** | 1.0 - 1.2 g/kg | Maintenance |
| **Mild metabolic stress** | 1.2 - 1.5 g/kg | Minor surgery |
| **Moderate stress** | 1.5 - 1.8 g/kg | Major surgery, infection |
| **Severe stress** | 1.8 - 2.0 g/kg | Sepsis, trauma |
| **Burns (30-50% BSA)** | 2.0 - 2.5 g/kg | High catabolism |
| **Burns (>50% BSA)** | 2.5 - 3.0 g/kg | Extreme catabolism |
| **Renal failure (non-dialysis)** | 0.6 - 0.8 g/kg | Minimize urea generation |
| **Dialysis patients** | 1.2 - 1.5 g/kg | Compensate protein losses |
| **Hepatic encephalopathy** | 0.6 - 1.0 g/kg | Use BCAA formula |
| **Elderly (>65 years)** | 1.0 - 1.2 g/kg | Prevent sarcopenia |
| **Critical illness (ICU)** | 1.2 - 2.0 g/kg | Individualized |

### **2.3 Nitrogen Balance Calculation**

```
Nitrogen Intake (g/day) = Protein (g/day) / 6.25

Nitrogen Balance = Nitrogen Intake - Nitrogen Losses

Nitrogen Losses = (24-hour UUN + 4) g/day
```

**Where:**
- UUN = Urinary Urea Nitrogen (measured from 24-hour urine collection)
- +4 accounts for insensible losses

**Goals:**
- Positive nitrogen balance: Anabolic state (healing, growth)
- Zero balance: Maintenance
- Negative balance: Catabolic state (stress, malnutrition)

---

## 3. CARBOHYDRATE (DEXTROSE) REQUIREMENTS

### **3.1 Macronutrient Distribution**

```
Carbohydrate Calories = 50-70% of Total Calories
```

**Typical Distribution:**
- Carbohydrate: 50-60%
- Protein: 15-20%
- Fat: 25-35%

### **3.2 Dextrose Calculation**

```
Dextrose Calories = Total Calories × 0.60 (60%)
Dextrose (g/day) = Dextrose Calories / 3.4 kcal/g
```

**Note:** IV dextrose provides **3.4 kcal/gram** (not 4 kcal/g like oral carbs)

**Example:**
- Total Calories: 2,000 kcal/day
- Dextrose Calories: 2,000 × 0.60 = 1,200 kcal
- Dextrose Amount: 1,200 / 3.4 = **353 grams/day**

### **3.3 Maximum Glucose Infusion Rate (GIR)**

```
GIR (mg/kg/min) = [Dextrose (g/day) / Weight (kg)] / 1440 min/day × 1000 mg/g
```

**Safe Limits:**
- **Adults:** 4-5 mg/kg/min (maximum 7 mg/kg/min)
- **Neonates:** 10-12 mg/kg/min
- **Critically ill:** 3-4 mg/kg/min (prevent hyperglycemia)

**Example:**
- Patient: 70 kg
- Dextrose: 350 g/day
- GIR = (350 / 70) / 1440 × 1000 = **3.47 mg/kg/min** ✅ Safe

### **3.4 Dextrose Concentrations in TPN**

| Dextrose % | g/100ml | kcal/100ml | Use |
|------------|---------|------------|-----|
| D10% | 10 g | 34 kcal | Peripheral |
| D20% | 20 g | 68 kcal | Central |
| D50% | 50 g | 170 kcal | Central (high conc) |
| D70% | 70 g | 238 kcal | Stock solution |

---

## 4. LIPID (FAT EMULSION) REQUIREMENTS

### **4.1 Lipid Calculation**

```
Lipid Calories = 25-30% of Total Calories
Lipid (g/day) = Lipid Calories / 9 kcal/g
```

**Example:**
- Total Calories: 2,000 kcal/day
- Lipid Calories: 2,000 × 0.30 = 600 kcal
- Lipid Amount: 600 / 9 = **67 grams/day**

### **4.2 Maximum Lipid Dosing**

| Patient Group | Maximum Dose | Notes |
|---------------|--------------|-------|
| **Adults** | 2.5 g/kg/day | ASPEN guideline |
| **Neonates / Infants** | 3.0 - 4.0 g/kg/day | Higher fat needs |
| **Critically ill** | 1.0 - 1.5 g/kg/day | Risk of immune suppression |
| **Liver disease** | 1.0 g/kg/day | Reduce fat load |

### **4.3 Lipid Emulsion Types**

| Type | Composition | Advantages |
|------|-------------|------------|
| **Intralipid (20%)** | 100% soybean oil | Standard, well-tolerated |
| **Clinoleic** | Soy + olive oil | Lower Omega-6 |
| **SMOFlipid** | Soy + MCT + olive + fish | Anti-inflammatory |
| **Omegaven** | 100% fish oil | Prevents liver disease (PNALD) |

### **4.4 Minimum Lipid for Essential Fatty Acids**

```
Minimum = 2-4% of Total Calories from linoleic acid
Practical Minimum = 0.5 g/kg/day
```

**Purpose:** Prevent essential fatty acid deficiency (EFAD)

---

## 5. ELECTROLYTE REQUIREMENTS

### **5.1 Standard Adult Daily Requirements**

| Electrolyte | Daily Requirement | Maximum | Monitoring |
|-------------|-------------------|---------|------------|
| **Sodium** | 1-2 mEq/kg (60-150 mEq) | 150 mEq/day | Serum Na+ |
| **Potassium** | 1-2 mEq/kg (40-100 mEq) | 120 mEq/day | Serum K+, ECG |
| **Chloride** | 1-2 mEq/kg (60-150 mEq) | 150 mEq/day | Serum Cl- |
| **Calcium** | 10-15 mEq/day | 20 mEq/day | Serum Ca2+ |
| **Magnesium** | 8-20 mEq/day | 24 mEq/day | Serum Mg2+ |
| **Phosphorus** | 20-40 mmol/day | 45 mmol/day | Serum PO4 |

### **5.2 Electrolyte Adjustments**

#### **Sodium:**
```
Deficit (mEq) = (140 - Serum Na+) × Body Weight (kg) × 0.6
```

**Correction:**
- Increase by max 10-12 mEq/L per 24 hours (prevent osmotic demyelination)
- Add to TPN or give separate IV bolus

#### **Potassium:**
```
Deficit (mEq) = (4.0 - Serum K+) × Body Weight (kg) × 0.4
```

**⚠️ Critical:**
- K+ < 2.5 mEq/L: Cardiac arrhythmias (emergency)
- Maximum infusion rate: 10 mEq/hour via central line

#### **Phosphorus:**
```
Deficit (mmol) = (Normal - Serum PO4) × Body Weight (kg) × 0.5
```

**Refeeding Syndrome Risk:**
- Add phosphorus BEFORE high dextrose (prevent hypophosphatemia)
- Monitor daily in malnourished patients

---

## 6. FLUID REQUIREMENTS

### **6.1 Holliday-Segar Method** (Pediatrics)

| Weight | Fluid Requirement |
|--------|-------------------|
| **0-10 kg** | 100 ml/kg/day |
| **11-20 kg** | 1000 ml + 50 ml/kg for each kg >10 |
| **>20 kg** | 1500 ml + 20 ml/kg for each kg >20 |

**Example (25 kg child):**
```
= 1500 ml + (5 kg × 20 ml/kg)
= 1500 + 100
= 1600 ml/day
```

### **6.2 Adult Maintenance Fluids**

```
Standard: 30-35 ml/kg/day
```

**Example (70 kg adult):**
```
= 70 kg × 30 ml/kg = 2,100 ml/day
= 70 kg × 35 ml/kg = 2,450 ml/day
Range: 2,100 - 2,450 ml/day
```

### **6.3 Adjustments**

**Increase fluids for:**
- Fever (+12% per °C >37°C)
- Burns
- Diarrhea / drains
- High-output ostomy

**Decrease fluids for:**
- Heart failure
- Renal failure (oliguria)
- SIADH
- Edema

---

## 7. VALIDATION RULES

### **7.1 Input Validation**

**Weight:**
- Minimum: 0.5 kg (premature neonate)
- Maximum: 300 kg
- Decimal precision: 1 place (e.g., 65.5 kg)

**Height:**
- Minimum: 40 cm (premature neonate)
- Maximum: 250 cm
- Decimal precision: 1 place

**Age:**
- Minimum: 0 (newborn, use days or months)
- Maximum: 120 years

**Stress Factor:**
- Minimum: 1.0 (normal)
- Maximum: 2.5 (severe burns)
- Step: 0.1

### **7.2 Output Validation (Safety Checks)**

**Total Calories:**
- Minimum: 400 kcal/day (pediatric)
- Maximum: 5,000 kcal/day (severe burns)
- Alert if >40 kcal/kg (risk of overfeeding)

**Protein:**
- Alert if >2.5 g/kg (kidney stress)
- Alert if <0.6 g/kg (inadequate)

**Glucose Infusion Rate:**
- Alert if >5 mg/kg/min (hyperglycemia risk)
- Alert if <2 mg/kg/min (inadequate energy)

**Lipids:**
- Alert if >2.5 g/kg (immune suppression)
- Alert if <0.5 g/kg (EFAD risk)

---

## 8. MEDICAL REFERENCES

### **8.1 Clinical Guidelines**

1. **ASPEN Clinical Guidelines (2021)**
   - American Society for Parenteral and Enteral Nutrition
   - Gold standard for TPN prescription
   - URL: https://www.nutritioncare.org/Guidelines_and_Clinical_Resources/

2. **ESPEN Guidelines (2020)**
   - European Society for Clinical Nutrition and Metabolism
   - Adult and pediatric TPN protocols

3. **FDA Guidance on Parenteral Nutrition**
   - Safety and efficacy standards

### **8.2 Key Research Papers**

1. **Harris, J.A., & Benedict, F.G. (1918)**
   - "A Biometric Study of Human Basal Metabolism"
   - Carnegie Institute Publication 279

2. **Mifflin, M.D., et al. (1990)**
   - "A new predictive equation for resting energy expenditure"
   - American Journal of Clinical Nutrition

3. **Singer, P., et al. (2019)**
   - "ESPEN guideline on clinical nutrition in the intensive care unit"
   - Clinical Nutrition

### **8.3 Textbooks**

1. **"The ASPEN Adult Nutrition Support Core Curriculum" (3rd Ed)**
2. **"Parenteral Nutrition" by Pichard & Jeejeebhoy**

---

## 📞 DISCLAIMER

⚠️ **This calculator is for clinical decision support only.**

✅ **Always:**
- Verify calculations manually
- Adjust based on patient labs
- Monitor clinical response
- Follow your institution's protocols

❌ **Never:**
- Use as sole decision-making tool
- Ignore abnormal lab values
- Apply without clinical context

**Consult a physician or clinical pharmacist for final TPN prescription.**

---

**Last Updated:** December 30, 2025  
**Version:** 1.0.0  
**Reviewed By:** Clinical Nutrition Team
