# 🚀 PART 2: DEPLOYMENT GUIDE
## Deploy TPN Calculator to Get Live URL

---

## 🎯 DEPLOYMENT STRATEGY

### **Your Project Architecture:**
```
┌─────────────────────────────────────────────┐
│         USER'S BROWSER                      │
│      (Doctors, Nurses, Pharmacists)         │
└──────────────┬──────────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────────┐
│      ANGULAR FRONTEND                       │
│      (Static HTML/CSS/JavaScript)           │
│      Platform: Netlify / Vercel             │
│      URL: https://tpn-calculator.netlify.app│
└──────────────┬──────────────────────────────┘
               │
               │ API Calls
               ▼
┌─────────────────────────────────────────────┐
│      ASP.NET BACKEND                        │
│      (.NET Framework Web Forms)             │
│      Platform: Render / Azure               │
│      URL: https://tpn-api.onrender.com      │
└──────────────┬──────────────────────────────┘
               │
               │ SQL Queries
               ▼
┌─────────────────────────────────────────────┐
│      SQL SERVER DATABASE                    │
│      Platform: Azure SQL / ElephantSQL      │
│      Connection: Encrypted connection string│
└─────────────────────────────────────────────┘
```

---

## 📊 PLATFORM COMPARISON

### **Frontend Deployment Options:**

| Platform | Free Tier | Custom Domain | Build Time | Best For |
|----------|-----------|---------------|------------|----------|
| **Netlify** ⭐ | ✅ Yes (100GB/mo) | ✅ Free SSL | Fast | Angular/React |
| **Vercel** | ✅ Yes (100GB/mo) | ✅ Free SSL | Very Fast | Next.js primary |
| **GitHub Pages** | ✅ Yes | ✅ Free | Manual | Static only |
| **Azure Static Apps** | ✅ Yes | ✅ Free SSL | Medium | Enterprise |

**🏆 RECOMMENDATION: Netlify**
- Easiest for Angular
- Automatic deployment from GitHub
- Free SSL certificate
- Great for healthcare apps (SOC 2 compliant)

---

### **Backend Deployment Options:**

| Platform | Free Tier | Database | Best For |
|----------|-----------|----------|----------|
| **Render** ⭐ | ✅ Yes (750hrs) | PostgreSQL only | .NET Core |
| **Azure App Service** | ❌ Paid ($13/mo) | ✅ SQL Server | .NET Framework |
| **AWS Elastic Beanstalk** | ❌ Paid | ✅ RDS SQL | Enterprise |
| **Heroku** | ❌ (No free tier) | PostgreSQL | Node/Python |

**⚠️ ISSUE:** Your backend is .NET Framework (Web Forms), NOT .NET Core.

**🏆 RECOMMENDATION for .NET Framework:**
- **Option 1:** Migrate to .NET Core 8 (modern, free hosting on Render)
- **Option 2:** Use Azure App Service (paid, but supports .NET Framework)
- **Option 3:** Convert calculations to API and rebuild in Node.js/FastAPI

---

### **Database Deployment Options:**

| Platform | Free Tier | Best For | SQL Server? |
|----------|-----------|----------|-------------|
| **Azure SQL Database** ⭐ | ❌ Paid ($5/mo) | .NET apps | ✅ Yes |
| **Supabase** | ✅ Free (500MB) | Modern apps | ❌ PostgreSQL |
| **Railway** | ✅ Free ($5 credit) | Small projects | ❌ PostgreSQL |
| **Local + ngrok** | ✅ Free | Development | ✅ Any |

**🏆 RECOMMENDATION: Azure SQL Database**
- Native SQL Server compatibility
- HIPAA-compliant (healthcare grade)
- Automated backups

---

## 🎯 DEPLOYMENT PATH DECISION

### ⚡ **QUICK PATH (Frontend Only - Calculations in Browser)**
✅ **Best for:** Demo, Portfolio, Non-Critical Use  
✅ **Cost:** $0  
✅ **Time:** 30 minutes  

**Steps:**
1. Move ALL calculation logic to Angular (TypeScript)
2. Remove backend dependency
3. Deploy Angular to Netlify
4. Data saved in browser `localStorage` (no database)

**✅ PROS:**
- Completely free
- Fast deployment
- No server maintenance

**❌ CONS:**
- No data persistence across devices
- No user management
- Calculations not centrally auditable

---

### 🏥 **PRODUCTION PATH (Full Stack - Medical Grade)**
✅ **Best for:** Hospital Use, Clinical Deployment  
✅ **Cost:** ~$20/month  
✅ **Time:** 4-6 hours  

