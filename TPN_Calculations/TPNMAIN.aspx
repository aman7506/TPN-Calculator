<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TPNMAIN.aspx.cs" Inherits="TNS_Calculations.TPNMAIN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

   <!-- Toastr CSS -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet" />

<!-- jQuery (Required by Toastr) -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<!-- Toastr JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

 <script type="text/javascript">
     function validateInput(textBox) {
         var value = textBox.value;
         var errorDiv = document.getElementById('calciumValueError');

         if (value !== '0' && value !== '1') {
             errorDiv.style.display = 'block'; // Show the error message
             // Optionally clear the invalid input if you prefer
             // textBox.value = '';
         } else {
             errorDiv.style.display = 'none';  // Hide the error message if valid
         }
     }
</script>

        <script type="text/javascript">
            function IsFinalSave() {
                var userConfirmation = confirm("Are you sure you want to save the Final?");
                if (userConfirmation) {
                    // User clicked "OK", proceed with saving
                    return true;
                } else {
                    // User clicked "Cancel", do not save
                    return false;
                }
            }
        </script>

<script type="text/javascript">
    // ... (saveScrollPosition and attachSaveScroll functions as before)

    function restoreScrollPosition() {
        var savedPosition = sessionStorage.getItem('scrollPosition');
        if (savedPosition !== null) {
            window.scrollTo(0, parseInt(savedPosition));
            sessionStorage.removeItem('scrollPosition');
        }
    }

    // TryDOMContentLoaded instead of window.onload
    document.addEventListener('DOMContentLoaded', restoreScrollPosition);
</script>


<script type="text/javascript">
    document.addEventListener("keydown", function (e) {
        // If Enter is pressed inside a TextBox or TextArea
        if (e.key === "Enter" &&
            (e.target.tagName === "INPUT" && e.target.type === "text")) {
            e.preventDefault();
            return false;
        }
    });
</script>

