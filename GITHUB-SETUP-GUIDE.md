# 📦 PART 1: GITHUB SETUP GUIDE
## Complete Guide to Upload TPN Calculator to GitHub

---

## 🎯 WHY WE NEED GIT & GITHUB FOR HEALTHCARE PROJECTS

### **Git = Version Control**
- Track every change in your medical calculation logic
- Rollback if a formula is accidentally changed
- Collaborate with healthcare professionals safely
- Maintain audit trail (important for healthcare compliance)

### **GitHub = Cloud Storage + Collaboration**
- Backup your code (prevent data loss)
- Share with reviewers (doctors, pharmacists, regulatory teams)
- Professional portfolio for healthcare IT projects
- Enable deployment automation

---

## ✅ STEP 1: INSTALL GIT

### 1️⃣ Download Git for Windows
```
https://git-scm.com/download/win
```

### 2️⃣ Install with these settings:
- ✅ Use Git from the Windows Command Prompt
- ✅ Checkout Windows-style, commit Unix-style line endings
- ✅ Use MinTTY terminal
- ✅ Enable Git Credential Manager

### 3️⃣ Verify Installation
Open **PowerShell** or **Command Prompt**:
```bash
git --version
```
**Expected Output:**
```
git version 2.x.x
```

---

## ✅ STEP 2: CONFIGURE GIT

### Set Your Identity (Required)
```bash
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

**Example:**
```bash
git config --global user.name "Dr. Aman Singh"
git config --global user.email "aman@hospital.com"
```

### Verify Configuration
```bash
git config --list
```

**WHY?** Every code change (commit) is signed with your name and email. This creates accountability in medical software.

---

## ✅ STEP 3: CREATE PERFECT `.gitignore`

### **What is `.gitignore`?**
A file that tells Git which files to IGNORE (not upload to GitHub).

### **Why Important for Healthcare Projects?**
- ❌ **Don't upload** database passwords
- ❌ **Don't upload** patient data
- ❌ **Don't upload** compiled files (too large, not needed)
- ❌ **Don't upload** temporary files
- ✅ **Only upload** source code

### Create `.gitignore` File

**Navigate to your project:**
```bash
cd "e:\Aman Project Files\TPN_Calculations"
```

**Create the file:**
```bash
New-Item -Path ".\.gitignore" -ItemType File
```

**Copy the content below** into `.gitignore` file:

```gitignore
##########################
# .NET / ASP.NET BACKEND
##########################

# Build Results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Ww][Ii][Nn]32/
[Aa][Rr][Mm]/
[Aa][Rr][Mm]64/
bld/
[Bb]in/
[Oo]bj/
[Ll]og/
[Ll]ogs/

# Visual Studio Files
.vs/
*.suo
*.user
*.userosscache
*.sln.docstates
*.userprefs

# Resharper
_ReSharper*/
*.[Rr]e[Ss]harper
*.DotSettings.user

# NuGet Packages
*.nupkg
*.snupkg
**/packages/*
!**/packages/build/
*.nuget.props
*.nuget.targets

# Web Publish Profiles
PublishProfiles/
*.pubxml
*.publishproj

##########################
# ANGULAR FRONTEND
##########################

# Node Modules (HUGE - Don't Upload!)
node_modules/
npm-debug.log*
yarn-debug.log*
yarn-error.log*

# Angular Build Output
dist/
tmp/
out-tsc/

# Angular Cache
.angular/
*.angular/
cache/

# IDE Files
.vscode/
.idea/
*.swp
*.swo
*~

##########################
# DATABASE FILES
##########################

# SQL Server Database Files
*.mdf
*.ldf
*.ndf

# Database Backups (TOO LARGE)
*.bak
*.sql.bak
tpnbackup

# IMPORTANT: Keep schema/migration scripts
# We'll create a separate lightweight schema.sql

##########################
# SENSITIVE FILES
##########################

# Configuration with Secrets
Web.config
appsettings.json
appsettings.*.json
*.env
*.env.*