**Steps:**
1. Keep Angular frontend
2. Migrate backend to .NET Core 8 (modern)
3. Deploy frontend to Netlify
4. Deploy backend to Render
5. Deploy database to Azure SQL
6. Configure CORS, SSL, environment variables

**✅ PROS:**
- Full audit trail
- Multi-user support
- Centralized data
- Secure authentication

**❌ CONS:**
- Monthly cost
- More complex setup

---

## 🚀 DEPLOYMENT OPTION 1: FRONTEND-ONLY (FREE)

### **Prerequisites:**
- GitHub repository created (from Part 1)
- Node.js installed
- Angular project working locally

---

### ✅ STEP 1: MOVE CALCULATIONS TO ANGULAR

#### **Current Architecture:**
```
User Input (Angular) → HTTP Request → ASP.NET Backend → Database
```

#### **New Architecture:**
```
User Input (Angular) → TypeScript Calculations → localStorage
```

**See:** `MIGRATION-TO-FRONTEND-ONLY.md` (will create below)

---

### ✅ STEP 2: UPDATE ANGULAR ENVIRONMENT FOR PRODUCTION

#### Navigate to Environments
```bash
cd "e:\Aman Project Files\TPN_Calculations\TPN-Calculator-Angular\src\environments"
```

#### Edit `environment.prod.ts`

**File:** `src/environments/environment.prod.ts`

```typescript
export const environment = {
  production: true,
  appName: 'TPN Calculator',
  version: '1.0.0',
  // No API URL needed (frontend-only)
  enableLogging: false,
  maxCalculationHistory: 50 // Store last 50 calculations
};
```

---

### ✅ STEP 3: BUILD PRODUCTION ANGULAR APP

```bash
cd "e:\Aman Project Files\TPN_Calculations\TPN-Calculator-Angular"
```

```bash
npm run build -- --configuration production
```

**Output:**
```
✔ Browser application bundle generation complete.
✔ Copying assets complete.
✔ Index html generation complete.

Initial Chunk Files               | Names         |  Raw Size
main.js                           | main          | 250.5 kB | 
polyfills.js                      | polyfills     |  90.2 kB | 
styles.css                        | styles        |  15.3 kB | 

Build at: 2025-12-30T12:30:00.000Z - Hash: abc123 - Time: 15000ms
```

**✅ Build Complete!**  
Files are in: `TPN-Calculator-Angular/dist/tpn-calculator/`

---

### ✅ STEP 4: CREATE NETLIFY CONFIGURATION

#### Create `netlify.toml` file

**Location:** `TPN-Calculator-Angular/netlify.toml`

```toml
[build]
  # Directory with built files
  publish = "dist/tpn-calculator"
  
  # Build command
  command = "npm run build -- --configuration production"

[build.environment]
  # Node version for build
  NODE_VERSION = "18"

[[redirects]]
  # Redirect all routes to index.html (Angular routing)
  from = "/*"
  to = "/index.html"
  status = 200

[[headers]]
  # Security headers (healthcare-grade)
  for = "/*"
  [headers.values]
    X-Frame-Options = "DENY"
    X-Content-Type-Options = "nosniff"
    X-XSS-Protection = "1; mode=block"
    Referrer-Policy = "strict-origin-when-cross-origin"
    Permissions-Policy = "geolocation=(), microphone=(), camera=()"
    
[[headers]]
  # Cache static assets
  for = "/assets/*"
  [headers.values]
    Cache-Control = "public, max-age=31536000, immutable"
```

**Save** and **commit** to GitHub:
```bash
git add netlify.toml
git commit -m "config: Add Netlify deployment configuration"
git push
```

---

### ✅ STEP 5: DEPLOY TO NETLIFY

#### 1️⃣ Create Netlify Account
Go to: `https://www.netlify.com/`
- Click **"Sign up"**
- Choose **"Sign up with GitHub"**
- Authorize Netlify

#### 2️⃣ Import Project
1. Click **"Add new site"** → **"Import an existing project"**
2. Choose **"Deploy with GitHub"**
3. Authorize Netlify to access your repositories
4. Select **`TPN-Calculator`** repository

#### 3️⃣ Configure Build Settings

**Base directory:** `TPN-Calculator-Angular`  
**Build command:** `npm run build -- --configuration production`  
**Publish directory:** `dist/tpn-calculator`  

#### 4️⃣ Deploy!
Click **"Deploy TPN-Calculator"**

**Build Process (3-5 minutes):**
```
Initializing build environment...
Installing Node.js 18...
Installing dependencies (npm install)...
Running build command...
✅ Build complete!
Deploying to CDN...
✅ Site is live!
```

#### 5️⃣ Get Your Live URL

**Netlify assigns a random URL:**
```
https://random-name-123456.netlify.app
```