<script type="text/javascript">
    function PrintBabyDetails() {
        // Baby Details
        var uhid = document.getElementById('<%= TextUHID.ClientID %>').value;
        var babyName = document.getElementById('<%= TextBabyName.ClientID %>').value;
        var dobRaw = document.getElementById('<%= TextDOB.ClientID %>').value;
      /////  var birthTime = document.getElementById('<%= TextBirthTime.ClientID %>').value;
        var age = document.getElementById('<%= TextAge.ClientID %>').value;
        var ageDays = age.match(/(\d+)d/);
        age = ageDays ? ageDays[1] + (ageDays[1] == "1" ? " Day" : " Days") : "";

        var TPN = document.getElementById('<%= TPNDAYText.ClientID %>').value;

        var gender = document.getElementById('<%= DropdownGender.ClientID %>').options[document.getElementById('<%= DropdownGender.ClientID %>').selectedIndex].text;
        var dobParts = dobRaw.split("-");
        var dob = dobParts.length === 3 ? dobParts[2] + "/" + dobParts[1] + "/" + dobParts[0] : "";

        // Dosing & Fluid
        var dosingWeight = document.getElementById('<%= TextDosingWeight.ClientID %>').value;
        var tfr = document.getElementById('<%= TextTFR.ClientID %>').value;
        var feed = document.getElementById('<%= TextFeed.ClientID %>').value;
        var ivm = document.getElementById('<%= TextIVM.ClientID %>').value;
        var a = document.getElementById('<%= TextA.ClientID %>').value;
        var l = document.getElementById('<%= TextL.ClientID %>').value;
        var g = document.getElementById('<%= TextG.ClientID %>').value;
        var na = document.getElementById('<%= TextNa.ClientID %>').value;
        var k = document.getElementById('<%= TextK.ClientID %>').value;
        var ca = document.getElementById('<%= TextCa.ClientID %>').value;
        var mg = document.getElementById('<%= TextMg.ClientID %>').value;

        var baseValue = document.getElementById('<%= DropdownBaseSolution.ClientID %>').value;
        var concValue = document.getElementById('<%= DropdownConcSolution.ClientID %>').value;
     
        var baseSolutionText;
        var baseSolution50mlText;

        if (baseValue == "0") { // Corrected condition: "0" for 10%
            baseSolutionText = "10% Dextrose (per)";
            baseSolution50mlText = "10% Dextrose (50 ml)";
          

        } else if (baseValue == "1") { // Corrected condition: "1" for 5%
            baseSolutionText = "5% Dextrose (per)";
            baseSolution50mlText = "5% Dextrose (50 ml)";
            console.log("baseSolutionText: for 5%", baseValue);
            console.log("baseSolution50mlText: for 5%:", concValue);
        } else {
            baseSolutionText = "";
            baseSolution50mlText = "";
        }

        var concSolutionText;
        var concSolution50mlText;

        if (concValue === "1") {
            concSolutionText = "25% Dextrose (per)";
            concSolution50mlText = "25% Dextrose (50 ml)";
        } else if (concValue === "0") {
            concSolutionText = "50% Dextrose (per)"; // Assuming value "2" corresponds to 50% MgSO4 based on your table
            concSolution50mlText = "50% Dextrose (50 ml)";
        } else {
            concSolutionText = "";
            concSolution50mlText = "";
        }

        var dextroseBasePer = document.getElementById('<%= TextDextroseBasePer.ClientID %>').value;
        var dextroseBase50ml = document.getElementById('<%= TextDextroseBase50ml.ClientID %>').value;
        var dextroseConcPer = document.getElementById('<%= TextDextroseConcPer.ClientID %>').value;
        var dextroseConc50ml = document.getElementById('<%= TextDextroseConc50ml.ClientID %>').value;

        // IVM Volume
        var n5 = document.getElementById('<%= TextN5.ClientID %>').value;
        var n2 = document.getElementById('<%= TextN2.ClientID %>').value;
        var ns = document.getElementById('<%= TextNS.ClientID %>').value;
        var dex10 = document.getElementById('<%= TextDex10.ClientID %>').value;
        var typeOfOralFeed = document.getElementById('<%= DropdownTypeOfOralFeed.ClientID %>').options[document.getElementById('<%= DropdownTypeOfOralFeed.ClientID %>').selectedIndex].text;
        var preNanStrength = document.getElementById('<%= DropdownPreNanStrength.ClientID %>').options[document.getElementById('<%= DropdownPreNanStrength.ClientID %>').selectedIndex].text;
        var po4 = document.getElementById('<%= TextPo4.ClientID %>').value;
        var calciumViaTPN = document.getElementById('<%= TextCalciumViaTPN.ClientID %>').value;
        var overfillFactor = document.getElementById('<%= TextOverfillFactor.ClientID %>').value;
        var sodiumSource = document.getElementById('<%= DropdownSodiumSource.ClientID %>').options[document.getElementById('<%= DropdownSodiumSource.ClientID %>').selectedIndex].text;
        var celcelInput = document.getElementById('<%= TextCelcelInput.ClientID %>').value;

        // Syringe 1
        var lipid = document.getElementById('<%= TextLipid.ClientID %>').value;
        var mvi = document.getElementById('<%= TextMVI.ClientID %>').value;
        var celcel = document.getElementById('<%= TextCelcel.ClientID %>').value;
        var syringe1Total = document.getElementById('<%= TextSyringe1Total.ClientID %>').value;

        // Syringe 2
        var aminovenPer = document.getElementById('<%= TextAminovenPer.ClientID %>').value;
        var aminoven50ml = document.getElementById('<%= TextAminoven50ml.ClientID %>').value;
        var naclPer = document.getElementById('<%= TextNaclPer.ClientID %>').value;
        var nacl50ml = document.getElementById('<%= TextNacl50ml.ClientID %>').value;
        var kclPer = document.getElementById('<%= TextKclPer.ClientID %>').value;
        var kcl50ml = document.getElementById('<%= TextKcl50ml.ClientID %>').value;
        var calciumPer = document.getElementById('<%= TextCalciumPer.ClientID %>').value;
        var calcium50ml = document.getElementById('<%= TextCalcium50ml.ClientID %>').value;
        var mgso4Per = document.getElementById('<%= TextMgso4Per.ClientID %>').value;
        var mgso450ml = document.getElementById('<%= TextMgso450ml.ClientID %>').value;

        var totalVolume = document.getElementById('<%= TextTotalVolume.ClientID %>').value;
        var totalPer50 = document.getElementById('<%= TextTotalPer50.ClientID %>').value;

        // Fluid Calcium
        var potPhos = document.getElementById('<%= TextPotPhos.ClientID %>').value;
        var calcium10 = document.getElementById('<%= TextCalcium10.ClientID %>').value;


        // Nutritional Requirements
        var nutriTfr = document.getElementById('<%= TextNutriationtfr.ClientID %>').value;
        var tfv = document.getElementById('<%= TextTfv.ClientID %>').value;
        var feeds = document.getElementById('<%= TextFeeds.ClientID %>').value;
        var ivfMlKg = document.getElementById('<%= TextivfMlKg.ClientID %>').value;
        var ivfMl = document.getElementById('<%= TextivfMl.ClientID %>').value;
        var tpnFluidMl = document.getElementById('<%= TextTpnFluidMl.ClientID %>').value;
        var tpnGlucoseG = document.getElementById('<%= TextTpnGlucoseG.ClientID %>').value;
        var fluidForGlucose = document.getElementById('<%= TextFluidForGlucose.ClientID %>').value;
        var osmolarity = document.getElementById('<%= TextOsmolarity.ClientID %>').value;
        var dextrose = document.getElementById('<%= TextDextrose.ClientID %>').value;
        var cnr = document.getElementById('<%= TextCnr.ClientID %>').value;
        var caloriesForToday = document.getElementById('<%= TextCaloriesForToday.ClientID %>').value;
        var proteinsForToday = document.getElementById('<%= TextProteinsForToday.ClientID %>').value;
        var naInIvm = document.getElementById('<%= TextNaInIvm.ClientID %>').value;
        var glucoseInIvm = document.getElementById('<%= TextGlucoseInIvm.ClientID %>').value;
        var kInPotphos = document.getElementById('<%= TextKInPotphos.ClientID %>').value;
        var printableContent = `
        <html>
        <head>
            <title>Baby Details</title>
            <style>
            @page {
                size: A4;
            }
            @media print {
    * {
        -webkit-print-color-adjust: exact !important; /* For Chrome, Safari */
        print-color-adjust: exact !important;        /* For Firefox, Edge */
    }
}

                body {
                    font-family: Arial, sans-serif;
                    margin: 10px;
                }
                .header {
                    text-align: center;
                    margin-bottom: 30px;
                }
                 .header img.logo-left {
            position: absolute;
            top: 0;
            left: 0;
            width: 80px;
        height: 60px;
        }
        .header img.logo-right {
            position: absolute;
            top: 0;
            right: 0;
            width: 80px;
        height: 60px;
        }
                .header h4 {
                    margin: 0;
                    font-size: 24px;
                }
               .header small {
        display: block;
        margin-top: 5px;
        font-size: 18px; /* Increased font size for address */
        color: #555;
    }
                h2 {
                    color: #007bff;
                    border-bottom: 2px solid #007bff;
                    padding-bottom: 5px;
                    margin-top: 40px;
                }
                table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 15px;
                    font-size: 14px; /* Reduced from default to slightly smaller */
                }
                td, th {
                    border: 1px solid #ccc;
                    padding: 6px 10px;
                    vertical-align: top;
                }
                th {
                    background-color: #f5f5f5;
                    text-align: left;
                }
                 .bold {
                    font-weight: bold;
                }
                .page-break {
                    page-break-before: always;
                }
            </style>
        </head>
        <body>
             <div class="header">
        <img src="Image/logo.png" class="logo-left" alt="Left Logo" />
        <h4>SRI BALAJI ACTION MEDICAL INSTITUTE</h4>
        <small>A - 4, Paschim Vihar, <strong>New Delhi</strong> - 110063</small>
                    <img src="Image/nabh.png" class="logo-right" alt="Right Logo" />

    </div>

            <h2>Baby Details</h2>
            <table>
                <tr><th>UHID No</th><td>${uhid}</td><th>Baby Name</th><td>${babyName}</td></tr>
                <tr><th>DOB</th><td>${dob}</td><th>Gender</th><td>${gender}</td></tr>
                     <tr><th>Age</th><td>${age}</td><th>Days of TPN</th><td>${TPN}</td></tr>

            </table>

            <h2>Dosing & Fluid Details</h2>
            <table>
                <tr><th>Dosing wt (kg)</th><td>${dosingWeight}</td><th>TFR (ml/kg)</th><td>${tfr}</td></tr>
                <tr><th>Feed (ml/kg)</th><td>${feed}</td><th>IVM (ml)</th><td>${ivm}</td></tr>
                <tr><th>A (g/kg/day)</th><td>${a}</td><th>L (g/kg/day)</th><td>${l}</td></tr>
                <tr><th>G (mg/kg/min)</th><td>${g}</td><th>Na (mEq/kg/d)</th><td>${na}</td></tr>
                <tr><th>K (mEq/kg/d)</th><td>${k}</td><th>Ca (mg/kg/d)</th><td>${ca}</td></tr>
                <tr><th>Mg (mEq/kg/d)</th><td>${mg}</td><th>Base Solution</th><td>${baseSolutionText}</td></tr>
                <tr><th>Conc. Solution</th><td>${concSolutionText}</td><td></td><td></td></tr>
            </table>

            <h2>IVM Volume Details</h2>
            <table>
                <tr><th>N/5 (mL)</th><td>${n5}</td><th>N/2 (mL)</th><td>${n2}</td></tr>
                <tr><th>NS (mL)</th><td>${ns}</td><th>10% Dex (mL)</th><td>${dex10}</td></tr>
                <tr><th>Type of Oral Feed</th><td>${typeOfOralFeed}</td><th>PreNan Strength</th><td>${preNanStrength}</td></tr>
                <tr><th>PO4 (mg/kg/d)</th><td>${po4}</td><th>Calcium via TPN</th><td>${calciumViaTPN}</td></tr>
                <tr><th>Overfill Factor</th><td>${overfillFactor}</td><th>Sodium Source</th><td>${sodiumSource}</td></tr>
                <tr><th>Celcel Input</th><td>${celcelInput}</td><td></td><td></td></tr>
            </table>


                <h2>Fluid Calcium</h2>
            <table>
                <tr><th>PotPhos (PO4)</th><td>${potPhos}</td><th>Total 10% Calcium</th><td>${calcium10}</td></tr>
            </table>
            <div class="page-break"></div>

            <h2>Syringe 1</h2>
            <table>
                <tr><th>20% Lipid (ml)</th><td>${lipid}</td><th>MVI (ml)</th><td>${mvi}</td></tr>
                <tr><th>Celcel (ml)</th><td>${celcel}</td><th>Syringe 1 Total (ml/hr)</th><td>${syringe1Total}</td></tr>
            </table>

                <h2>Syringe 2</h2>
           
           <table border="1" cellpadding="6" cellspacing="0" style="width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;">
    <thead>
        <tr>
            <th colspan="2" style="text-align:left;">Total</th>
            <th colspan="2" style="text-align:left;">Per (50 ml)</th>
        </tr>
    </thead>
    <tbody>
        <tr><th>10% Aminoven (per)</th><td>${aminovenPer}</td><th>10% Aminoven</th><td>${aminoven50ml}</td></tr>
        <tr><th>3% NaCl (per)</th><td>${naclPer}</td><th>3% NaCl</th><td>${nacl50ml}</td></tr>
        <tr><th>KCl (per)</th><td>${kclPer}</td><th>KCl</th><td>${kcl50ml}</td></tr>
        <tr><th>10% Calcium Gluconate (per)</th><td>${calciumPer}</td><th>10% Calcium Gluconate</th><td>${calcium50ml}</td></tr>
        <tr><th>50% MgSO₄ (per)</th><td>${mgso4Per}</td><th>50% MgSO4</th><td>${mgso450ml}</td></tr>
        <tr><th>${baseSolutionText}</th><td>${dextroseBasePer}</td><th>${baseSolution50mlText}</th><td>${dextroseBase50ml}</td></tr>
        <tr><th>${concSolutionText}</th><td>${dextroseConcPer}</td><th>${concSolution50mlText}</th><td>${dextroseConc50ml}</td></tr>
        <tr class="bold"><th>Total Volume (ml)</th><td>${totalVolume}</td><th>Total Per Hour</th><td>${totalPer50}</td></tr>
    </tbody>
</table>


                <h2>Nutritional Requirements</h2>
            <div>
                <table>
                    <tr><th>TFR (ml/kg)</th><td>${nutriTfr}</td><th>TFV (ml)</th><td>${tfv}</td></tr>
                    <tr><th>Feeds (ml)</th><td>${feeds}</td><th>IVF (ml/kg)</th><td>${ivfMlKg}</td></tr>
                    <tr><th>IVF (ml)</th><td>${ivfMl}</td><th>TPN fluid (ml)</th><td>${tpnFluidMl}</td></tr>
                    <tr><th>TPN Glucose (g)</th><td>${tpnGlucoseG}</td><th>Fluid for Glucose</th><td>${fluidForGlucose}</td></tr>
                    <tr><th>Osmolarity</th><td>${osmolarity}</td><th>Dextrose %</th><td>${dextrose}</td></tr>
                    <tr><th>CNR</th><td>${cnr}</td><th>Calories for today</th><td>${caloriesForToday}</td></tr>
                    <tr><th>Proteins for today</th><td>${proteinsForToday}</td><th>Na in IVM (mEq/kg/d)</th><td>${naInIvm}</td></tr>
                    <tr><th>Glucose in IVM (g)</th><td>${glucoseInIvm}</td><th>K in Potphos (mEq/kg/d)</th><td>${kInPotphos}</td></tr>
                </table>
            </div>

            <br>
<div style="display: flex; justify-content: space-between; margin-top: 50px; font-size: 16px;">
    <div>
        <strong></strong> ${new Date().toLocaleString()}
    </div>
    <div style="text-align: right;">
        <strong>Doctor's Signature:</strong> ___________________
    </div>
</div>
        </body>
        </html>
        `;

        var printWindow = window.open('', '_blank', 'height=900,width=800');
        printWindow.document.write(printableContent);
        printWindow.document.close();
        printWindow.onload = function () {
            printWindow.focus();
            printWindow.print();
        };
    }