# Connection Strings
connectionStrings.config

# KEEP TEMPLATE FILES:
!Web.config.example
!appsettings.example.json

##########################
# SYSTEM FILES
##########################

# Windows
Thumbs.db
Desktop.ini
$RECYCLE.BIN/
*.cab
*.msi
*.msix
*.msm
*.msp

# macOS
.DS_Store
.AppleDouble
.LSOverride

##########################
# LOGS & TEMPORARY FILES
##########################

*.log
*.tmp
*.temp
*.txt.bak
*-log.txt
database-restore-log.txt
setup-output.txt

##########################
# BUILD SCRIPTS (Personal)
##########################

# Your personal batch files
START-ALL.bat
STOP-ALL.bat
1-CreateBackup.bat
2-RestoreLocal.bat
3-UpdateWebConfig.bat
AUTO-RESTORE-DATABASE.bat
Start-IISExpress.bat
Stop-IISExpress.bat
STATUS-AND-NEXT-STEPS.bat

# PowerShell Scripts (Local Setup Only)
Backup-Database.ps1
Extract-DatabaseSchema.ps1
Generate-CompleteDatabase.ps1

##########################
# SETUP GUIDES (Personal)
##########################

COMPLETE-SETUP-GUIDE.txt
DATABASE-SETUP-GUIDE.md
FINAL-DATABASE-RESTORE.txt
RESTORE-DATABASE-GUIDE.txt

# List files
procedures-list.txt
remote-procedures-list.txt
remote-tables-list.txt
tables.txt
test-procedure.txt
```

**Save the file.**

---

## ✅ STEP 4: INITIALIZE GIT REPOSITORY

### Navigate to Project
```bash
cd "e:\Aman Project Files\TPN_Calculations"
```

### Initialize Git
```bash
git init
```

**What Happens?**
- Creates hidden `.git` folder
- Your project is now a Git repository
- Git starts tracking changes

**Output:**
```
Initialized empty Git repository in e:/Aman Project Files/TPN_Calculations/.git/
```

---

## ✅ STEP 5: CREATE CONFIGURATION TEMPLATE FILES

### **WHY?**
We don't upload `Web.config` (contains passwords), but we need to show developers what to configure.

### Create `Web.config.example`

1. **Copy your Web.config:**
```bash
Copy-Item ".\TPN_Calculations\Web.config" ".\TPN_Calculations\Web.config.example"
```

2. **Edit** `Web.config.example`:

**Find:**
```xml
<connectionStrings>
  <add name="TPNConnection" 
       connectionString="Server=localhost;Initial Catalog=TPNCalculations;User ID=sa;Password=sql123;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Replace with:**
```xml
<connectionStrings>
  <add name="TPNConnection" 
       connectionString="Server=YOUR_SERVER;Initial Catalog=TPNCalculations;User ID=YOUR_USER;Password=YOUR_PASSWORD;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Save** as `Web.config.example`.

### Create `environments/environment.ts.example` (Angular)

```bash
cd "TPN-Calculator-Angular\src\environments"
Copy-Item "environment.ts" "environment.ts.example"
```

**Edit** and replace API URLs:
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:44399/' // Replace with your backend URL
};
```

---

## ✅ STEP 6: CREATE DATABASE SCHEMA FILE (Lightweight)

### **Problem:** 
Your `TPNDatabase-Backup.sql` is 1.6 MB (TOO BIG for GitHub).

### **Solution:** 
Create lightweight schema-only file.

### Using SQL Server Management Studio (SSMS):

1. Connect to your **localhost** SQL Server
2. Right-click `TPNCalculations` database → **Tasks** → **Generate Scripts**
3. Click **Next**
4. Select **Check "Specific database objects"**
5. Check:
   - ✅ Tables
   - ✅ Stored Procedures