**Test it!** Open in browser.

---

### ✅ STEP 6: CUSTOMIZE DOMAIN (Optional)

#### Change Site Name:
1. Go to **Site settings** → **General** → **Site details**
2. Click **"Change site name"**
3. Enter: `tpn-calculator-yourname`
4. Save

**New URL:**
```
https://tpn-calculator-yourname.netlify.app
```

#### Add Custom Domain (Optional):
1. Buy domain (e.g., `tpncalculator.com` from Namecheap)
2. In Netlify: **Domain settings** → **Add custom domain**
3. Follow DNS configuration steps
4. ✅ Free SSL certificate auto-installed

---

## 🚀 DEPLOYMENT OPTION 2: FULL STACK (PAID)

### **BACKEND MIGRATION REQUIRED**

Your current backend is **.NET Framework Web Forms** which is **NOT supported** by free hosting.

### **Options:**

---

### **Option A: Migrate to .NET Core 8** (Recommended)

#### ✅ **Advantages:**
- Free hosting on Render
- Modern, performant
- Cross-platform
- JSON API (cleaner than Web Forms)

#### **Steps:**
1. Create new .NET Core 8 Web API project
2. Copy calculation logic from `TPNMAIN.aspx.cs`
3. Convert to RESTful API controllers
4. Update Angular to call new API
5. Deploy to Render (free)

**Estimated Time:** 4-6 hours  
**Cost:** $0 (Render free tier)

**See:** `BACKEND-MIGRATION-GUIDE.md` (will create)

---

### **Option B: Use Azure App Service**

#### ✅ **Advantages:**
- Direct support for .NET Framework
- No code changes needed
- SQL Server integration

#### ❌ **Disadvantages:**
- **Cost:** ~$13/month (Basic plan)
- Overkill for small project

#### **Steps:**
1. Create Azure account
2. Create App Service (Windows, .NET Framework 4.8)
3. Create Azure SQL Database
4. Set connection string in Azure
5. Deploy via Visual Studio

**Estimated Time:** 2 hours  
**Cost:** ~$20/month (app + database)

---

### **Option C: Serverless Functions (Azure Functions)**

Convert each calculation to an Azure Function.

**Pros:** Pay-per-use  
**Cons:** Requires significant refactoring

---

## 🗄️ DATABASE DEPLOYMENT

### **Option 1: Azure SQL Database** (Recommended)

#### **Pricing:**
- **Basic Tier:** $5/month (2GB storage)
- **Standard S0:** $15/month (250GB storage)

#### **Setup Steps:**

1. **Create Azure Account:** `https://azure.microsoft.com/free/`
2. **Create SQL Database:**
   - Go to **Azure Portal** → **Create a resource** → **SQL Database**
   - **Database name:** `tpncalculations`
   - **Server:** Create new
   - **Pricing tier:** Basic ($5/mo)
   - Click **Create**

3. **Get Connection String:**
   ```
   Server=tcp:tpn-server.database.windows.net,1433;
   Initial Catalog=tpncalculations;
   User ID=sqladmin;
   Password=YOUR_SECURE_PASSWORD;
   Encrypt=True;
   ```

4. **Configure Firewall:**
   - Allow Azure services
   - Add your IP address

5. **Restore Database:**
   - Use SSMS to connect to Azure SQL
   - Run your `schema.sql`
   - Insert sample data

---

### **Option 2: Local Database + ngrok** (Development Only)

Keep database on your local machine, expose via tunnel.

**Steps:**
```bash
# Install ngrok
choco install ngrok

# Expose local SQL Server
ngrok tcp 1433
```

**❌ Not Recommended for Production:**
- Your PC must stay on
- Security risks
- Unreliable

---

## 🔐 ENVIRONMENT VARIABLES & SECRETS

### **Angular Environment (Frontend)**

**File:** `src/environments/environment.prod.ts`

```typescript
export const environment = {
  production: true,
  apiUrl: 'https://tpn-api.onrender.com', // Your deployed backend
  appVersion: '1.0.0',
  enableAnalytics: false
};
```

### **ASP.NET Backend (If using .NET Core)**

**File:** `appsettings.Production.json` (NOT in GitHub)

```json
{
  "ConnectionStrings": {
    "TPNDatabase": "Server=tcp:tpn-server.database.windows.net,1433;..."
  },
  "Jwt": {
    "SecretKey": "YOUR_SECRET_KEY_HERE_MIN_32_CHARS",
    "Issuer": "TPNCalculator",
    "Audience": "TPNUsers"
  },
  "AllowedOrigins": [
    "https://tpn-calculator-yourname.netlify.app"
  ]
}
```