</script>










    <style>
.toast-success {
    background-color: #E91E63 !important; /* Pinkish Red */
    color: #fff !important;
}


        .hospital-logo {
            width: 80px;
        }
        .nabh-logo {
            width: 80px;
        }
        .hospital-name {
            font-size: 20px;
            font-weight: bold;
        }
        .hospital-address {
            font-size: 14px;
        }
        .borderBox {
            border: 1px solid #ccc;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 20px;
        }
        .section-title {
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 15px;
        }
        .form-group label {
            font-weight: 500;
            display: block;
            margin-bottom: 5px;
        }
        .form-control:read-only {
            background-color: #f8f9fa;
        }
        .btn-group .btn {
            flex: 1;
        }
        .equal-height {
            display: flex;
            flex-wrap: wrap;
        }
        .equal-height > div {
            display: flex;
            flex-direction: column;
            height: 100%;
        }

          .text-danger {
        color: red;
        font-size: 0.9em; /* Adjust size as needed */
        margin-top: 5px; /* Add some spacing */
    }

                   .like-button {
  display: inline-block;
  padding: 8px 20px;
  background-color: #007BFF;
  color: white;
  text-align: center;
  text-decoration: none;
  border-radius: 5px;
  font-family: Arial, sans-serif;
  font-size: 16px;
  transition: background-color 0.3s;
}