6. Click **Next**
7. Click **Advanced** button
8. Find **"Types of data to script"** → Select **"Schema only"** (NOT "Schema and data")
9. Find **"Script DROP and CREATE"** → Select **"Script CREATE"**
10. Click **OK**
11. Choose **"Save to file"** → Browse to: `e:\Aman Project Files\TPN_Calculations\database\schema.sql`
12. Click **Finish**

### Create Sample Data File (Optional)

**Create:** `database\sample-data.sql`

```sql
-- Sample Users for Testing (NOT REAL PATIENT DATA)
USE TPNCalculations;
GO

INSERT INTO Users (Username, Password, Role, CreatedDate) 
VALUES 
('demo_doctor', 'demo123', 'Doctor', GETDATE()),
('demo_pharmacist', 'demo123', 'Pharmacist', GETDATE());
GO
```

---

## ✅ STEP 7: STAGE FILES FOR COMMIT

### **What is Staging?**
Telling Git which files to include in the next commit.

### Add All Files
```bash
cd "e:\Aman Project Files\TPN_Calculations"
git add .
```

**WHY `.` (dot)?** 
It means "add all files that are not in `.gitignore`".

### Check Status
```bash
git status
```

**You should see:**
```
Changes to be committed:
  (use "git rm --cached <file>..." to unstage)
        new file:   .gitignore
        new file:   TPN-Calculator-Angular/src/app/...
        new file:   TPN_Calculations/...
        new file:   database/schema.sql
```

**✅ Verify:** NO `node_modules/`, NO `bin/`, NO `obj/`, NO `Web.config`

---

## ✅ STEP 8: MAKE FIRST COMMIT

### **What is a Commit?**
A snapshot of your code at this moment with a description.

### Commit Command
```bash
git commit -m "Initial commit: TPN Calculator v1.0 - Angular frontend + ASP.NET backend"
```

**Output:**
```
[master (root-commit) abc123] Initial commit: TPN Calculator v1.0
 150 files changed, 25000 insertions(+)
```

---

## ✅ STEP 9: COMMIT MESSAGE STANDARDS (Healthcare Best Practices)

### **Why Important?**
- Medical calculations must be traceable
- Changes to formulas must be documented
- Regulatory compliance requires clear history

### **Format:**
```
<type>: <short description>

<detailed explanation (optional)>
<medical/regulatory context (if applicable)>
```

### **Types:**
- `feat:` - New feature
- `fix:` - Bug fix (CRITICAL for medical apps)
- `calc:` - Calculation logic change (ALWAYS explain)
- `docs:` - Documentation only
- `test:` - Adding tests
- `refactor:` - Code restructuring (same functionality)
- `security:` - Security-related changes

### **Examples for TPN Project:**

#### ✅ Good Examples:

```bash
git commit -m "calc: Update protein requirement formula to 1.5g/kg for ICU patients

Previous: 1.2g/kg (general ward)
New: 1.5g/kg (ICU patients as per ASPEN guidelines 2023)
Reference: ASPEN Clinical Guidelines - Critical Care Nutrition
Reviewed by: Dr. Smith (Clinical Pharmacist)
"
```

```bash
git commit -m "fix: Correct lipid emulsion calculation for neonates

Issue: Calculation was using adult max (2.5g/kg) instead of neonatal max (3g/kg)
Impact: Underdosing in neonatal ICU
Fix: Added patient age check in calculateLipids() function
Tested: 50 sample neonatal cases
"
```

```bash
git commit -m "feat: Add electrolyte validation warnings

- Sodium: 130-145 mEq/L
- Potassium: 3.5-5.0 mEq/L
- Magnesium: 1.5-2.5 mg/dL
Displays yellow warning if outside normal range
"
```

```bash
git commit -m "security: Sanitize patient name input to prevent SQL injection

Added input validation in TPNMAIN.aspx.cs (line 245)
Parameterized all SQL queries
"
```

#### ❌ Bad Examples:

```bash
git commit -m "fixed bug"  # WHAT BUG? WHERE?
git commit -m "updated calculations"  # WHICH CALCULATION? WHY?
git commit -m "changes"  # USELESS
```

