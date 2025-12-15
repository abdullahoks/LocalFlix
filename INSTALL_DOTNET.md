# ⚠️ .NET SDK Installation Required

## The Problem
The `dotnet` command is not recognized because **.NET SDK is not installed** on your system.

## Solution: Install .NET SDK

### Step 1: Download .NET SDK
1. Visit: **https://dotnet.microsoft.com/download**
2. Click **"Download .NET SDK"** (recommended: .NET 8.0 or later)
3. Choose **Windows x64** installer
4. Run the downloaded installer

### Step 2: Install
1. Run the `.exe` file you downloaded
2. Follow the installation wizard
3. Accept the license agreement
4. Click "Install"
5. Wait for installation to complete (2-3 minutes)

### Step 3: Verify Installation
1. **Close and reopen** your PowerShell/Terminal (important!)
2. Run this command to verify:
   ```powershell
   dotnet --version
   ```
3. You should see a version number like `8.0.x`

### Step 4: Run Your Netflix Player
Once .NET is installed, run these commands **one at a time**:

```powershell
cd "C:\Users\Abdullah OK\Desktop\VsCode\Netflix\NetflixPlayer"
dotnet restore
dotnet run
```

Then open your browser to: **http://localhost:5000**

---

## Quick Download Links

- **Windows x64**: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.404-windows-x64-installer
- **All Downloads**: https://dotnet.microsoft.com/download/dotnet/8.0

---

## After Installation

1. ✅ Close and reopen your terminal
2. ✅ Navigate to NetflixPlayer folder
3. ✅ Run `dotnet restore`
4. ✅ Run `dotnet run`
5. ✅ Open http://localhost:5000
6. ✅ Click "Scan Movies" and enjoy!

---

**Need Help?** The installation is straightforward - just download and run the installer. It will automatically configure everything for you.