.like-button:hover {
  background-color: #0056b3;
}

.like-button:active {
  background-color: #004080;
}

.grandtotal-label {
    color: #003366;                /* Rich deep blue */
    font-weight: 800;             /* Extra bold */
    font-size: 1.15rem;           /* Slightly larger */
    text-transform: uppercase;    /* Optional: makes it stand out more */
}

.grandtotal-textbox {
    background-color: #d9edf7 !important; /* Light sky blue */
    color: #003366;                       /* Deep blue text */
    font-weight:bold;                  /* Extra bold number */
    border: 2px solid #007bff;           /* Bootstrap blue border */
    border-radius: 6px;
    font-size: 1.1rem;                   /* Larger text */
    box-shadow: 0 0 8px rgba(0, 123, 255, 0.25); /* Glow effect */
}

 .is-invalid {
        border-color: red !important;
        background-color: #ffe5e5 !important;
    }
 .custom-btn {
        font-weight: 500;
        padding: 8px 20px;
        font-size: 16px;
        border-radius: 8px;
        border: none;
        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
        transition: background-color 0.3s ease, transform 0.2s ease;
    }

    .custom-btn:hover {
        transform: translateY(-2px);
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
    }




    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <asp:Panel ID="PanelForm" runat="server" CssClass="card p-4">
            <div class="borderBox mb-4">