---

## ✅ STEP 10: CREATE GITHUB REPOSITORY

### 1️⃣ Go to GitHub
```
https://github.com
```

### 2️⃣ Create Account (if needed)
- Sign up with professional email
- Verify email

### 3️⃣ Create New Repository

1. Click **"+"** (top right) → **"New repository"**

2. **Fill Details:**

   **Repository Name:**
   ```
   TPN-Calculator
   ```

   **Description:**
   ```
   Total Parenteral Nutrition (TPN) Calculator - Healthcare application for calculating IV nutrition values (calories, proteins, lipids, electrolytes) based on patient parameters. Built with Angular & ASP.NET.
   ```

   **Visibility:**
   - ✅ **Public** (for portfolio, sharing)
   - ☐ **Private** (if hospital confidential)

   **DO NOT Initialize:**
   - ☐ Add README (we already have one)
   - ☐ Add .gitignore (we already have one)
   - ☐ Choose a license

3. Click **"Create repository"**

---

## ✅ STEP 11: CONNECT LOCAL REPO TO GITHUB

### 1️⃣ Copy Repository URL
On the GitHub page, copy the HTTPS URL:
```
https://github.com/YOUR_USERNAME/TPN-Calculator.git
```

### 2️⃣ Add Remote Origin
```bash
cd "e:\Aman Project Files\TPN_Calculations"
git remote add origin https://github.com/YOUR_USERNAME/TPN-Calculator.git
```

**Replace** `YOUR_USERNAME` with your GitHub username.

### 3️⃣ Verify Remote
```bash
git remote -v
```

**Output:**
```
origin  https://github.com/YOUR_USERNAME/TPN-Calculator.git (fetch)
origin  https://github.com/YOUR_USERNAME/TPN-Calculator.git (push)
```

---

## ✅ STEP 12: PUSH CODE TO GITHUB

### 1️⃣ Rename Branch (GitHub Default is 'main', not 'master')
```bash
git branch -M main
```

### 2️⃣ Push Code
```bash
git push -u origin main
```

**What Happens?**
1. Git will ask for GitHub credentials
2. Enter your **GitHub username**
3. Enter your **Personal Access Token** (NOT password - see below)

### 3️⃣ Create GitHub Personal Access Token (If Needed)

**GitHub removed password authentication. You need a token.**

