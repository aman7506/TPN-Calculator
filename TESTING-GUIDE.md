# 🧪 TPN CALCULATOR - TESTING GUIDE

---

## 📋 TABLE OF CONTENTS

1. [Testing Overview](#testing-overview)
2. [Calculation Validation](#calculation-validation)
3. [Test Cases](#test-cases)
4. [Manual Testing](#manual-testing)
5. [Safety Validation](#safety-validation)
6. [Regression Testing](#regression-testing)

---

## 1. TESTING OVERVIEW

### **Why Testing is Critical in Healthcare:**

❗ **Patient Safety:** Incorrect calculations can cause:
- Malnutrition or overfeeding
- Electrolyte imbalances (cardiac arrhythmias)
- Hyperglycemia (sepsis risk)
- Refeeding syndrome (fatal)

✅ **Testing Ensures:**
- Mathematical accuracy
- Boundary condition handling
- Input validation
- Error handling

---

## 2. CALCULATION VALIDATION

### **2.1 Energy Calculation Test**

**Test Formula:** Harris-Benedict Equation (Male)

```
Input:
- Gender: Male
- Weight: 70 kg
- Height: 170 cm
- Age: 30 years
- Activity Factor: 1.2 (bedbound)
- Stress Factor: 1.0 (normal)

Expected Calculation:
BEE = 66.5 + (13.75 × 70) + (5.003 × 170) - (6.755 × 30)
BEE = 66.5 + 962.5 + 850.51 - 202.65
BEE = 1676.86 kcal/day

TEE = BEE × 1.2 × 1.0
TEE = 1676.86 × 1.2
TEE = 2012.23 kcal/day

✅ PASS if result = 2010-2015 kcal/day (±0.2% tolerance)
```

---

### **2.2 Protein Calculation Test**

```
Input:
- Weight: 70 kg
- Stress Level: Moderate (1.5 g/kg)

Expected Calculation:
Protein = 70 kg × 1.5 g/kg
Protein = 105 g/day

✅ PASS if result = 105 g/day
```

---

### **2.3 Dextrose Calculation Test**

```
Input:
- Total Calories: 2000 kcal/day
- Carbohydrate %: 60%

Expected Calculation:
Dextrose Calories = 2000 × 0.60 = 1200 kcal
Dextrose grams = 1200 / 3.4 = 352.94 g/day

✅ PASS if result = 350-355 g/day
```

---

### **2.4 Lipid Calculation Test**

```
Input:
- Total Calories: 2000 kcal/day
- Lipid %: 30%

Expected Calculation:
Lipid Calories = 2000 × 0.30 = 600 kcal
Lipid grams = 600 / 9 = 66.67 g/day

✅ PASS if result = 66-67 g/day
```

---

### **2.5 Glucose Infusion Rate Test**

```
Input:
- Weight: 70 kg
- Dextrose: 350 g/day

Expected Calculation:
GIR = (350 g / 70 kg) / 1440 min × 1000
GIR = 5 / 1440 × 1000
GIR = 3.47 mg/kg/min

✅ PASS if result = 3.4-3.5 mg/kg/min
✅ PASS if < 5 mg/kg/min (safe limit)
```

---

## 3. TEST CASES

### **Test Case 1: Normal Adult**

| Parameter | Value |
|-----------|-------|
| **Patient ID** | TEST001 |
| **Gender** | Male |
| **Age** | 35 years |
| **Weight** | 70 kg |
| **Height** | 175 cm |
| **Stress** | Normal (1.0) |

**Expected Results:**
- Total Calories: 2,010 - 2,100 kcal/day
- Protein: 70 - 84 g/day (1.0-1.2 g/kg)
- Dextrose: 300 - 350 g/day
- Lipids: 55 - 70 g/day
- GIR: 3.0 - 3.5 mg/kg/min

---

### **Test Case 2: Burns Patient (High Stress)**

| Parameter | Value |
|-----------|-------|
| **Patient ID** | TEST002 |
| **Gender** | Male |
| **Age** | 45 years |
| **Weight** | 80 kg |
| **Height** | 180 cm |
| **Stress** | Severe (2.0) |
| **Protein Factor** | 2.0 g/kg |

**Expected Results:**
- Total Calories: 3,800 - 4,200 kcal/day
- Protein: 160 g/day (2.0 g/kg)
- Dextrose: 600 - 700 g/day
- Lipids: 100 - 130 g/day
- **⚠️ Alert:** High calorie load - monitor glucose

---

### **Test Case 3: Elderly Female**

| Parameter | Value |
|-----------|-------|
| **Patient ID** | TEST003 |
| **Gender** | Female |
| **Age** | 75 years |
| **Weight** | 55 kg |
| **Height** | 160 cm |
| **Stress** | Mild (1.2) |

**Expected Results:**
- Total Calories: 1,400 - 1,600 kcal/day
- Protein: 55 - 66 g/day (1.0-1.2 g/kg)
- Dextrose: 240 - 280 g/day
- Lipids: 40 - 50 g/day

---

### **Test Case 4: Premature Neonate**

| Parameter | Value |
|-----------|-------|
| **Patient ID** | TEST004 |
| **Gender** | Female |
| **Age** | 0 days (newborn) |
| **Weight** | 1.5 kg |
| **Height** | 42 cm |

**Expected Results:**
- Total Calories: 120-150 kcal/kg/day = 180-225 kcal/day
- Protein: 3.0-3.5 g/kg/day = 4.5-5.25 g/day
- Lipids: <= 3 g/kg/day = <= 4.5 g/day
- GIR: < 12 mg/kg/min

---

### **Test Case 5: Renal Failure (Low Protein)**

| Parameter | Value |
|-----------|-------|
| **Patient ID** | TEST005 |
| **Gender** | Male |
| **Age** | 60 years |
| **Weight** | 75 kg |
| **Height** | 170 cm |
| **Condition** | Renal failure (non-dialysis) |
| **Protein Factor** | 0.6 g/kg |

**Expected Results:**
- Total Calories: 1,800 - 2,100 kcal/day
- Protein: **45 g/day (0.6 g/kg)** - LOW protein
- Dextrose: 350 g/day
- Lipids: 70 g/day
- **⚠️ Alert:** Low protein for renal protection

---

## 4. MANUAL TESTING

### **4.1 Input Validation Testing**

#### **Test 1: Negative Weight**
```
Input: Weight = -50 kg
Expected: ERROR "Weight must be positive"
✅ PASS / ❌ FAIL
```

#### **Test 2: Extreme Weight**
```
Input: Weight = 500 kg
Expected: WARNING "Unusual weight - verify"
✅ PASS / ❌ FAIL
```

#### **Test 3: Invalid Height**
```
Input: Height = 10 cm
Expected: ERROR "Height must be between 40-250 cm"
✅ PASS / ❌ FAIL
```

#### **Test 4: Age Over 120**
```
Input: Age = 150 years
Expected: ERROR "Invalid age"
✅ PASS / ❌ FAIL
```

---

### **4.2 Boundary Testing**

#### **Test 5: Minimum Values**
```
Input:
- Weight: 0.5 kg (smallest neonate)
- Height: 40 cm
- Age: 0 days

Expected: Calculations complete without error
✅ PASS / ❌ FAIL
```

#### **Test 6: Maximum Values**
```
Input:
- Weight: 300 kg (morbid obesity)
- Height: 250 cm
- Age: 120 years
- Stress: 2.5 (extreme)

Expected: Calculations complete, warnings shown
✅ PASS / ❌ FAIL
```

---

### **4.3 Safety Alert Testing**

#### **Test 7: High Glucose Infusion Rate**
```
Input:
- Weight: 50 kg
- Dextrose: 400 g/day

Expected Calculation:
GIR = (400/50)/1440 × 1000 = 5.56 mg/kg/min

Expected Alert: ⚠️ "GIR >5 mg/kg/min - Risk of hyperglycemia"
✅ PASS / ❌ FAIL
```

#### **Test 8: Excessive Lipids**
```
Input:
- Weight: 60 kg
- Lipids: 180 g/day = 3.0 g/kg/day

Expected Alert: ⚠️ "Lipid dose >2.5 g/kg - Risk of immune suppression"
✅ PASS / ❌ FAIL
```

#### **Test 9: Refeeding Risk**
```
Input:
- Severely malnourished patient
- Rapid calorie increase

Expected Alert: ⚠️ "REFEEDING SYNDROME RISK - Start low, advance slowly"
✅ PASS / ❌ FAIL
```

---

## 5. SAFETY VALIDATION

### **5.1 Cross-Validation with Clinical References**

| Calculation | Reference Standard | Your Result | Status |
|-------------|-------------------|-------------|--------|
| Energy (Harris-Benedict) | ASPEN Guidelines | | |
| Protein (Stress) | ESPEN ICU Guidelines | | |
| GIR Limits | Pediatric Nutrition Handbook | | |
| Electrolytes | Daily Requirements Table | | |

---

### **5.2 Medical Review Checklist**

Before clinical use, have a **clinical pharmacist or physician** verify:

- [ ] Harris-Benedict formula implemented correctly
- [ ] Stress factors match clinical guidelines
- [ ] Protein ranges appropriate for each condition
- [ ] Maximum GIR validation working
- [ ] Lipid limits enforced
- [ ] Electrolyte recommendations safe
- [ ] Refeeding syndrome warnings present
- [ ] Medical disclaimer displayed
- [ ] All units clearly labeled
- [ ] Print output is legible and complete

---

## 6. REGRESSION TESTING

### **After Any Code Change:**

Run all test cases (TEST001 - TEST005) and verify:
- [ ] All calculations within ±1% of expected
- [ ] All validations still trigger
- [ ] All safety alerts display correctly
- [ ] UI displays correctly
- [ ] Print function works
- [ ] Save to database succeeds
- [ ] No console errors

---

## 7. AUTOMATED TESTING SCRIPTS

### **Create Test Script (TypeScript)**

**File:** `src/app/testing/calculation.spec.ts`

```typescript
describe('TPN Calculations', () => {
  
  it('should calculate correct energy for TEST001', () => {
    const result = calculateEnergy({
      gender: 'Male',
      weight: 70,
      height: 175,
      age: 35,
      activityFactor: 1.2,
      stressFactor: 1.0
    });
    
    expect(result).toBeCloseTo(2012, 10); // ±10 kcal tolerance
  });
  
  it('should calculate correct protein for TEST002', () => {
    const result = calculateProtein({
      weight: 80,
      proteinFactor: 2.0
    });
    
    expect(result).toBe(160);
  });
  
  it('should alert on high GIR for TEST007', () => {
    const result = calculateGIR({
      weight: 50,
      dextrose: 400
    });
    
    expect(result.value).toBeGreaterThan(5);
    expect(result.alert).toBe(true);
    expect(result.message).toContain('hyperglycemia');
  });
  
});
```

**Run tests:**
```bash
npm test
```

---

## 8. USER ACCEPTANCE TESTING (UAT)

### **Clinical Staff Feedback Form:**

**Tester:** Dr.  _______________  
**Date:** _______________  
**Department:** _______________

**Test Patient Details:**
- [ ] Easy to enter patient data
- [ ] Results displayed clearly
- [ ] Medical units clearly labeled
- [ ] Can understand calculations
- [ ] Print output is acceptable
- [ ] Would use in clinical practice

**Rating (1-5):** ___  
**Comments:**  
_____________________________________

---

## 9. PERFORMANCE TESTING

### **Load Time:**
- [ ] Page loads in < 2 seconds
- [ ] Calculation completes in < 500ms
- [ ] Database save completes in < 1 second

### **Device Testing:**
- [ ] Works on Chrome (Desktop)
- [ ] Works on Firefox
- [ ] Works on Edge
- [ ] Works on Safari (Mac)
- [ ] Works on iPad
- [ ] Works on Android tablet
- [ ] Works on mobile (responsive)

---

## 🎯 TESTING CHECKLIST SUMMARY

Before going live:

- [ ] All 5 test cases pass (TEST001-TEST005)
- [ ] All 9 manual tests pass
- [ ] All safety alerts trigger correctly
- [ ] Cross-validated with clinical references
- [ ] Reviewed by clinical pharmacist/physician
- [ ] Regression tests pass
- [ ] UAT completed by end users
- [ ] Performance is acceptable
- [ ] Works on all target devices

**Only deploy when ALL boxes are checked!**

---

**Last Updated:** December 30, 2025  
**Version:** 1.0.0  
**Clinical Review By:** [Pending]