<h2 class="section-title" style="border-bottom: 2px solid #007bff; padding-bottom: 8px; color: #007bff; font-size: 1.5rem; font-weight: 500; margin-bottom: 15px;">Baby Details</h2>                



                <div class="row g-3">
                    <div class="col-md-3 form-group">
                        <asp:Label ID="LabelUHID" runat="server" Text="UHID No" AssociatedControlID="TextUHID"></asp:Label>
                        <asp:TextBox ID="TextUHID" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 form-group">
                        <asp:Label ID="LabelBabyName" runat="server" Text="Baby Name" AssociatedControlID="TextBabyName"></asp:Label>
                        <asp:TextBox ID="TextBabyName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                   <div class="col-md-3 form-group">
                        <asp:Label ID="LabelDOB" runat="server" Text="DOB" AssociatedControlID="TextDOB"></asp:Label>
                        <asp:TextBox ID="TextDOB" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>
                    </div>
                    <div class="col-md-3 form-group">
                        <asp:Label ID="LabelBirthTime" runat="server" Text="Birth Time" AssociatedControlID="TextBirthTime" Visible="false"></asp:Label>
                        <asp:TextBox ID="TextBirthTime" runat="server" CssClass="form-control" TextMode="Time"  Visible="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3 form-group">
                        <asp:Label ID="LabelAge" runat="server" Text="Age" AssociatedControlID="TextAge"></asp:Label>
                        <asp:TextBox ID="TextAge" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3 form-group">
                        <asp:Label ID="LabelGender" runat="server" Text="Gender" AssociatedControlID="DropdownGender"></asp:Label>
                        <asp:DropDownList ID="DropdownGender" runat="server" CssClass="form-select">
                            <asp:ListItem Value="male">Male</asp:ListItem>
                            <asp:ListItem Value="female">Female</asp:ListItem>
                            <asp:ListItem Value="others">Others</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                         <div class="col-md-3 form-group">
         <asp:Label ID="TPNDAYlabel" runat="server" Text="Days of TPN" AssociatedControlID="TPNDAYText"></asp:Label>
         <asp:TextBox ID="TPNDAYText" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
     </div>
                </div>
            </div>
                       <div class="row mt-4 equal-height">
    <div class="col-lg-6 col-sm-12 col-md-6 mb-4">
        <div class="borderBox">
            <h2 class="section-title" style="border-bottom: 2px solid #007bff; padding-bottom: 8px; color: #007bff; font-size: 1.5rem; font-weight: 500; margin-bottom: 15px;">
                Dosing & Fluid Details</h2>
            <div class="row g-3">
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelDosingWeight" runat="server" Text="Dosing wt (kg)" AssociatedControlID="TextDosingWeight"></asp:Label>
                    <asp:TextBox ID="TextDosingWeight" runat="server" CssClass="form-control" Text="0" ></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelTFR" runat="server" Text="TFR (ml/kg)" AssociatedControlID="TextTFR"></asp:Label>
                    <asp:TextBox ID="TextTFR" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelFeed" runat="server" Text="Feed (ml/kg)" AssociatedControlID="TextFeed"></asp:Label>
                    <asp:TextBox ID="TextFeed" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelIVM" runat="server" Text="IVM (ml)" AssociatedControlID="TextIVM"></asp:Label>
                    <asp:TextBox ID="TextIVM" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelA" runat="server" Text="A (g/kg/day)" AssociatedControlID="TextA"></asp:Label>
                    <asp:TextBox ID="TextA" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelL" runat="server" Text="L (g/kg/day)" AssociatedControlID="TextL"></asp:Label>
                    <asp:TextBox ID="TextL" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelG" runat="server" Text="G (mg/kg/min)" AssociatedControlID="TextG"></asp:Label>
                    <asp:TextBox ID="TextG" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelNa" runat="server" Text="Na (mEq/kg/d)" AssociatedControlID="TextNa"></asp:Label>
                    <asp:TextBox ID="TextNa" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelK" runat="server" Text="K (mEq/kg/d)" AssociatedControlID="TextK"></asp:Label>
                    <asp:TextBox ID="TextK" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelCa" runat="server" Text="Ca (mg/kg/d)" AssociatedControlID="TextCa"></asp:Label>
                    <asp:TextBox ID="TextCa" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelMg" runat="server" Text="Mg (mEq/kg/d)" AssociatedControlID="TextMg"></asp:Label>
                    <asp:TextBox ID="TextMg" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelBaseSolution" runat="server" Text="Dextrose" AssociatedControlID="DropdownBaseSolution"></asp:Label>
                    <asp:DropDownList ID="DropdownBaseSolution" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="DropdownBaseSolution_SelectedIndexChanged" >
                        <asp:ListItem Value="1">5%</asp:ListItem>
                        <asp:ListItem Value="0">10%</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelConcSolution" runat="server" Text="Conc. Solution" AssociatedControlID="DropdownConcSolution"></asp:Label>
                    <asp:DropDownList ID="DropdownConcSolution" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="DropdownConcSolution_SelectedIndexChanged" >
                        <asp:ListItem Value="1">25%</asp:ListItem>
                        <asp:ListItem Value="0">50%</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-6 col-sm-12 col-md-6 mb-4">
        <div class="borderBox">
            <h2 class="section-title" style="border-bottom: 2px solid #007bff; padding-bottom: 8px; color: #007bff; font-size: 1.5rem; font-weight: 500; margin-bottom: 15px;">
                IVM Volume Details</h2>
            <div class="row g-3">
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelN5" runat="server" Text="N/5 (mL)" AssociatedControlID="TextN5"></asp:Label>
                    <asp:TextBox ID="TextN5" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelN2" runat="server" Text="N/2 (mL)" AssociatedControlID="TextN2"></asp:Label>
                    <asp:TextBox ID="TextN2" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelNS" runat="server" Text="NS (mL)" AssociatedControlID="TextNS"></asp:Label>
                    <asp:TextBox ID="TextNS" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelDex10" runat="server" Text="10% Dex (mL)" AssociatedControlID="TextDex10"></asp:Label>
                    <asp:TextBox ID="TextDex10" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelTypeOfOralFeed" runat="server" Text="Type of oral feed" AssociatedControlID="DropdownTypeOfOralFeed"></asp:Label>
                    <asp:DropDownList ID="DropdownTypeOfOralFeed" runat="server" CssClass="form-select" >
                        <asp:ListItem Value="EBM/PDHM">EBM/PDHM</asp:ListItem>
                        <asp:ListItem Value="NPO">NPO</asp:ListItem>
                        <asp:ListItem Value="Formula">Formula</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelPreNanStrength" runat="server" Text="PreNan Strength" AssociatedControlID="DropdownPreNanStrength"></asp:Label>
                    <asp:DropDownList ID="DropdownPreNanStrength" runat="server" CssClass="form-select" >
                        <asp:ListItem Value="None">None</asp:ListItem>
                        <asp:ListItem Value="Quarter">Quarter</asp:ListItem>
                        <asp:ListItem Value="Half">Half</asp:ListItem>
                        <asp:ListItem Value="Full">Full</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelPo4" runat="server" Text="PO4 (mg/kg/d)" AssociatedControlID="TextPo4"></asp:Label>
                    <asp:TextBox ID="TextPo4" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelCalciumViaTPN" runat="server" Text="Calcium via TPN" AssociatedControlID="TextCalciumViaTPN"></asp:Label>
                    <asp:TextBox ID="TextCalciumViaTPN" runat="server" CssClass="form-control"  onkeyup="validateInput(this)" Text="0"></asp:TextBox>
                    <div id="calciumValueError" class="text-danger" style="display:none;">Please enter only 0 or 1.</div>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelOverfillFactor" runat="server" Text="Overfill factor" AssociatedControlID="TextOverfillFactor"></asp:Label>
                    <asp:TextBox ID="TextOverfillFactor" runat="server" CssClass="form-control" Text="1"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelSodiumSource" runat="server" Text="Sodium source" AssociatedControlID="DropdownSodiumSource"></asp:Label>
                    <asp:DropDownList ID="DropdownSodiumSource" runat="server" CssClass="form-select" OnSelectedIndexChanged="DropdownSodiumSource_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="3% NaCl">3% NaCl</asp:ListItem>
                        <asp:ListItem Value="CRL">Conc. RL</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="LabelCelcelInput" runat="server" Text="Celcel Input" AssociatedControlID="TextCelcelInput"></asp:Label>
                    <asp:TextBox ID="TextCelcelInput" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                </div>
                    <div class="col-md-12">
        <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>

            </div>
        </div>
    </div>
