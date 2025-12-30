# ⚡ QUICK START GUIDE - From Zero to Live Deployment

**Goal:** Get your TPN Calculator from localhost → GitHub → Live URL in 2 hours

---

## 📊 TIME ESTIMATE

| Task | Time |
|------|------|
| ✅ GitHub Setup | 20 min |
| ✅ Frontend Deployment (Netlify) | 15 min |
| ✅ Documentation | 10 min |
| ⏰ **TOTAL** | **45 minutes** |

*(Backend deployment requires migration to .NET Core = additional 4-6 hours)*

---

## 🚀 PHASE 1: GITHUB SETUP (20 MIN)

### Step 1: Install Git (5 min)

```bash
# Download and install Git for Windows
# https://git-scm.com/download/win

# Verify installation
git --version
```

### Step 2: Configure Git (2 min)

```bash
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

### Step 3: Initialize Repository (3 min)

```bash
# Navigate to project
cd "e:\Aman Project Files\TPN_Calculations"

# Initialize Git
git init

# Add all files (will respect .gitignore)
git add .

# First commit
git commit -m "Initial commit: TPN Calculator v1.0"
```

### Step 4: Create GitHub Repository (5 min)

1. Go to: https://github.com
2. Sign in (or create account)
3. Click **"+"** → **"New repository"**
4. **Name:** `TPN-Calculator`
5. **Description:** `Total Parenteral Nutrition Calculator - Healthcare web app`
6. **Visibility:** Public
7. **DO NOT** initialize with README
8. Click **"Create repository"**

### Step 5: Push to GitHub (5 min)

```bash
# Add remote (replace YOUR_USERNAME)
git remote add origin https://github.com/YOUR_USERNAME/TPN-Calculator.git

# Rename branch to main
git branch -M main

# Push
git push -u origin main
```

**You'll be asked for credentials:**
- Username: [Your GitHub username]
- Password: [Personal Access Token - see below]

**Create Token:**
1. Go to: https://github.com/settings/tokens
2. Generate new token (classic)
3. Select scope: `repo`
4. Copy token and use as password

✅ **DONE!** View at: `https://github.com/YOUR_USERNAME/TPN-Calculator`

---

## 🌐 PHASE 2: FRONTEND DEPLOYMENT (15 MIN)

### Option A: Deploy to Netlify (Recommended)

#### Step 1: Create Netlify Account (3 min)

1. Go to: https://www.netlify.com
2. Click **"Sign up with GitHub"**
3. Authorize Netlify

#### Step 2: Import Project (2 min)

1. Click **"Add new site"** → **"Import an existing project"**
2. Choose **"GitHub"**
3. Select **`TPN-Calculator`** repository

#### Step 3: Configure Build (5 min)

**Build settings:**
```
Base directory: TPN-Calculator-Angular
Build command: npm run build -- --configuration production
Publish directory: TPN-Calculator-Angular/dist/tpn-calculator
```

####Step 4: Create `netlify.toml` (3 min)

**File:** `TPN-Calculator-Angular/netlify.toml`

```toml
[build]
  publish = "dist/tpn-calculator"
  command = "npm run build -- --configuration production"

[build.environment]
  NODE_VERSION = "18"

[[redirects]]
  from = "/*"
  to = "/index.html"
  status = 200
```

**Commit and push:**
```bash
git add TPN-Calculator-Angular/netlify.toml
git commit -m "config: Add Netlify deployment configuration"
git push
```

#### Step 5:Deploy (2 min)

1. Click **"Deploy site"**
2. Wait for build (3-5 minutes)
3. ✅ **LIVE!**

**Your URL:**
```
https://random-name-123456.netlify.app
```

#### Step 6: Customize URL (Optional)

1. Go to **Site settings** → **Change site name**
2. Enter: `tpn-calculator-yourname`
3. New URL: `https://tpn-calculator-yourname.netlify.app`

---

## 📝 PHASE 3: FINALIZE DOCUMENTATION (10 MIN)

### Step 1: Update README with Live URL (5 min)

Edit `README-NEW.md`:

```markdown
🌐 **Live Demo:** https://tpn-calculator-yourname.netlify.app
```

