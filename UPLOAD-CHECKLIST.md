# ✅ GITHUB UPLOAD CHECKLIST

**Use this checklist as you complete each step!**

---

## 📋 **STEP 1: CREATE GITHUB REPOSITORY** (2 min)

### ✅ Actions:

- [ ] **1.1** Open https://github.com/new (DONE - Already opened in browser!)
- [ ] **1.2** Repository name: `TPN-Calculator`
- [ ] **1.3** Description: `Total Parenteral Nutrition Calculator - Healthcare web app`
- [ ] **1.4** Visibility: ✅ Public (recommended)
- [ ] **1.5** DO NOT check: "Add README", "Add .gitignore", "Choose license"
- [ ] **1.6** Click "Create repository"
- [ ] **1.7** Leave tab open

**✅ Step 1 Complete** when you see: `https://github.com/aman7506/TPN-Calculator`

---

## 📋 **STEP 2: GET PERSONAL ACCESS TOKEN** (1 min)

### ✅ Actions:

- [ ] **2.1** Open https://github.com/settings/tokens
- [ ] **2.2** Click "Generate new token (classic)"
- [ ] **2.3** Enter password if asked
- [ ] **2.4** Note: `TPN Calculator Access`
- [ ] **2.5** Expiration: `90 days`
- [ ] **2.6** Scope: ✅ Check ONLY `repo`
- [ ] **2.7** Click "Generate token"
- [ ] **2.8** **COPY TOKEN** (starts with `ghp_`)
- [ ] **2.9** Paste in Notepad and save

**✅ Step 2 Complete** when token is saved in Notepad

---

## 📋 **STEP 3: PUSH TO GITHUB** (30 sec)

### ✅ Actions:

- [ ] **3.1** Open PowerShell (Windows + X → PowerShell)
- [ ] **3.2** Run: `cd "e:\Aman Project Files\TPN_Calculations"`
- [ ] **3.3** Run: `$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")`
- [ ] **3.4** Run: `git --version` (verify Git works)
- [ ] **3.5** Run: `git status` (should say "nothing to commit")
- [ ] **3.6** Run: `git push -u origin main`
- [ ] **3.7** Username: `aman7506`
- [ ] **3.8** Password: **[Paste token from Notepad]**
- [ ] **3.9** Wait for upload (30 seconds)

**✅ Step 3 Complete** when you see: `Branch 'main' set up to track remote branch`

---

## 📋 **STEP 4: VERIFY** (30 sec)

### ✅ Actions:

- [ ] **4.1** Go to https://github.com/aman7506/TPN-Calculator
- [ ] **4.2** See README.md displayed
- [ ] **4.3** See folders: TPN-Calculator-Angular, TPN_Calculations
- [ ] **4.4** See ~850 files
- [ ] **4.5** Verify NO `node_modules/` (correct!)
- [ ] **4.6** Verify NO `Web.config` (correct!)

**✅ Step 4 Complete** when all files are visible on GitHub!

---

## 🎉 **ALL DONE!**

**When all 4 steps are checked, you have:**

✅ Code on GitHub: https://github.com/aman7506/TPN-Calculator  
✅ Professional documentation  
✅ Version control setup  
✅ Portfolio-ready project  

---

## 🚀 **OPTIONAL: DEPLOY TO NETLIFY** (15 min)

- [ ] **5.1** Go to https://www.netlify.com
- [ ] **5.2** Sign up with GitHub
- [ ] **5.3** Import TPN-Calculator repo
- [ ] **5.4** Build: `npm run build -- --configuration production`
- [ ] **5.5** Publish: `TPN-Calculator-Angular/dist/tpn-calculator`
- [ ] **5.6** Deploy
- [ ] **5.7** Get live URL!

**See:** `QUICK-START-DEPLOYMENT.md` for details

---

## 📞 **NEED HELP?**

**Step 1 issues:** Check `STEP-BY-STEP-UPLOAD-GUIDE.md` → Step 1  
**Step 2 issues:** Check `STEP-BY-STEP-UPLOAD-GUIDE.md` → Step 2  
**Step 3 issues:** Check `STEP-BY-STEP-UPLOAD-GUIDE.md` → Step 3  
**Git errors:** Check `STEP-BY-STEP-UPLOAD-GUIDE.md` → Troubleshooting  

---

**🎯 FOCUS:** Complete Steps 1-3 (takes 3 minutes total)

**⭐ You're almost there! Just 3 simple steps!**