</div>
            <div class="borderBox mb-4">
                <h2 class="section-title" style="border-bottom: 2px solid #007bff; padding-bottom: 8px; color: #007bff; font-size: 1.5rem; font-weight: 500; margin-bottom: 15px;" >Syringe 1</h2>
                <div class="row g-3">
                    <div class="col-md-4 form-group">
                        <asp:Label ID="LabelLipid" runat="server" Text="20% Lipid (ml)" AssociatedControlID="TextLipid"></asp:Label>
                        <asp:TextBox ID="TextLipid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label ID="LabelMVI" runat="server" Text="MVI (ml)" AssociatedControlID="TextMVI"></asp:Label>
                        <asp:TextBox ID="TextMVI" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label ID="LabelCelcel" runat="server" Text="Celcel (ml)" AssociatedControlID="TextCelcel"></asp:Label>
                        <asp:TextBox ID="TextCelcel" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label ID="LabelSyringe1Total" runat="server" Text="Syringe 1 Total (ml)/hr" AssociatedControlID="TextSyringe1Total"></asp:Label>
                        <asp:TextBox ID="TextSyringe1Total" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6 col-sm-12 col-md-6 mb-4">
                    <div class="borderBox">
                        <h2 class="section-title" style="border-bottom: 2px solid #007bff; padding-bottom: 8px; color: #007bff; font-size: 1.5rem; font-weight: 500; margin-bottom: 15px;">
                            Syringe 2</h2>
                        <div class="row g-3">
                            <div class="col-6 form-group">
                                                             <h5 class="mb-2">Total</h5>
                                <br />
                                <asp:Label ID="LabelAminovenPer" runat="server" Text="10% Aminoven (per)" AssociatedControlID="TextAminovenPer"></asp:Label>
                                <asp:TextBox ID="TextAminovenPer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
    <h5 class="mb-2">Per (50 ml)</h5>
                                         <br />
                                <asp:Label ID="LabelAminoven50ml" runat="server" Text="10% Aminoven" AssociatedControlID="TextAminoven50ml"></asp:Label>
                                <asp:TextBox ID="TextAminoven50ml" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelNaclPer" runat="server" Text="3% NaCl (per)" AssociatedControlID="TextNaclPer"></asp:Label>
                                <asp:TextBox ID="TextNaclPer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelNacl50ml" runat="server" Text="3% NaCl" AssociatedControlID="TextNacl50ml"></asp:Label>
                                <asp:TextBox ID="TextNacl50ml" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelKclPer" runat="server" Text="KCl (per)" AssociatedControlID="TextKclPer"></asp:Label>
                                <asp:TextBox ID="TextKclPer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelKcl50ml" runat="server" Text="KCl" AssociatedControlID="TextKcl50ml"></asp:Label>
                                <asp:TextBox ID="TextKcl50ml" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelCalciumPer" runat="server" Text="Calcium Gluconate(per)" AssociatedControlID="TextCalciumPer"></asp:Label>
                                <asp:TextBox ID="TextCalciumPer" runat="server" CssClass="form-control" ReadOnly="true"  ></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelCalcium50ml" runat="server" Text="Calcium Gluconate" AssociatedControlID="TextCalcium50ml"></asp:Label>
                                <asp:TextBox ID="TextCalcium50ml" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelMgso4Per" runat="server" Text="50% MgSO4 (per)" AssociatedControlID="TextMgso4Per"></asp:Label>
                                <asp:TextBox ID="TextMgso4Per" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelMgso450ml" runat="server" Text="50% MgSO4" AssociatedControlID="TextMgso450ml"></asp:Label>
                                <asp:TextBox ID="TextMgso450ml" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                             <div class="col-6 form-group">
                                 <asp:Label ID="LabelDextroseBasePer" runat="server" Text="5% Dextrose (per)"></asp:Label>
                                  <asp:TextBox ID="TextDextroseBasePer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelDextroseBase50ml" runat="server" Text="5% Dextrose"></asp:Label>
                                <asp:TextBox ID="TextDextroseBase50ml" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelDextroseConcPer" runat="server" Text="25% Dextrose (per)"></asp:Label>
                                 <asp:TextBox ID="TextDextroseConcPer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelDextroseConc50ml" runat="server" Text="25% Dextrose"></asp:Label>
                                <asp:TextBox ID="TextDextroseConc50ml" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>