1. Go to: `https://github.com/settings/tokens`
2. Click **"Generate new token (classic)"**
3. **Note:** `TPN Calculator Access`
4. **Expiration:** 90 days (or custom)
5. **Scopes:** Check ✅ `repo` (full control of private repositories)
6. Click **"Generate token"**
7. **COPY THE TOKEN** (you won't see it again)
8. Use this token as your password when pushing

### 4️⃣ Successful Push Output:
```
Enumerating objects: 150, done.
Counting objects: 100% (150/150), done.
Delta compression using up to 8 threads
Compressing objects: 100% (120/120), done.
Writing objects: 100% (150/150), 1.5 MiB | 2.5 MiB/s, done.
Total 150 (delta 30), reused 0 (delta 0)
To https://github.com/YOUR_USERNAME/TPN-Calculator.git
 * [new branch]      main -> main
Branch 'main' set up to track remote branch 'main' from 'origin'.
```

---

## ✅ STEP 13: VERIFY ON GITHUB

1. Go to: `https://github.com/YOUR_USERNAME/TPN-Calculator`
2. You should see:
   - ✅ Your project folders
   - ✅ `.gitignore` file
   - ✅ `README.md` preview
   - ✅ Commit message

---

## ✅ STEP 14: FUTURE WORKFLOW

### After Making Changes:

```bash
# 1. Check what changed
git status

# 2. Stage changes
git add .

# 3. Commit with descriptive message
git commit -m "fix: Update dextrose concentration calculation for pediatric patients"

# 4. Push to GitHub
git push
```

### Before Making Critical Changes:

```bash
# Create a new branch for testing
git checkout -b feature/new-calculation-method

# Make changes, test thoroughly
# ...

# If successful, merge back
git checkout main
git merge feature/new-calculation-method
git push
```

---

## 🎯 REPOSITORY STRUCTURE (BEST PRACTICES)

### **Ideal GitHub Structure:**
```
TPN-Calculator/
├── .gitignore                       # What to ignore
├── README.md                        # Main documentation
├── LICENSE                          # MIT/Apache (if open source)
├── CONTRIBUTING.md                  # How others can contribute
├── CHANGELOG.md                     # Version history
│
├── TPN-Calculator-Angular/          # Frontend
│   ├── src/
│   ├── package.json
│   └── README.md                    # Angular-specific setup
│
├── TPN_Calculations/                # Backend
│   ├── TPNMAIN.aspx                # Main calculation page
│   ├── Web.config.example          # Template config
│   └── README.md                    # ASP.NET setup
│
├── database/                        # Database files
│   ├── schema.sql                  # Table structures
│   ├── stored-procedures.sql       # All procedures
│   ├── sample-data.sql             # Test data (no real patient data)
│   └── README.md                    # Database setup guide
│
├── docs/                            # Documentation
│   ├── PROJECT_DOCUMENTATION.md    # Full technical docs
│   ├── DEPLOYMENT_GUIDE.md         # How to deploy
│   ├── CALCULATION_LOGIC.md        # Medical calculation explanations
│   ├── API_DOCUMENTATION.md        # Backend API reference
│   └── TESTING_GUIDE.md            # How to test calculations
│
└── .github/                         # GitHub-specific
    └── workflows/                   # CI/CD automation (optional)
        └── angular-build.yml        # Auto-test on commit
```

---

## 🔒 SECURITY CHECKLIST

### ✅ Before Pushing to GitHub:

- [x] `.gitignore` prevents `Web.config` upload
- [x] `.gitignore` prevents database backups upload
- [x] No patient data in sample SQL files
- [x] No real passwords in configuration examples
- [x] Remove all TODO comments with sensitive info
- [x] Check for API keys in Angular environment files
- [x] Verify no `.mdf` / `.ldf` database files

### Scan for Secrets:
```bash
# Search for potential passwords
findstr /s /i "password" *.cs *.config *.ts *.json

# Search for connection strings
findstr /s /i "connectionstring" *.cs *.config *.ts
```

**If found outside `Web.config.example`:* Remove them!

---

## 📊 NEXT STEPS

✅ **COMPLETED:**
- Git installed and configured
- .gitignore created
- Repository initialized
- Code committed
- GitHub repository created
- Code pushed to GitHub

➡️ **NEXT: PART 2 - DEPLOYMENT**
See: `DEPLOYMENT-GUIDE.md`

---

## 🆘 TROUBLESHOOTING

### **Error: "fatal: not a git repository"**
```bash
# Make sure you're in the project directory
cd "e:\Aman Project Files\TPN_Calculations"
git init
```

### **Error: "failed to push some refs"**
```bash
# Pull first, then push
git pull origin main --allow-unrelated-histories
git push origin main
```

### **Error: "large files detected"**
```bash
# Check what's being uploaded
git ls-files --cached | Select-String -Pattern "\.bak|\.mdf|\.ldf"

# If found, remove from cache
git rm --cached path/to/large/file
# Update .gitignore
# Commit again
```

### **Error: "Support for password authentication was removed"**
- Create Personal Access Token (see Step 12.3 above)
- Use token instead of password

---

## 📞 SUPPORT RESOURCES

- **Git Official Docs:** https://git-scm.com/doc
- **GitHub Guides:** https://guides.github.com
- **Healthcare Software Best Practices:** FDA Guidance on Software Validation

---

**🎉 CONGRATULATIONS!**
Your TPN Calculator is now safely version-controlled and backed up on GitHub!

**Next:** Deploy it to get a live URL → See `DEPLOYMENT-GUIDE.md`
