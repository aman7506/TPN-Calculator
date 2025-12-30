# GitHub Upload Guide

Hey! So you've built this TPN Calculator and now want to get it on GitHub. Here's how to do it step by step.

## Prerequisites

Make sure you have:
- Git installed on your machine
- A GitHub account (username: aman7506)
- Your code ready in: `e:\Aman Project Files\TPN_Calculations`

## Step 1: Create Repository on GitHub

First, go to GitHub and create a new repository.

1. Open https://github.com/new in your browser
2. Fill in these details:
   - **Repository name**: `TPN-Calculator` (keep it simple, with a hyphen)
   - **Description**: Something like "Total Parenteral Nutrition Calculator - A healthcare web app for IV nutrition calculations"
   - **Visibility**: Choose Public (good for portfolio) or Private (if it's confidential)
   - **Important**: Don't check any boxes for README, .gitignore, or license - we already have these locally

3. Click the green "Create repository" button

You'll see a page with setup instructions. We're not following those exactly since we already have code.

## Step 2: Get a Personal Access Token

GitHub stopped accepting passwords for git operations back in 2021. You need a token instead.

Here's how:

1. Go to https://github.com/settings/tokens
2. Click "Generate new token (classic)"
3. You might need to enter your password
4. Fill this out:
   - **Note**: Just write "TPN Calculator" or whatever helps you remember
   - **Expiration**: I'd suggest 90 days
   - **Scopes**: Check the `repo` box (this gives full repo access)
5. Scroll down and click "Generate token"
6. **IMPORTANT**: Copy this token NOW. Once you leave this page, you can't see it again.
   - It'll look like: `ghp_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx`
   - Save it in notepad or somewhere safe temporarily

## Step 3: Push Your Code

Now let's get your code online.

Open PowerShell and run these commands:

```powershell
# Navigate to your project
cd "e:\Aman Project Files\TPN_Calculations"

# Make sure Git can find the path
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

# Check if git is working
git --version
# You should see something like: git version 2.43.0

# Check your current status
git status
# Should show: "On branch main, nothing to commit"

# Push to GitHub
git push -u origin main
```

When you run that last command, it'll ask for:
- **Username**: Type `aman7506` and press Enter
- **Password**: Paste your token (the one starting with `ghp_`) and press Enter
  - Note: You won't see anything when you paste - that's normal for security

You should see something like:

```
Enumerating objects: 850, done.
Counting objects: 100% (850/850), done.
Writing objects: 100% (850/850), 3.2 MiB, done.
...
Branch 'main' set up to track remote branch 'main' from 'origin'.
```

That's it! Your code is now on GitHub.

## Step 4: Verify Upload

Go to https://github.com/aman7506/TPN-Calculator

You should see:
- Your project files and folders
- The README.md displayed nicely
- All your documentation files
- Your angular and ASP.NET code

**What should NOT be there**:
- `node_modules/` folder (way too big, around 500MB)
- `Web.config` (has your database passwords)
- `bin/` or `obj/` folders
- Large database backup files

If you DON'T see these, that's perfect! Your .gitignore is working correctly.

## Common Issues

**"git: command not found"**
- Solution: Close PowerShell and open a new window, or restart your computer

**"Authentication failed"**
- Make sure you're using the token, not your GitHub password
- Generate a new token if you lost the old one

**"Repository not found"**
- Double-check you created the repo on GitHub first
- Verify the name matches exactly: `TPN-Calculator`

**Files you don't want are uploaded**
- Check your `.gitignore` file
- You can remove files with: `git rm --cached filename`

## Making Future Updates

Once this initial push is done, updating is super easy:

```powershell
cd "e:\Aman Project Files\TPN_Calculations"

# Make your code changes...

# Check what changed
git status

# Add all changes
git add .

# Commit with a message
git commit -m "Add dark mode feature"

# Push
git push
```

No need to enter credentials again - Git will remember for this session.

## Next Steps

Now that your code is on GitHub, you might want to:

1. **Share it** - Add the link to your resume or LinkedIn
2. **Deploy it** - Get a live URL using Netlify (see DEPLOYMENT-GUIDE.md)
3. **Add topics** - On GitHub, add tags like `healthcare`, `angular`, `medical-software`
4. **Get a license** - Click "Add file" → "Create new file" → name it LICENSE and choose MIT

## Tips

- Commit often with clear messages
- Don't commit sensitive data (passwords, API keys)
- Use branches for new features: `git checkout -b feature-name`
- Pull before pushing if working with others: `git pull`

That's pretty much it! You've got your code backed up and visible on GitHub now.

---

**Questions?**
- Check the official Git docs: https://git-scm.com/doc
- GitHub guides: https://guides.github.com

Written: Dec 30, 2025