<div class="col-6 form-group">
    <asp:Label ID="LabelTotalVolume" runat="server" 
        Text="Total Volume (ml)" 
        CssClass="grandtotal-label" 
        AssociatedControlID="TextTotalVolume"></asp:Label>
    <asp:TextBox ID="TextTotalVolume" runat="server" 
        CssClass="form-control grandtotal-textbox" 
        ReadOnly="true"></asp:TextBox>
</div>

<div class="col-6 form-group">
    <asp:Label ID="LabelTotalPer50" runat="server" 
        Text="Total Per Hour" 
        CssClass="grandtotal-label" 
        AssociatedControlID="TextTotalPer50"></asp:Label>
    <asp:TextBox ID="TextTotalPer50" runat="server" 
        CssClass="form-control grandtotal-textbox" 
        ReadOnly="true"></asp:TextBox>
</div>



                        </div>
                    </div>
                </div>

                <div class="col-lg-6 col-sm-12 col-md-6 mb-4">
                    <div class="borderBox">
                        <h2 class="section-title" style="border-bottom: 2px solid #007bff; padding-bottom: 8px; color: #007bff; font-size: 1.5rem; font-weight: 500; margin-bottom: 15px;">
                            Fluid Calcium</h2>
                        <div class="row g-3">
                          
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelPotPhos" runat="server" Text="PotPhos (PO4)" AssociatedControlID="TextPotPhos"></asp:Label>
                                <asp:TextBox ID="TextPotPhos" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 form-group">
                                <asp:Label ID="LabelCalcium10" runat="server" Text="Total 10% Calcium" AssociatedControlID="TextCalcium10"></asp:Label>
                                <asp:TextBox ID="TextCalcium10" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

      


 <div class="borderBox mt-4">
    <h2 class="section-title" style="border-bottom: 2px solid #007bff; padding-bottom: 8px; color: #007bff; font-size: 1.5rem; font-weight: 500; margin-bottom: 15px;">
        Nutritional Requirements</h2>
    <div class="row g-3">
        <div class="col-3 form-group">
            <asp:Label ID="LabelNutriationtfr" runat="server" Text="TFR (ml/kg)" AssociatedControlID="TextNutriationtfr"></asp:Label>
            <asp:TextBox ID="TextNutriationtfr" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
             <asp:Label ID="LabelTfv" runat="server" Text="TFV (ml)" AssociatedControlID="TextTfv"></asp:Label>
            <asp:TextBox ID="TextTfv" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelFeeds" runat="server" Text="Feeds (ml)" AssociatedControlID="TextFeeds"></asp:Label>
            <asp:TextBox ID="TextFeeds" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelivfMlKg" runat="server" Text="IVF (ml/kg)" AssociatedControlID="TextivfMlKg"></asp:Label>
            <asp:TextBox ID="TextivfMlKg" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
             <asp:Label ID="LabelivfMl" runat="server" Text="IVF (ml)" AssociatedControlID="TextivfMl"></asp:Label>
            <asp:TextBox ID="TextivfMl" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelTpnFluidMl" runat="server" Text="TPN fluid (ml)" AssociatedControlID="TextTpnFluidMl"></asp:Label>
            <asp:TextBox ID="TextTpnFluidMl" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelTpnGlucoseG" runat="server" Text="TPN Glucose (g)" AssociatedControlID="TextTpnGlucoseG"></asp:Label>
            <asp:TextBox ID="TextTpnGlucoseG" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelFluidForGlucose" runat="server" Text="Fluid for Glucose" AssociatedControlID="TextFluidForGlucose"></asp:Label>
            <asp:TextBox ID="TextFluidForGlucose" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelOsmolarity" runat="server" Text="Osmolarity (Sy1 + Sy2)" AssociatedControlID="TextOsmolarity"></asp:Label>
            <asp:TextBox ID="TextOsmolarity" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelDextrose" runat="server" Text="Dextrose %" AssociatedControlID="TextDextrose"></asp:Label>
            <asp:TextBox ID="TextDextrose" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelCnr" runat="server" Text="CNR" AssociatedControlID="TextCnr"></asp:Label>
            <asp:TextBox ID="TextCnr" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelCaloriesForToday" runat="server" Text="Calories for today" AssociatedControlID="TextCaloriesForToday"></asp:Label>
            <asp:TextBox ID="TextCaloriesForToday" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelProteinsForToday" runat="server" Text="Proteins for today" AssociatedControlID="TextProteinsForToday"></asp:Label>
            <asp:TextBox ID="TextProteinsForToday" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelNaInIvm" runat="server" Text="Na in IVM (mEq/kg/d)" AssociatedControlID="TextNaInIvm"></asp:Label>
            <asp:TextBox ID="TextNaInIvm" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelGlucoseInIvm" runat="server" Text="Glucose in IVM (g)" AssociatedControlID="TextGlucoseInIvm"></asp:Label>
            <asp:TextBox ID="TextGlucoseInIvm" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-3 form-group">
            <asp:Label ID="LabelKInPotphos" runat="server" Text="K in Potphos (mEq/kg/d)" AssociatedControlID="TextKInPotphos"></asp:Label>
            <asp:TextBox ID="TextKInPotphos" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