Rename and commit:
```bash
# Backup old README
mv README.md README-OLD.md

# Use new README
mv README-NEW.md README.md

git add README.md
git commit -m "docs: Update README with live deployment URL"
git push
```

### Step 2: Add Screenshot (5 min)

1. Open your live site
2. Take screenshot
3. Save as `screenshots/main-screen.png`
4. Update README:

```markdown
## Screenshots

![Main Screen](screenshots/main-screen.png)
```

```bash
git add screenshots/
git commit -m "docs: Add screenshots"
git push
```

---

## 🎉 YOU'RE LIVE!

✅ **GitHub Repository:** `https://github.com/YOUR_USERNAME/TPN-Calculator`  
✅ **Live Application:** `https://tpn-calculator-yourname.netlify.app`  
✅ **Professional Documentation:** Complete

---

## 📧 SHARE YOUR PROJECT

### For Portfolio:
```
I built a healthcare web app for calculating Total Parenteral Nutrition (TPN).

🌐 Live Demo: https://tpn-calculator-yourname.netlify.app
💻 GitHub: https://github.com/YOUR_USERNAME/TPN-Calculator

Tech Stack: Angular 15, ASP.NET, SQL Server
Purpose: Eliminate calculation errors in IV nutrition prescriptions
```

### For LinkedIn:
```
🏥 Just deployed my healthcare project!

"TPN Calculator" - A clinical decision support tool for Total Parenteral Nutrition calculations.

✨ Features:
✅ Automated calorie/protein/lipid calculations
✅ Safety validation & warnings
✅ Medical-grade accuracy

Built with Angular, ASP.NET & SQL Server.

#Healthcare #MedicalSoftware #WebDevelopment #Angular

🔗 https://tpn-calculator-yourname.netlify.app
```

---

## 🔄 ONGOING WORKFLOW

### Making Updates:

```bash
# 1. Make changes to code

# 2. Test locally
cd TPN-Calculator-Angular
npm start

# 3. Stage changes
git add .

# 4. Commit with clear message
git commit -m "feat: Add dark mode toggle"

# 5. Push to GitHub
git push

# 6. Netlify auto-deploys!
# Check: https://app.netlify.com
```

**Netlify automatically rebuilds on every push!**

---

## 🆘 TROUBLESHOOTING

### "Build failed" on Netlify

**Check build log for errors:**

Common issues:
1. **Missing dependencies** → Run `npm install` locally first
2. **TypeScript errors** → Run `ng build --prod` locally
3. **Wrong Node version** → Add to `netlify.toml`:
   ```toml
   [build.environment]
     NODE_VERSION = "18"
   ```

### "404 Not Found" on refresh

**Fix:** Ensure `netlify.toml` has redirect rule:
```toml
[[redirects]]
  from = "/*"
  to = "/index.html"
  status = 200
```

### "Module not found" error

**Fix:**
```bash
cd TPN-Calculator-Angular
rm -rf node_modules package-lock.json
npm install
git add package-lock.json
git commit -m "fix: Update dependencies"
git push
```

---

## 📚 NEXT STEPS

### Immediate:
- [ ] Add medical disclaimer to home page
- [ ] Test all calculations with sample patients
- [ ] Get clinical review from pharmacist
- [ ] Add Google Analytics (optional)

### Week 1:
- [ ] Share with 3 colleagues for feedback
- [ ] Fix any bugs discovered
- [ ] Add FAQ section
- [ ] Create video tutorial

### Month 1:
- [ ] Consider backend deployment (.NET Core migration)
- [ ] Add user authentication
- [ ] Implement calculation history
- [ ] Mobile app planning

---

## 🎯 SUCCESS CHECKLIST

- [x] Code on GitHub
- [x] Live URL working
- [x] Professional documentation
- [x] Medical disclaimer visible
- [ ] Clinical validation complete
- [ ] Shared on portfolio
- [ ] LinkedIn post published

---

**🎉 CONGRATULATIONS!**

You now have a fully deployed, professional healthcare application!

**Share it with the world and help improve patient care! 🏥💙**
