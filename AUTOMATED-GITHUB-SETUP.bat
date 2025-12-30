@echo off
REM ========================================
REM AUTOMATED GITHUB SETUP FOR TPN CALCULATOR
REM ========================================

echo.
echo ========================================
echo TPN CALCULATOR - AUTOMATED GITHUB SETUP
echo ========================================
echo.
echo Your GitHub Profile: https://github.com/aman7506/
echo Target Repository: TPN-Calculator
echo.

REM Add Git to PATH for this session
set "PATH=%PATH%;C:\Program Files\Git\bin"

REM Navigate to project directory
cd /d "e:\Aman Project Files\TPN_Calculations"

echo [1/8] Checking Git installation...
git --version
if %errorlevel% neq 0 (
    echo ERROR: Git is not installed or not in PATH
    echo Please restart your terminal or computer after installing Git
    pause
    exit /b 1
)
echo ✓ Git found!
echo.

echo [2/8] Configuring Git with your GitHub profile...
git config --global user.name "aman7506"
git config --global user.email "aman7506@users.noreply.github.com"
echo ✓ Git configured!
echo.

echo [3/8] Initializing Git repository...
if exist ".git" (
    echo Repository already initialized
) else (
    git init
    echo ✓ Repository initialized!
)
echo.

echo [4/8] Adding all files (respecting .gitignore)...
git add .
echo ✓ Files staged!
echo.

echo [5/8] Creating initial commit...
git commit -m "Initial commit: TPN Calculator v1.0 - Complete medical calculation system

- Angular 15 frontend with Material Design
- ASP.NET Web Forms backend (.NET Framework)
- SQL Server database (9 tables, 10 stored procedures)
- Complete documentation (10 professional guides)
- Medical-grade calculation logic (ASPEN/ESPEN compliant)
- Testing procedures and validation
- Deployment guides for Netlify and Azure

Tech Stack: Angular, TypeScript, ASP.NET, C#, SQL Server
Purpose: Eliminate errors in Total Parenteral Nutrition calculations
Target Users: Doctors, Clinical Pharmacists, Nurses"
echo ✓ Initial commit created!
echo.

echo [6/8] Adding GitHub remote...
git remote remove origin 2>nul
git remote add origin https://github.com/aman7506/TPN-Calculator.git
echo ✓ Remote added!
echo.

echo [7/8] Renaming branch to main...
git branch -M main
echo ✓ Branch renamed!
echo.

echo [8/8] Ready to push to GitHub!
echo.
echo ========================================
echo NEXT STEPS:
echo ========================================
echo.
echo 1. Create repository on GitHub:
echo    → Go to: https://github.com/new
echo    → Repository name: TPN-Calculator
echo    → Description: Total Parenteral Nutrition Calculator - Healthcare web app
echo    → Visibility: Public (or Private)
echo    → DO NOT initialize with README, .gitignore, or license
echo    → Click "Create repository"
echo.
echo 2. Get your Personal Access Token:
echo    → Go to: https://github.com/settings/tokens
echo    → Generate new token (classic)
echo    → Note: "TPN Calculator Access"
echo    → Scopes: Check "repo" (full control)
echo    → Generate token and COPY IT
echo.
echo 3. Push to GitHub:
echo    → Run: git push -u origin main
echo    → Username: aman7506
echo    → Password: [paste your token]
echo.
echo ========================================
echo ALL LOCAL SETUP COMPLETE! ✓
echo ========================================
echo.
echo Your local Git is configured and ready!
echo.
echo To complete the upload:
echo   1. Create repository: https://github.com/new
echo   2. Then run: git push -u origin main
echo.

pause