</div>
       
<div class="button-container">
       <asp:Button ID="ButtonCalculateSave" runat="server" Text="Calculate &amp; Save"
       CssClass="btn btn-info me-3 custom-btn" OnClick="ButtonCalculateSave_Click" Visible="false" />

    <asp:Button ID="ButtonDraftSave" runat="server" Text="Calculate &amp; Draft"
        CssClass="btn btn-primary me-3 custom-btn" OnClick="ButtonDraftSave_Click" />

    <asp:Button ID="ButtonFinalSave" runat="server" Text="Final Save"
        CssClass="btn btn-success me-3 custom-btn" OnClientClick="return  IsFinalSave();"  OnClick="ButtonFinalSave_Click" />

 





    <asp:Button ID="ButtonPrint" runat="server" Text="Print / Download"
        CssClass="btn btn-dark me-3 custom-btn" OnClientClick="PrintBabyDetails(); return false;" />

        <asp:Button ID="btnexit" runat="server" Text="EXIT"
        CssClass="btn btn-secondary me-3 custom-btn" OnClick="btnexit_Click" />

        <asp:Button ID="Buttonclear" runat="server" Text="CLEAR"
        CssClass="btn btn-danger me-3 custom-btn" OnClick="btnClear_Click" />
</div>

    





            </div>
        </asp:Panel>
    </div>
</asp:Content>