### **Set Secrets in Netlify:**
1. Go to **Site settings** → **Environment variables**
2. Add:
   - `API_URL` = `https://tpn-api.onrender.com`

### **Set Secrets in Render:**
1. Go to **Dashboard** → **Environment**
2. Add:
   - `DATABASE_URL` = `Server=tcp:...` (from Azure)
   - `JWT_SECRET` = (generate random 32-char string)

---

## ✅ PRODUCTION BUILD CHECKLIST

### **Before Deploying:**

- [ ] All calculations tested with medical sample data
- [ ] No console.log() statements in production code
- [ ] Error handling implemented (try-catch blocks)
- [ ] Loading indicators for API calls
- [ ] User-friendly error messages (not technical jargon)
- [ ] Medical disclaimer displayed prominently
- [ ] Input validation (prevent negative weights, etc.)
- [ ] SSL certificate enabled (HTTPS)
- [ ] CORS configured correctly
- [ ] Database connection encrypted
- [ ] No sensitive data in logs
- [ ] Backup strategy in place

---

## 🧪 TESTING BEFORE GO-LIVE

### **1. Calculation Accuracy Testing**

Create test cases:

**File:** `tests/calculation-tests.csv`

```csv
TestID,Weight_kg,Height_cm,Age,ExpectedCalories,ExpectedProtein,ExpectedLipids
TEST001,70,170,30,2100,105,70
TEST002,50,160,25,1750,87.5,58.3
TEST003,90,180,45,2520,126,84
```

Run each test case and verify results.

### **2. Cross-Browser Testing**
- ✅ Chrome
- ✅ Firefox
- ✅ Edge
- ✅ Safari (iOS)
- ✅ Mobile browsers

### **3. Load Testing**
Use: `https://www.webpagetest.org/`
- Enter your Netlify URL
- Check load time (should be < 3 seconds)

---

## 📊 DEPLOYMENT SUMMARY

### **Recommended Path for TPN Calculator:**

#### **Immediate (Free Demo):**
```
✅ Frontend: Netlify
✅ Calculations: In-browser (TypeScript)
✅ Storage: localStorage
✅ Cost: $0
✅ URL: https://tpn-calculator-yourname.netlify.app
```

#### **Future (Production):**
```
✅ Frontend: Netlify
✅ Backend: Render (.NET Core 8 API)
✅ Database: Azure SQL Database
✅ Cost: ~$20/month
✅ URLs:
   - Frontend: https://tpn-calculator.netlify.app
   - Backend API: https://tpn-api.onrender.com
```

---

## 🆘 COMMON DEPLOYMENT ERRORS & FIXES

### **Error: "Module not found" during Netlify build**
```bash
# Fix: Ensure all dependencies in package.json
npm install
git add package.json package-lock.json
git commit -m "fix: Update dependencies"
git push
```

### **Error: "404 on page refresh" (Angular routing)**
**Fix:** Add `netlify.toml` with redirect rules (see Step 4 above)

### **Error: "CORS policy blocking API requests"**
**Fix:** Add CORS headers in backend:
```csharp
// .NET Core Startup.cs
services.AddCors(options => {
    options.AddPolicy("AllowNetlify", builder => {
        builder.WithOrigins("https://tpn-calculator-yourname.netlify.app")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```

### **Error: "Database connection failed"**
**Fix:** Check connection string, firewall rules, credentials

### **Error: "Build failed - out of memory"**
**Fix:** Reduce bundle size:
```bash
# Enable production optimizations
ng build --prod --build-optimizer
```

---

## 🎉 POST-DEPLOYMENT TASKS

### ✅ Week 1:
- Monitor Netlify analytics
- Test with real users (doctors/nurses)
- Collect feedback
- Fix critical bugs

### ✅ Month 1:
- Add Google Analytics (track usage)
- Implement user feedback
- Write user guide
- Create video tutorial

### ✅ Month 3:
- Consider healthcare compliance audit
- Add more calculations
- Implement user authentication
- Prepare for App Store (PWA)

---

## 📞 NEXT STEPS

✅ **COMPLETED:**
- Understand deployment options
- Netlify deployment guide
- Environment configuration
- Production build process

➡️ **NEXT: PART 3 - DOCUMENTATION**
See: `COMPLETE-DOCUMENTATION.md`

---

## 📚 RESOURCES

- **Netlify Docs:** https://docs.netlify.com/
- **Angular Deployment:** https://angular.io/guide/deployment
- **.NET Core Hosting:** https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/
- **Azure SQL:** https://azure.microsoft.com/en-us/products/azure-sql/database/

**🎉 YOUR TPN CALCULATOR WILL BE LIVE SOON!**
