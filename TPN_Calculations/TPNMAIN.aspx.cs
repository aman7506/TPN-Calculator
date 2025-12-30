using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TNS_Calculations
{
    public partial class TPNMAIN : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["TPNCalculations"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TextTotalVolume.Attributes["style"] = "color: Black; font-weight: bold;";
                TextTotalPer50.Attributes["style"] = "color: Black; font-weight: bold;";


                string registrationNumber = Request.QueryString["RegNo"];

                TextUHID.Text = registrationNumber.ToString();

                string DaysOfTPN = Request.QueryString["DaysOfTPN"];
                TPNDAYText.Text = DaysOfTPN.ToString();

                string PatientName = Request.QueryString["PatientName"];
                TextBabyName.Text = PatientName.ToString();

                string admissionDateTimeStr = Request.QueryString["Admissiondate"];


                if (DateTime.TryParse(admissionDateTimeStr, out DateTime admissionDateTime))
                {
                    // Set Date part in "yyyy-MM-dd" format (required for TextMode="Date")
                    TextDOB.Text = admissionDateTime.ToString("yyyy-MM-dd");

                    // Set Time part in "HH:mm" format (24-hour, required for TextMode="Time")
                    TextBirthTime.Text = admissionDateTime.ToString("HH:mm");

                    CalculateAge_TextChanged();
                }
                else
                {
                    // Optional: handle parsing error
                    TextDOB.Text = "";
                    TextBirthTime.Text = "";
                }

                string AgewithGender = Request.QueryString["AgewithGender"];

                if (!string.IsNullOrEmpty(AgewithGender) && AgewithGender.Contains("/"))
                {
                    string[] parts = AgewithGender.Split('/');
                    if (parts.Length == 2)
                    {
                        string age = parts[0].Trim();    // "17 Days"
                        string gender = parts[1].Trim().ToLower(); // "female"

                      /////  TextAge.Text = age;

                        // Manually match gender (case-insensitive)
                        foreach (ListItem item in DropdownGender.Items)
                        {
                            if (item.Text.ToLower() == gender)
                            {
                                DropdownGender.ClearSelection();
                                item.Selected = true;
                                break;
                            }
                        }
                    }
                }

                AutoFillData();
                ButtonHide();
                ApplyNegativeColor();
            }
        }

        protected void ButtonHide()
        {
            string Encounter = Request.QueryString["Encounter"];
            string registrationNumber = Request.QueryString["RegNo"];
            string Locationid = Session["Actlocation"]?.ToString(); // Null check for Session
            string DaysofTPN = Request.QueryString["DaysOfTPN"];
            string Role = Session["Role"]?.ToString(); // Null check for Session

            if (!string.IsNullOrEmpty(Encounter) &&
                !string.IsNullOrEmpty(registrationNumber) &&
                !string.IsNullOrEmpty(Locationid))
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_AutoFillDataDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Baby_Registration_No", registrationNumber);
                        cmd.Parameters.AddWithValue("@EncounterNo", Encounter);
                        cmd.Parameters.AddWithValue("@LocationId", Locationid);
                        cmd.Parameters.AddWithValue("@DaysOfTPN", DaysofTPN);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        dataAdapter.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string isfinalsave = ds.Tables[0].Rows[0]["IsFinalSave"].ToString();

                            if (isfinalsave == "1" && Role == "Admin")
                            {
                                // Admin can edit even after final save
                                ButtonDraftSave.Visible = false;
                                Buttonclear.Visible = true;
                                ButtonFinalSave.Visible = true;
                                ButtonPrint.Visible = true;
                                btnexit.Visible = true;
                            }
                            else if (isfinalsave == "1")
                            {
                                // Non-admin, final save – restrict editing
                                ButtonDraftSave.Visible = false;
                                Buttonclear.Visible = false;
                                ButtonFinalSave.Visible = false;
                                ButtonPrint.Visible = true;
                                btnexit.Visible = true;
                            }
                            else
                            {
                                // Not final saved yet
                                ButtonDraftSave.Visible = true;
                                Buttonclear.Visible = true;
                                ButtonFinalSave.Visible = true;
                                ButtonPrint.Visible = false;
                                btnexit.Visible = true;
                            }
                        }
                        else
                        {
                            // No data found
                            ButtonPrint.Visible = false;
                            ButtonFinalSave.Visible = false;
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void AutoFillData()
        {
            string Encounter = Request.QueryString["Encounter"];
            string registrationNumber = Request.QueryString["RegNo"];
            string DaysofTPN = Request.QueryString["DaysOfTPN"];
            string Locationid = Session["Actlocation"]?.ToString(); // Added null check for Session

            if (!string.IsNullOrEmpty(Encounter) && !string.IsNullOrEmpty(registrationNumber) && !string.IsNullOrEmpty(Locationid))
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_AutoFillDataDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Baby_Registration_No", registrationNumber);
                        cmd.Parameters.AddWithValue("@EncounterNo", Encounter); // Added EncounterNo
                        cmd.Parameters.AddWithValue("@LocationId", Locationid);
                        cmd.Parameters.AddWithValue("@DaysOfTPN", DaysofTPN);


                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        dataAdapter.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables[0].Rows[0];

                            // Set TextBoxes
                            TextDosingWeight.Text = row["Dosing_wt_kg"].ToString();
                            TextTFR.Text = row["TFR_ml_kg"].ToString();
                            TextFeed.Text = row["Feed_ml_kg"].ToString();
                            TextIVM.Text = row["IVM_ml"].ToString();
                            TextA.Text = row["A_g_kg_day"].ToString();
                            TextL.Text = row["L_g_kg_day"].ToString();
                            TextG.Text = row["G_mg_kg_min"].ToString();
                            TextNa.Text = row["Na_mEq_kg_d"].ToString();
                            TextK.Text = row["K_mEq_kg_d"].ToString();
                            TextCa.Text = row["Ca_mg_kg_d"].ToString();
                            TextMg.Text = row["Mg_mEq_kg_d"].ToString();
                            TPNDAYText.Text = row["DaysofTPN"].ToString();

                            // Set DropDowns
                            DropdownBaseSolution.SelectedValue = row["Dextrose_5_percent"].ToString() == "1" ? "1" : "0";
                            DropdownConcSolution.SelectedValue = row["Dextrose_25_percent"].ToString() == "1" ? "1" : "0";

                            // === Set Dextrose Concentrated Solution Labels ===
                            if (DropdownConcSolution.SelectedValue == "1")
                            {
                                LabelDextroseConcPer.Text = "25% Dextrose (per)";
                                LabelDextroseConc50ml.Text = "25 % Dextrose(50 ml)";
                            }
                            else
                            {
                                LabelDextroseConcPer.Text = "50% Dextrose (per)";
                                LabelDextroseConc50ml.Text = "50 % Dextrose(50 ml)";
                                // Optionally set TextDextroseConcPer or others here if required
                            }

                            // === Set Dextrose Base Solution Labels ===
                            if (DropdownBaseSolution.SelectedValue == "1")
                            {
                                LabelDextroseBasePer.Text = "5% Dextrose (per)";
                                LabelDextroseBase50ml.Text = "5 % Dextrose(50 ml)";
                            }
                            else
                            {
                                LabelDextroseBasePer.Text = "10% Dextrose (per)";
                                LabelDextroseBase50ml.Text = "10 % Dextrose(50 ml)";
                                // Optionally set TextDextroseBasePer or others here if required
                            }


                            // IVM Volume Details
                            TextN5.Text = row["N_5_mL"].ToString();
                            TextN2.Text = row["N_2_mL"].ToString();
                            TextNS.Text = row["NS_mL"].ToString();
                            TextDex10.Text = row["Dex_10_percent_mL"].ToString();
                            DropdownTypeOfOralFeed.SelectedValue = row["Type_of_oral_feed"].ToString();
                            DropdownPreNanStrength.SelectedValue = row["PreNan_strength"].ToString();
                            TextPo4.Text = row["PO4_mg_kg_d"].ToString();
                            TextCalciumViaTPN.Text = row["Calcium_via_TPN"].ToString();
                            TextOverfillFactor.Text = row["Overfill_factor"].ToString();
                            DropdownSodiumSource.SelectedValue = row["Sodium_source"].ToString();
                            TextCelcelInput.Text = row["Celcel"].ToString();

                            // Syringe 1 fields
                            TextLipid.Text = row["Lipid_20_percent"].ToString();
                            TextMVI.Text = row["MVI"].ToString();
                            TextCelcel.Text = row["Celcel"].ToString();
                            TextSyringe1Total.Text = row["Syringe1_Total"].ToString();

                            // Syringe 2 fields
                            TextAminovenPer.Text = row["Aminoven_10_percent"].ToString();
                            TextAminoven50ml.Text = row["Aminoven_10_percent_50ml"].ToString();
                            TextNaclPer.Text = row["Nacl_3_percent"].ToString();
                            TextNacl50ml.Text = row["Nacl_3_percent_50ml"].ToString();
                            TextKclPer.Text = row["KCl_15_Percent"].ToString();
                            TextKcl50ml.Text = row["Kcl_15_percent_50ml"].ToString();
                            TextCalciumPer.Text = row["Calcium_10_Percent"].ToString();
                            TextCalcium50ml.Text = row["Calcium_10_percent_50ml"].ToString();
                            TextMgso4Per.Text = row["MgSO4_50_Percent"].ToString();
                            TextMgso450ml.Text = row["Mgso4_50_percent_50ml"].ToString();
                            TextDextroseBasePer.Text = row["Dextrose_5_Percent"].ToString();
                            TextDextroseBase50ml.Text = row["Dextrose_5_percent_50ml"].ToString();
                            TextDextroseConcPer.Text = row["Dextrose_25_Percent"].ToString();
                            TextDextroseConc50ml.Text = row["Dextrose_25_percent_50ml"].ToString();

                            // Grand totals
                            TextTotalVolume.Text = row["Total_Volume"].ToString();
                            TextTotalPer50.Text = row["Total_Per_Hour_50"].ToString();

                            // Fluid Calcium fields
                            TextPotPhos.Text = row["PotPhos_PO4"].ToString();
                            TextCalcium10.Text = row["Calcium_10_percent_2_New"].ToString();


                            // Nutritional Requirements
                            TextNutriationtfr.Text = row["TFR_mL_kg"].ToString();
                            TextTfv.Text = row["TFV_mL"].ToString();
                            TextFeeds.Text = row["Feed_mL"].ToString();
                            TextivfMlKg.Text = row["IVF_mL_kg"].ToString();
                            TextivfMl.Text = row["IVF_mL"].ToString();
                            TextTpnFluidMl.Text = row["TPN_Fluid_mL"].ToString();
                            TextTpnGlucoseG.Text = row["TPN_Glucose_g"].ToString();
                            TextFluidForGlucose.Text = row["Fluid_for_Glucose"].ToString();
                            TextOsmolarity.Text = row["Osmolarity_Sy1_Sy2"].ToString();
                            TextDextrose.Text = row["Dextrose_percent"].ToString();
                            TextCnr.Text = row["CNR"].ToString();
                            TextCaloriesForToday.Text = row["Calories_for_Today"].ToString();
                            TextProteinsForToday.Text = row["Proteins_for_Today"].ToString();
                            TextNaInIvm.Text = row["Na_in_IVM_mEq_kg_d"].ToString();
                            TextGlucoseInIvm.Text = row["Glucose_in_IVM_g"].ToString();
                            TextKInPotphos.Text = row["K_in_Potphos_mEq_kg_d"].ToString();


                        }
                    }
                    conn.Close();
                }
            }
        }

        private void ApplyNegativeColor()
        {
            SetTextColor(TextTotalVolume);
            SetTextColor(TextTotalPer50);
            SetTextColor(TextPotPhos);
            SetTextColor(TextCalcium10);

            SetTextColor(TextAminovenPer);
            SetTextColor(TextAminoven50ml);
            SetTextColor(TextNaclPer);
            SetTextColor(TextNacl50ml);
            SetTextColor(TextKclPer);
            SetTextColor(TextKcl50ml);
            SetTextColor(TextCalciumPer);
            SetTextColor(TextCalcium50ml);
            SetTextColor(TextMgso4Per);
            SetTextColor(TextMgso450ml);
            SetTextColor(TextDextroseBasePer);
            SetTextColor(TextDextroseBase50ml);
            SetTextColor(TextDextroseConcPer);
            SetTextColor(TextDextroseConc50ml);

            SetTextColor(TextNutriationtfr);
            SetTextColor(TextTfv);
            SetTextColor(TextFeeds);
            SetTextColor(TextivfMlKg);
            SetTextColor(TextivfMl);
            SetTextColor(TextTpnFluidMl);
            SetTextColor(TextTpnGlucoseG);
            SetTextColor(TextFluidForGlucose);
            SetTextColor(TextOsmolarity);
            SetTextColor(TextDextrose);
            SetTextColor(TextCnr);
            SetTextColor(TextCaloriesForToday);
            SetTextColor(TextProteinsForToday);
            SetTextColor(TextNaInIvm);
            SetTextColor(TextGlucoseInIvm);
            SetTextColor(TextKInPotphos);
        }

        private void SetTextColor(TextBox textBox)
        {
            if (double.TryParse(textBox.Text, out double value))
            {
                if (value < 0)
                {
                    textBox.Attributes["style"] = "color: red; font-weight: bold;";
                }
                else
                {
                    textBox.Attributes["style"] = "color: black; font-weight: normal;";
                }
            }
        }
        protected void ButtonCalculateSave_Click(object sender, EventArgs e)
        {
            Nutritional_TextChanged();
            Lipid_TextChanged();
            Syring1_Celcel_TextChanged();
            wrongvalue();
            Total_TextChanged();
            Syring1_Celcel_TextChanged();
            Sodium_source();

            fluidUpdate();


            fluidUpdate();

            CalculateValues();
            CalculateValues50_Dext();
            CalculateTotalVolume();
            Syringe2_50ml();
            Osmolarity();
            ApplyNegativeColor();
            ButtonHide();


        }

        protected void ButtonDraftSave_Click(object sender, EventArgs e)
        {
            if ( !ValidateAllFields())
                return;
            Nutritional_TextChanged();
            Lipid_TextChanged();
            Syring1_Celcel_TextChanged();
            wrongvalue();
            Total_TextChanged();
            Syring1_Celcel_TextChanged();
            Sodium_source();

            fluidUpdate();


            fluidUpdate();

            CalculateValues();
            CalculateValues50_Dext();
            CalculateTotalVolume();
            Syringe2_50ml();
            Osmolarity();
            ApplyNegativeColor();
            InsertData();
            ButtonHide();
        }

        protected void ButtonFinalSave_Click(object sender, EventArgs e)
        {
            if (!TPNDAYS())
                return;
            Nutritional_TextChanged();
            Lipid_TextChanged();
            Syring1_Celcel_TextChanged();
            wrongvalue();
            Total_TextChanged();
            Syring1_Celcel_TextChanged();
            Sodium_source();

            fluidUpdate();


            fluidUpdate();

            CalculateValues();
            CalculateValues50_Dext();
            CalculateTotalVolume();
            Syringe2_50ml();
            Osmolarity();
            ApplyNegativeColor();
            FinalData();
            ButtonHide();
        }

        private bool TPNDAYS()
        {
            string tpnDays = TPNDAYText.Text.Trim();

            if (string.IsNullOrWhiteSpace(tpnDays))
            {
                ShowAlert("Missing TPN Days", "Please enter the number of TPN Days.");
                return false;
            }

            return true;
        }

        private bool ValidateAllFields()
        {
            var fieldsToCheck = new Dictionary<string, TextBox>
    {
        { "Dosing wt (kg)", TextDosingWeight },
        { "TFR (ml/kg)", TextTFR },
        { "Feed (ml/kg)", TextFeed },
        { "IVM (ml)", TextIVM },
        { "A (g/kg/day)", TextA },
        { "L (g/kg/day)", TextL },
        { "G (mg/kg/min)", TextG },
        { "Na (mEq/kg/d)", TextNa },
        { "K (mEq/kg/d)", TextK },
        { "Ca (mg/kg/d)", TextCa },
        { "Mg (mEq/kg/d)", TextMg },
        { "N/5 (mL)", TextN5 },
        { "N/2 (mL)", TextN2 },
        { "NS (mL)", TextNS },
        { "10% Dex (mL)", TextDex10 },
        { "PO4 (mg/kg/d)", TextPo4 },
        { "Calcium via TPN", TextCalciumViaTPN },
        { "Overfill factor", TextOverfillFactor },
        { "Celcel Input", TextCelcelInput }
    };

            foreach (var field in fieldsToCheck)
            {
                if (string.IsNullOrWhiteSpace(field.Value.Text))
                {
                    ShowAlert("Missing Field", $"Please enter a value for {field.Key}.");
                    return false;
                }
            }

            return true;
        }


        private void ShowAlert(string title, string message)
        {
            string script = $@"
        <script src='https://cdn.jsdelivr.net/npm/sweetalert2@11'></script>
        <script>
            Swal.fire({{
                icon: 'warning',
                title: '{title}',
                text: '{message}',
                confirmButtonText: 'OK',
                backdrop: true,
                allowOutsideClick: false,
                allowEscapeKey: true,
                customClass: {{
                    popup: 'swal2-popup-class'
                }}
            }});
        </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "TPNDaysAlert", script);
        }


        protected void InsertData()
        {
           //// string connString = ConfigurationManager.ConnectionStrings["TPNCalculations"].ConnectionString;

     


            string registrationNumber = string.Empty;
           registrationNumber = Request.QueryString["RegNo"];
            string Locationid = Session["Actlocation"].ToString();
            string DaysOfTPN = Request.QueryString["DaysOfTPN"];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("USP_insert_Dosing_Wt", conn);
                cmd.CommandType = CommandType.StoredProcedure;
     
                cmd.Parameters.AddWithValue("@Baby_Registration_No", registrationNumber);
                cmd.Parameters.AddWithValue("@Locationid", Locationid);
                cmd.Parameters.AddWithValue("@DaysofTPN", DaysOfTPN);

                cmd.Parameters.AddWithValue("@Action", "selectAll");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    InsertDataDraft();
                    insertupdatestatus();
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    InsertDataDraftUpdate();
                    insertupdatestatus();
                }
            }

          
        }

        protected void InsertDataDraft()

        {

            string Encounter = Request.QueryString["Encounter"];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand com = new SqlCommand("USP_insert_Dosing_Wt", conn))
                {
                    com.CommandType = CommandType.StoredProcedure;

                    // Basic Info
                    com.Parameters.AddWithValue("@Action", "Insert");
                    com.Parameters.AddWithValue("@Baby_Registration_No", TextUHID.Text.Trim());
                    com.Parameters.AddWithValue("@Name", TextBabyName.Text.Trim());
                    com.Parameters.AddWithValue("@BirthDate", TextDOB.Text.Trim());
                    com.Parameters.AddWithValue("@Gender", DropdownGender.SelectedValue);
                    com.Parameters.AddWithValue("@DaysofTPN", TPNDAYText.Text.Trim());


                    // Dosing & Fluids
                    com.Parameters.AddWithValue("@Dosing_wt_kg", TextDosingWeight.Text.Trim());
                    com.Parameters.AddWithValue("@TFR_ml_kg", TextTFR.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml_kg", TextFeed.Text.Trim());
                    com.Parameters.AddWithValue("@IVM_ml", TextIVM.Text.Trim());
                    com.Parameters.AddWithValue("@A_g_kg_day", TextA.Text.Trim());
                    com.Parameters.AddWithValue("@L_g_kg_day", TextL.Text.Trim());
                    com.Parameters.AddWithValue("@G_mg_kg_min", TextG.Text.Trim());
                    com.Parameters.AddWithValue("@Na_mEq_kg_d", TextNa.Text.Trim());
                    com.Parameters.AddWithValue("@K_mEq_kg_d", TextK.Text.Trim());
                    com.Parameters.AddWithValue("@Ca_mg_kg_d", TextCa.Text.Trim());
                    com.Parameters.AddWithValue("@Mg_mEq_kg_d", TextMg.Text.Trim());

                    // Dextrose (based on dropdown selection)
                    com.Parameters.AddWithValue("@Dextrose_5_percent", DropdownBaseSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_10_percent", DropdownBaseSolution.SelectedValue == "0" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_25_percent", DropdownConcSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_50_percent", DropdownConcSolution.SelectedValue == "0" ? "1" : "0");

                    // IVM Volume Details
                    com.Parameters.AddWithValue("@N_5_mL", TextN5.Text.Trim());
                    com.Parameters.AddWithValue("@N_2_mL", TextN2.Text.Trim());
                    com.Parameters.AddWithValue("@NS_mL", TextNS.Text.Trim());
                    com.Parameters.AddWithValue("@Dex_10_percent_mL", TextDex10.Text.Trim());
                    com.Parameters.AddWithValue("@Type_of_oral_feed", DropdownTypeOfOralFeed.SelectedValue);
                    com.Parameters.AddWithValue("@PreNan_strength", DropdownPreNanStrength.SelectedValue);
                    com.Parameters.AddWithValue("@PO4_mg_kg_d", TextPo4.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_via_TPN", TextCalciumViaTPN.Text.Trim());
                    com.Parameters.AddWithValue("@Overfill_factor", TextOverfillFactor.Text.Trim());
                    com.Parameters.AddWithValue("@Sodium_source", DropdownSodiumSource.SelectedValue);
                    com.Parameters.AddWithValue("@Celcel_Input", TextCelcelInput.Text.Trim());
                    com.Parameters.AddWithValue("@IsFinalSave", "0");
                    com.Parameters.AddWithValue("@EncounterNo", Encounter.ToString());


                    // Syringe 1
                    com.Parameters.AddWithValue("@Lipid_20_percent", TextLipid.Text.Trim());
                    com.Parameters.AddWithValue("@MVI", TextMVI.Text.Trim());
                    com.Parameters.AddWithValue("@Celcel_Syring1", TextCelcel.Text.Trim());
                    com.Parameters.AddWithValue("@Syringe1_Total", TextSyringe1Total.Text.Trim());

                    // Syringe 2
                    com.Parameters.AddWithValue("@Aminoven_10_percent", TextAminovenPer.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent", TextNaclPer.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent", TextKclPer.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent", TextCalciumPer.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent", TextMgso4Per.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_5_percent", TextDextroseBasePer.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_25_percent", TextDextroseConcPer.Text.Trim());

                    com.Parameters.AddWithValue("@Aminoven_10_percent_50ml", TextAminoven50ml.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent_50ml", TextNacl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent_50ml", TextKcl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent_50ml", TextCalcium50ml.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent_50ml", TextMgso450ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_5_percent_50ml", TextDextroseBase50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_25_percent_50ml", TextDextroseConc50ml.Text.Trim());

                    // Fluid Calcium
                    com.Parameters.AddWithValue("@PotPhos_PO4", TextPotPhos.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_5_percent_2_New", ""); // Adjust if this input exists
                    com.Parameters.AddWithValue("@Calcium_10_percent_2_New", TextCalcium10.Text.Trim());

                    // Total values
                    com.Parameters.AddWithValue("@Total_Volume", TextTotalVolume.Text.Trim());
                    com.Parameters.AddWithValue("@Total_Per_Hour_50", TextTotalPer50.Text.Trim());

                    // Nutritional Requirements
                    com.Parameters.AddWithValue("@TFV_ml", TextTfv.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml", TextFeeds.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml_kg", TextivfMlKg.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml", TextivfMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_fluid_ml", TextTpnFluidMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_Glucose_g", TextTpnGlucoseG.Text.Trim());
                    com.Parameters.AddWithValue("@Fluid_for_Glucose", TextFluidForGlucose.Text.Trim());
                    com.Parameters.AddWithValue("@Osmolarity_Sy1_Sy2", TextOsmolarity.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_percent", TextDextrose.Text.Trim());
                    com.Parameters.AddWithValue("@CNR", TextCnr.Text.Trim());
                    com.Parameters.AddWithValue("@Calories_for_today", TextCaloriesForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Proteins_for_today", TextProteinsForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Na_in_IVM_mEq_kg_d", TextNaInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@Glucose_in_IVM_g", TextGlucoseInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@K_in_Potphos_mEq_kg_d", TextKInPotphos.Text.Trim());
                    // Metadata
                    com.Parameters.AddWithValue("@CreatedBy", (String)Session["uid"]);
                    com.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    com.Parameters.AddWithValue("@LocationId", (String)Session["Actlocation"]);

                    com.ExecuteNonQuery();
                    string script = "toastr.success('Records have been successfully Drafted!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "toastrSuccess", script, true);

                }
            }
        }


        protected void InsertDataDraftUpdate()

        {

            string Encounter = Request.QueryString["Encounter"];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand com = new SqlCommand("USP_insert_Dosing_Wt", conn))
                {
                    com.CommandType = CommandType.StoredProcedure;

                    // Basic Info
                    com.Parameters.AddWithValue("@Action", "DraftUpdate");
                    com.Parameters.AddWithValue("@Baby_Registration_No", TextUHID.Text.Trim());
                    com.Parameters.AddWithValue("@DaysofTPN", TPNDAYText.Text.Trim());
                    com.Parameters.AddWithValue("@Name", TextBabyName.Text.Trim());
                    com.Parameters.AddWithValue("@BirthDate", TextDOB.Text.Trim());
                    com.Parameters.AddWithValue("@Gender", DropdownGender.SelectedValue);

                    // Dosing & Fluids
                    com.Parameters.AddWithValue("@Dosing_wt_kg", TextDosingWeight.Text.Trim());
                    com.Parameters.AddWithValue("@TFR_ml_kg", TextTFR.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml_kg", TextFeed.Text.Trim());
                    com.Parameters.AddWithValue("@IVM_ml", TextIVM.Text.Trim());
                    com.Parameters.AddWithValue("@A_g_kg_day", TextA.Text.Trim());
                    com.Parameters.AddWithValue("@L_g_kg_day", TextL.Text.Trim());
                    com.Parameters.AddWithValue("@G_mg_kg_min", TextG.Text.Trim());
                    com.Parameters.AddWithValue("@Na_mEq_kg_d", TextNa.Text.Trim());
                    com.Parameters.AddWithValue("@K_mEq_kg_d", TextK.Text.Trim());
                    com.Parameters.AddWithValue("@Ca_mg_kg_d", TextCa.Text.Trim());
                    com.Parameters.AddWithValue("@Mg_mEq_kg_d", TextMg.Text.Trim());

                    // Dextrose (based on dropdown selection)
                    com.Parameters.AddWithValue("@Dextrose_5_percent", DropdownBaseSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_10_percent", DropdownBaseSolution.SelectedValue == "0" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_25_percent", DropdownConcSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_50_percent", DropdownConcSolution.SelectedValue == "0" ? "1" : "0");

                    // IVM Volume Details
                    com.Parameters.AddWithValue("@N_5_mL", TextN5.Text.Trim());
                    com.Parameters.AddWithValue("@N_2_mL", TextN2.Text.Trim());
                    com.Parameters.AddWithValue("@NS_mL", TextNS.Text.Trim());
                    com.Parameters.AddWithValue("@Dex_10_percent_mL", TextDex10.Text.Trim());
                    com.Parameters.AddWithValue("@Type_of_oral_feed", DropdownTypeOfOralFeed.SelectedValue);
                    com.Parameters.AddWithValue("@PreNan_strength", DropdownPreNanStrength.SelectedValue);
                    com.Parameters.AddWithValue("@PO4_mg_kg_d", TextPo4.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_via_TPN", TextCalciumViaTPN.Text.Trim());
                    com.Parameters.AddWithValue("@Overfill_factor", TextOverfillFactor.Text.Trim());
                    com.Parameters.AddWithValue("@Sodium_source", DropdownSodiumSource.SelectedValue);
                    com.Parameters.AddWithValue("@Celcel_Input", TextCelcelInput.Text.Trim());
                    com.Parameters.AddWithValue("@IsFinalSave", "0");
                    com.Parameters.AddWithValue("@EncounterNo", Encounter.ToString());


                    // Syringe 1
                    com.Parameters.AddWithValue("@Lipid_20_percent", TextLipid.Text.Trim());
                    com.Parameters.AddWithValue("@MVI", TextMVI.Text.Trim());
                    com.Parameters.AddWithValue("@Celcel_Syring1", TextCelcel.Text.Trim());
                    com.Parameters.AddWithValue("@Syringe1_Total", TextSyringe1Total.Text.Trim());

                    // Syringe 2
                    com.Parameters.AddWithValue("@Aminoven_10_percent", TextAminovenPer.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent", TextNaclPer.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent", TextKclPer.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent", TextCalciumPer.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent", TextMgso4Per.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_5_percent", TextDextroseBasePer.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_25_percent", TextDextroseConcPer.Text.Trim());

                    com.Parameters.AddWithValue("@Aminoven_10_percent_50ml", TextAminoven50ml.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent_50ml", TextNacl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent_50ml", TextKcl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent_50ml", TextCalcium50ml.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent_50ml", TextMgso450ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_5_percent_50ml", TextDextroseBase50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_25_percent_50ml", TextDextroseConc50ml.Text.Trim());

                    // Fluid Calcium
                    com.Parameters.AddWithValue("@PotPhos_PO4", TextPotPhos.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_5_percent_2_New", ""); // Adjust if this input exists
                    com.Parameters.AddWithValue("@Calcium_10_percent_2_New", TextCalcium10.Text.Trim());

                    // Total values
                    com.Parameters.AddWithValue("@Total_Volume", TextTotalVolume.Text.Trim());
                    com.Parameters.AddWithValue("@Total_Per_Hour_50", TextTotalPer50.Text.Trim());

                    // Nutritional Requirements
                    com.Parameters.AddWithValue("@TFV_ml", TextTfv.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml", TextFeeds.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml_kg", TextivfMlKg.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml", TextivfMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_fluid_ml", TextTpnFluidMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_Glucose_g", TextTpnGlucoseG.Text.Trim());
                    com.Parameters.AddWithValue("@Fluid_for_Glucose", TextFluidForGlucose.Text.Trim());
                    com.Parameters.AddWithValue("@Osmolarity_Sy1_Sy2", TextOsmolarity.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_percent", TextDextrose.Text.Trim());
                    com.Parameters.AddWithValue("@CNR", TextCnr.Text.Trim());
                    com.Parameters.AddWithValue("@Calories_for_today", TextCaloriesForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Proteins_for_today", TextProteinsForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Na_in_IVM_mEq_kg_d", TextNaInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@Glucose_in_IVM_g", TextGlucoseInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@K_in_Potphos_mEq_kg_d", TextKInPotphos.Text.Trim());
                    // Metadata
                    com.Parameters.AddWithValue("@CreatedBy", (String)Session["uid"]);
                    com.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    com.Parameters.AddWithValue("@LocationId", (String)Session["Actlocation"]);

                    com.ExecuteNonQuery();
                    string script = "toastr.success('Records have been successfully Drafted!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "toastrSuccess", script, true);


                }
            }
        }


        protected void FinalData()
        {
            //// string connString = ConfigurationManager.ConnectionStrings["TPNCalculations"].ConnectionString;




            string registrationNumber = string.Empty;
            registrationNumber = Request.QueryString["RegNo"];
            string Locationid = Session["Actlocation"].ToString();
            string DaysOfTPN = Request.QueryString["DaysOfTPN"];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("USP_insert_Dosing_Wt", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Baby_Registration_No", registrationNumber);
                cmd.Parameters.AddWithValue("@Locationid", Locationid);
                cmd.Parameters.AddWithValue("@DaysofTPN", DaysOfTPN);

                cmd.Parameters.AddWithValue("@Action", "selectAll");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    InsertDataFinal();
                    statusupdate();
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    FinalDataDraftUpdate();
                    statusupdate();
                }
            }


        }

        protected void InsertDataFinal()
        {

            string Encounter = Request.QueryString["Encounter"];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand com = new SqlCommand("USP_insert_Dosing_Wt", conn))
                {
                    com.CommandType = CommandType.StoredProcedure;

                    // Basic Info
                    com.Parameters.AddWithValue("@Action", "Insert");
                    com.Parameters.AddWithValue("@Baby_Registration_No", TextUHID.Text.Trim());
                    com.Parameters.AddWithValue("@Name", TextBabyName.Text.Trim());
                    com.Parameters.AddWithValue("@BirthDate", TextDOB.Text.Trim());
                    com.Parameters.AddWithValue("@Gender", DropdownGender.SelectedValue);
                    com.Parameters.AddWithValue("@DaysofTPN", TPNDAYText.Text.Trim());

                    // Dosing & Fluids
                    com.Parameters.AddWithValue("@Dosing_wt_kg", TextDosingWeight.Text.Trim());
                    com.Parameters.AddWithValue("@TFR_ml_kg", TextTFR.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml_kg", TextFeed.Text.Trim());
                    com.Parameters.AddWithValue("@IVM_ml", TextIVM.Text.Trim());
                    com.Parameters.AddWithValue("@A_g_kg_day", TextA.Text.Trim());
                    com.Parameters.AddWithValue("@L_g_kg_day", TextL.Text.Trim());
                    com.Parameters.AddWithValue("@G_mg_kg_min", TextG.Text.Trim());
                    com.Parameters.AddWithValue("@Na_mEq_kg_d", TextNa.Text.Trim());
                    com.Parameters.AddWithValue("@K_mEq_kg_d", TextK.Text.Trim());
                    com.Parameters.AddWithValue("@Ca_mg_kg_d", TextCa.Text.Trim());
                    com.Parameters.AddWithValue("@Mg_mEq_kg_d", TextMg.Text.Trim());

                    // Dextrose (based on dropdown selection)
                    com.Parameters.AddWithValue("@Dextrose_5_percent", DropdownBaseSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_10_percent", DropdownBaseSolution.SelectedValue == "0" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_25_percent", DropdownConcSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_50_percent", DropdownConcSolution.SelectedValue == "0" ? "1" : "0");

                    // IVM Volume Details
                    com.Parameters.AddWithValue("@N_5_mL", TextN5.Text.Trim());
                    com.Parameters.AddWithValue("@N_2_mL", TextN2.Text.Trim());
                    com.Parameters.AddWithValue("@NS_mL", TextNS.Text.Trim());
                    com.Parameters.AddWithValue("@Dex_10_percent_mL", TextDex10.Text.Trim());
                    com.Parameters.AddWithValue("@Type_of_oral_feed", DropdownTypeOfOralFeed.SelectedValue);
                    com.Parameters.AddWithValue("@PreNan_strength", DropdownPreNanStrength.SelectedValue);
                    com.Parameters.AddWithValue("@PO4_mg_kg_d", TextPo4.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_via_TPN", TextCalciumViaTPN.Text.Trim());
                    com.Parameters.AddWithValue("@Overfill_factor", TextOverfillFactor.Text.Trim());
                    com.Parameters.AddWithValue("@Sodium_source", DropdownSodiumSource.SelectedValue);
                    com.Parameters.AddWithValue("@Celcel_Input", TextCelcelInput.Text.Trim());
                    com.Parameters.AddWithValue("@IsFinalSave", "1");
                    com.Parameters.AddWithValue("@EncounterNo", Encounter.ToString());


                    // Syringe 1
                    com.Parameters.AddWithValue("@Lipid_20_percent", TextLipid.Text.Trim());
                    com.Parameters.AddWithValue("@MVI", TextMVI.Text.Trim());
                    com.Parameters.AddWithValue("@Celcel_Syring1", TextCelcel.Text.Trim());
                    com.Parameters.AddWithValue("@Syringe1_Total", TextSyringe1Total.Text.Trim());

                    // Syringe 2
                    com.Parameters.AddWithValue("@Aminoven_10_percent", TextAminovenPer.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent", TextNaclPer.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent", TextKclPer.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent", TextCalciumPer.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent", TextMgso4Per.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_5_percent", TextDextroseBasePer.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_25_percent", TextDextroseConcPer.Text.Trim());

                    com.Parameters.AddWithValue("@Aminoven_10_percent_50ml", TextAminoven50ml.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent_50ml", TextNacl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent_50ml", TextKcl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent_50ml", TextCalcium50ml.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent_50ml", TextMgso450ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_5_percent_50ml", TextDextroseBase50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_25_percent_50ml", TextDextroseConc50ml.Text.Trim());

                    // Fluid Calcium
                    com.Parameters.AddWithValue("@PotPhos_PO4", TextPotPhos.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_5_percent_2_New", ""); // Adjust if this input exists
                    com.Parameters.AddWithValue("@Calcium_10_percent_2_New", TextCalcium10.Text.Trim());

                    // Total values
                    com.Parameters.AddWithValue("@Total_Volume", TextTotalVolume.Text.Trim());
                    com.Parameters.AddWithValue("@Total_Per_Hour_50", TextTotalPer50.Text.Trim());

                    // Nutritional Requirements
                    com.Parameters.AddWithValue("@TFV_ml", TextTfv.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml", TextFeeds.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml_kg", TextivfMlKg.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml", TextivfMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_fluid_ml", TextTpnFluidMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_Glucose_g", TextTpnGlucoseG.Text.Trim());
                    com.Parameters.AddWithValue("@Fluid_for_Glucose", TextFluidForGlucose.Text.Trim());
                    com.Parameters.AddWithValue("@Osmolarity_Sy1_Sy2", TextOsmolarity.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_percent", TextDextrose.Text.Trim());
                    com.Parameters.AddWithValue("@CNR", TextCnr.Text.Trim());
                    com.Parameters.AddWithValue("@Calories_for_today", TextCaloriesForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Proteins_for_today", TextProteinsForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Na_in_IVM_mEq_kg_d", TextNaInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@Glucose_in_IVM_g", TextGlucoseInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@K_in_Potphos_mEq_kg_d", TextKInPotphos.Text.Trim());
                    // Metadata
                    com.Parameters.AddWithValue("@CreatedBy", (String)Session["uid"]);
                    com.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    com.Parameters.AddWithValue("@LocationId", (String)Session["Actlocation"]);

                    com.ExecuteNonQuery();
                }
            }
        }

        protected void FinalDataDraftUpdate()

        {

            string Encounter = Request.QueryString["Encounter"];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand com = new SqlCommand("USP_insert_Dosing_Wt", conn))
                {
                    com.CommandType = CommandType.StoredProcedure;

                    // Basic Info
                    com.Parameters.AddWithValue("@Action", "FinalUpdate");
                    com.Parameters.AddWithValue("@Baby_Registration_No", TextUHID.Text.Trim());
                    com.Parameters.AddWithValue("@Name", TextBabyName.Text.Trim());
                    com.Parameters.AddWithValue("@BirthDate", TextDOB.Text.Trim());
                    com.Parameters.AddWithValue("@Gender", DropdownGender.SelectedValue);
                    com.Parameters.AddWithValue("@DaysofTPN", TPNDAYText.Text.Trim());

                    // Dosing & Fluids
                    com.Parameters.AddWithValue("@Dosing_wt_kg", TextDosingWeight.Text.Trim());
                    com.Parameters.AddWithValue("@TFR_ml_kg", TextTFR.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml_kg", TextFeed.Text.Trim());
                    com.Parameters.AddWithValue("@IVM_ml", TextIVM.Text.Trim());
                    com.Parameters.AddWithValue("@A_g_kg_day", TextA.Text.Trim());
                    com.Parameters.AddWithValue("@L_g_kg_day", TextL.Text.Trim());
                    com.Parameters.AddWithValue("@G_mg_kg_min", TextG.Text.Trim());
                    com.Parameters.AddWithValue("@Na_mEq_kg_d", TextNa.Text.Trim());
                    com.Parameters.AddWithValue("@K_mEq_kg_d", TextK.Text.Trim());
                    com.Parameters.AddWithValue("@Ca_mg_kg_d", TextCa.Text.Trim());
                    com.Parameters.AddWithValue("@Mg_mEq_kg_d", TextMg.Text.Trim());

                    // Dextrose (based on dropdown selection)
                    com.Parameters.AddWithValue("@Dextrose_5_percent", DropdownBaseSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_10_percent", DropdownBaseSolution.SelectedValue == "0" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_25_percent", DropdownConcSolution.SelectedValue == "1" ? "1" : "0");
                    com.Parameters.AddWithValue("@Dextrose_50_percent", DropdownConcSolution.SelectedValue == "0" ? "1" : "0");

                    // IVM Volume Details
                    com.Parameters.AddWithValue("@N_5_mL", TextN5.Text.Trim());
                    com.Parameters.AddWithValue("@N_2_mL", TextN2.Text.Trim());
                    com.Parameters.AddWithValue("@NS_mL", TextNS.Text.Trim());
                    com.Parameters.AddWithValue("@Dex_10_percent_mL", TextDex10.Text.Trim());
                    com.Parameters.AddWithValue("@Type_of_oral_feed", DropdownTypeOfOralFeed.SelectedValue);
                    com.Parameters.AddWithValue("@PreNan_strength", DropdownPreNanStrength.SelectedValue);
                    com.Parameters.AddWithValue("@PO4_mg_kg_d", TextPo4.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_via_TPN", TextCalciumViaTPN.Text.Trim());
                    com.Parameters.AddWithValue("@Overfill_factor", TextOverfillFactor.Text.Trim());
                    com.Parameters.AddWithValue("@Sodium_source", DropdownSodiumSource.SelectedValue);
                    com.Parameters.AddWithValue("@Celcel_Input", TextCelcelInput.Text.Trim());
                    com.Parameters.AddWithValue("@IsFinalSave", "1");
                    com.Parameters.AddWithValue("@EncounterNo", Encounter.ToString());


                    // Syringe 1
                    com.Parameters.AddWithValue("@Lipid_20_percent", TextLipid.Text.Trim());
                    com.Parameters.AddWithValue("@MVI", TextMVI.Text.Trim());
                    com.Parameters.AddWithValue("@Celcel_Syring1", TextCelcel.Text.Trim());
                    com.Parameters.AddWithValue("@Syringe1_Total", TextSyringe1Total.Text.Trim());

                    // Syringe 2
                    com.Parameters.AddWithValue("@Aminoven_10_percent", TextAminovenPer.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent", TextNaclPer.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent", TextKclPer.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent", TextCalciumPer.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent", TextMgso4Per.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_5_percent", TextDextroseBasePer.Text.Trim());
                    com.Parameters.AddWithValue("@Syring2_Dextrose_25_percent", TextDextroseConcPer.Text.Trim());

                    com.Parameters.AddWithValue("@Aminoven_10_percent_50ml", TextAminoven50ml.Text.Trim());
                    com.Parameters.AddWithValue("@NaCl_3_percent_50ml", TextNacl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@KCl_15_percent_50ml", TextKcl50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_10_percent_50ml", TextCalcium50ml.Text.Trim());
                    com.Parameters.AddWithValue("@MgSO4_50_percent_50ml", TextMgso450ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_5_percent_50ml", TextDextroseBase50ml.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_25_percent_50ml", TextDextroseConc50ml.Text.Trim());

                    // Fluid Calcium
                    com.Parameters.AddWithValue("@PotPhos_PO4", TextPotPhos.Text.Trim());
                    com.Parameters.AddWithValue("@Calcium_5_percent_2_New", ""); // Adjust if this input exists
                    com.Parameters.AddWithValue("@Calcium_10_percent_2_New", TextCalcium10.Text.Trim());

                    // Total values
                    com.Parameters.AddWithValue("@Total_Volume", TextTotalVolume.Text.Trim());
                    com.Parameters.AddWithValue("@Total_Per_Hour_50", TextTotalPer50.Text.Trim());

                    // Nutritional Requirements
                    com.Parameters.AddWithValue("@TFV_ml", TextTfv.Text.Trim());
                    com.Parameters.AddWithValue("@Feed_ml", TextFeeds.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml_kg", TextivfMlKg.Text.Trim());
                    com.Parameters.AddWithValue("@IVF_ml", TextivfMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_fluid_ml", TextTpnFluidMl.Text.Trim());
                    com.Parameters.AddWithValue("@TPN_Glucose_g", TextTpnGlucoseG.Text.Trim());
                    com.Parameters.AddWithValue("@Fluid_for_Glucose", TextFluidForGlucose.Text.Trim());
                    com.Parameters.AddWithValue("@Osmolarity_Sy1_Sy2", TextOsmolarity.Text.Trim());
                    com.Parameters.AddWithValue("@Dextrose_percent", TextDextrose.Text.Trim());
                    com.Parameters.AddWithValue("@CNR", TextCnr.Text.Trim());
                    com.Parameters.AddWithValue("@Calories_for_today", TextCaloriesForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Proteins_for_today", TextProteinsForToday.Text.Trim());
                    com.Parameters.AddWithValue("@Na_in_IVM_mEq_kg_d", TextNaInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@Glucose_in_IVM_g", TextGlucoseInIvm.Text.Trim());
                    com.Parameters.AddWithValue("@K_in_Potphos_mEq_kg_d", TextKInPotphos.Text.Trim());
                    // Metadata
                    com.Parameters.AddWithValue("@UpdatedBy", (String)Session["uid"]);
                    com.Parameters.AddWithValue("@UpdatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    com.Parameters.AddWithValue("@LocationId", (String)Session["Actlocation"]);

                    com.ExecuteNonQuery();
                }
            }
        }


        protected void insertupdatestatus()
        {

              string Encounter = Request.QueryString["Encounter"];
            string DaysOfTPN = Request.QueryString["DaysOfTPN"];

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("USP_insert_Dosing_Wt", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EncounterNo", Encounter);
                cmd.Parameters.AddWithValue("@Baby_Registration_No", TextUHID.Text);

                    cmd.Parameters.AddWithValue("@CreatedBy", (String)Session["uid"]);
                    cmd.Parameters.AddWithValue("@LocationId", (String)Session["Actlocation"]);
                cmd.Parameters.AddWithValue("@DaysofTPN", DaysOfTPN);

                cmd.Parameters.AddWithValue("@Action", "InsertStatus");

                    cmd.ExecuteNonQuery();
                    /// this.Controls.Add(new LiteralControl("<script type='text/javascript'>toastr.warning('Header Details has been Successfully Updated!')</script>"));
                    con.Close();
                }
            
           
        }

        protected void statusupdate()
        {

            string Encounter = Request.QueryString["Encounter"];
            string DaysOfTPN = Request.QueryString["DaysOfTPN"];
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_insert_Dosing_Wt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EncounterNo", Encounter);
                cmd.Parameters.AddWithValue("@Baby_Registration_No", TextUHID.Text);

                cmd.Parameters.AddWithValue("@CreatedBy", (String)Session["uid"]);
                cmd.Parameters.AddWithValue("@LocationId", (String)Session["Actlocation"]);
                cmd.Parameters.AddWithValue("@DaysofTPN", DaysOfTPN);


                cmd.Parameters.AddWithValue("@Action", "UpdateStatus");

                cmd.ExecuteNonQuery();
                /// this.Controls.Add(new LiteralControl("<script type='text/javascript'>toastr.warning('Header Details has been Successfully Updated!')</script>"));
                con.Close();
            }

        }

        protected void CalculateAge_TextChanged()
        {
            Debug.WriteLine($"Birth Time Text Received: {TextBirthTime.Text}"); // Log the value

            if (!string.IsNullOrEmpty(TextDOB.Text) && !string.IsNullOrEmpty(TextBirthTime.Text))
            {
                if (DateTime.TryParse(TextDOB.Text, out DateTime dob))
                {
                    DateTime birthDateTime = DateTime.MinValue;
                    bool timeParsed = false;

                    // Try parsing with various formats
                    string[] formats = { "h:mm tt", "hh:mm tt", "h.mm tt", "hh.mm tt", "HH:mm" };
                    foreach (string format in formats)
                    {
                        if (DateTime.TryParseExact(TextBirthTime.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
                        {
                            birthDateTime = dob.Date + parsedTime.TimeOfDay;
                            timeParsed = true;
                            break;
                        }
                    }

                    if (timeParsed)
                    {
                        DateTime currentDateTime = DateTime.Now;
                        TimeSpan ageSpan = currentDateTime - birthDateTime;
                        //string ageString = $"{ageSpan.Days}d {ageSpan.Hours}h {ageSpan.Minutes}m";
                        string ageString = $"{ageSpan.Days}days";

                        TextAge.Text = ageString;
                    }
                    else
                    {
                        TextAge.Text = "Invalid Time";
                    }
                }
                else
                {
                    TextAge.Text = "Invalid Date";
                }
            }
            else
            {
                TextAge.Text = ""; // Clear age if DOB or Birth Time is empty
            }
        }

        protected void fluidUpdate()
        {
            try
            {
                if (!string.IsNullOrEmpty(TextTpnFluidMl.Text) && !string.IsNullOrEmpty(TextLipid.Text) && !string.IsNullOrEmpty(TextAminovenPer.Text) && !string.IsNullOrEmpty(TextNaclPer.Text)
                    && !string.IsNullOrEmpty(TextKclPer.Text) && !string.IsNullOrEmpty(TextCalciumPer.Text)
                    && !string.IsNullOrEmpty(TextMVI.Text) && !string.IsNullOrEmpty(TextCelcel.Text))
                {

                    // Parse inputs
                    double L7 = double.Parse(TextTpnFluidMl.Text);
                    double B3 = double.Parse(TextLipid.Text);
                    double B7 = double.Parse(TextAminovenPer.Text);
                    double B8 = double.Parse(TextNaclPer.Text);
                    double B9 = double.Parse(TextKclPer.Text);
                    double B10 = double.Parse(TextCalciumPer.Text);
                    double B4 = double.Parse(TextMVI.Text);
                    double B5 = double.Parse(TextCelcel.Text);

                    // Apply formula: (L7 - B3 - B7 - B8 - B9 - B10) - B4 - B5
                    double result = (L7 - B3 - B7 - B8 - B9 - B10) - B4 - B5;

                    // Custom rounding: round to 1 decimal place, round up if hundredths > 5
                    double rounded = Math.Floor(result * 10) / 10;
                    double hundredths = (result * 100) % 10;

                    if (hundredths > 5)
                    {
                        rounded += 0.1;
                    }

                    TextFluidForGlucose.Text = rounded.ToString("0.0");
                }
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }
        protected void Nutritional_TextChanged()
        {
            try
            {


                ////////tfr ml
                if (!string.IsNullOrEmpty(TextTFR.Text))
                {
                    TextNutriationtfr.Text = TextTFR.Text;
                }


                /////TFV
                if (!string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextTFR.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextTFR.Text, out decimal textAValue))
                    {
                        decimal result = dosingWeight * textAValue;
                        TextTfv.Text = result.ToString();
                    }
                    else
                    {

                        TextTfv.Text = "0";
                    }
                }
                ////FEEDS
                if (!string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextFeed.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextFeed.Text, out decimal textAValue))
                    {
                        decimal result = dosingWeight * textAValue;
                        TextFeeds.Text = result.ToString();
                    }
                    else
                    {

                        TextFeeds.Text = "0";
                    }
                }
                /////  IVF kg
                if (!string.IsNullOrEmpty(TextTFR.Text) && !string.IsNullOrEmpty(TextFeed.Text))
                {
                    if (decimal.TryParse(TextTFR.Text, out decimal dosingWeight) && decimal.TryParse(TextFeed.Text, out decimal textAValue))
                    {
                        decimal result = dosingWeight - textAValue;
                        TextivfMlKg.Text = result.ToString();
                    }
                    else
                    {

                        TextivfMlKg.Text = "0";
                    }
                }


                /////  IVF (ml)
                if (!string.IsNullOrEmpty(TextivfMlKg.Text) && !string.IsNullOrEmpty(TextDosingWeight.Text))
                {
                    if (decimal.TryParse(TextivfMlKg.Text, out decimal dosingWeight) && decimal.TryParse(TextDosingWeight.Text, out decimal textAValue))
                    {
                        decimal result = dosingWeight * textAValue;
                        TextivfMl.Text = result.ToString();
                    }
                    else
                    {

                        TextivfMl.Text = "0";
                    }
                }

                ///// TPN fluid (ml)
                if (!string.IsNullOrEmpty(TextivfMlKg.Text) && !string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextIVM.Text))
                {
                    if (decimal.TryParse(TextivfMlKg.Text, out decimal dosingWeight) && decimal.TryParse(TextDosingWeight.Text, out decimal textAValue) && decimal.TryParse(TextIVM.Text, out decimal TextIVMValue))
                    {
                        decimal result = (dosingWeight * textAValue) - TextIVMValue;
                        TextTpnFluidMl.Text = result.ToString();
                    }
                    else
                    {

                        TextTpnFluidMl.Text = "0";
                    }
                }

                ///// TextGlucoseInIvm
                if (!string.IsNullOrEmpty(TextDex10.Text))
                {
                    if (decimal.TryParse(TextDex10.Text, out decimal dosingWeight))
                    {
                        decimal result = dosingWeight * 0.1m; // Use 'm' suffix for decimal literal
                        TextGlucoseInIvm.Text = result.ToString();
                    }
                    else
                    {
                        TextGlucoseInIvm.Text = "0";
                    }
                }


                ///// TPN Glucose (g)
                if (!string.IsNullOrEmpty(TextG.Text) && !string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextGlucoseInIvm.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextG.Text, out decimal TextGValue) && decimal.TryParse(TextGlucoseInIvm.Text, out decimal TextGlucoseInIvmvalue))
                    {
                        decimal result = (dosingWeight * TextGValue * 1.44m) - TextGlucoseInIvmvalue;
                        TextTpnGlucoseG.Text = result.ToString();
                    }
                    else
                    {
                        TextTpnGlucoseG.Text = "0";
                    }
                }







                ///// Na in IVM (mEq/kg/d)
                if (!string.IsNullOrEmpty(TextN5.Text) && !string.IsNullOrEmpty(TextN2.Text) && !string.IsNullOrEmpty(TextNS.Text) && !string.IsNullOrEmpty(TextDosingWeight.Text))
                {
                    if (decimal.TryParse(TextN5.Text, out decimal n5Value) && decimal.TryParse(TextN2.Text, out decimal n2Value) && decimal.TryParse(TextNS.Text, out decimal nsValue)
                        && decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeightValue))
                    {
                        decimal result = ((n5Value * 0.031m) + (n2Value * 0.077m) + (nsValue * 0.154m)) / dosingWeightValue;
                        TextNaInIvm.Text = result.ToString("N2");
                    }
                    else
                    {
                        TextNaInIvm.Text = "0";
                    }
                }
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }
        protected void Lipid_TextChanged()
        {
            try
            {




                ////SYTRING 2 
                if (!string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextA.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextA.Text, out decimal textAValue))
                    {
                        decimal result = 10 * dosingWeight * textAValue;
                        TextAminovenPer.Text = result.ToString();
                    }
                    else
                    {
                        // Handle the case where the text in TextDosingWeight or TextL is not a valid number.
                        // You might want to display an error message to the user.
                        TextAminovenPer.Text = "0"; // Or display a more user-friendly message
                    }
                }


                //////3% NaCl (per)
                if (!string.IsNullOrEmpty(TextNa.Text) && !string.IsNullOrEmpty(TextNaInIvm.Text))
                {
                    Sodium_source();
                }

                ////////15% KCl
                //if (!string.IsNullOrEmpty(TextK.Text) && !string.IsNullOrEmpty(TextPotPhos.Text) && TextDosingWeight.Text != "0")
                //{
                //    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextK.Text, out decimal TextKValue) && decimal.TryParse(TextPotPhos.Text, out decimal TextPotPhosValue))
                //    {
                //        decimal result = (TextKValue - (4.4m * TextPotPhosValue / dosingWeight)) * (dosingWeight / 2);
                //        TextKclPer.Text = result.ToString("N2");
                //    }
                //    else
                //    {
                //        TextKclPer.Text = "0";
                //    }
                //}


                //////10% KCl
                if (TextCalciumViaTPN.Text != "0" && !string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextCa.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextCa.Text, out decimal TextKValue))
                    {
                        decimal result = (dosingWeight * TextKValue) / 9.3m;
                        TextCalciumPer.Text = result.ToString("N2");
                    }
                    else
                    {
                        TextCalciumPer.Text = "0";
                    }
                }

                //////10% KCl
                if (TextCalciumViaTPN.Text == "0" && !string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextCa.Text))
                {


                    TextCalciumPer.Text = "0";

                }

                //////50% MgSO4 (per)
                if (!string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextMg.Text))
                {

                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextMg.Text, out decimal textLValue))
                    {
                        decimal result = (textLValue * dosingWeight) / 12;
                        TextMgso4Per.Text = result.ToString("N2");
                    }

                }


                /////////SYRING 1 
                if (!string.IsNullOrEmpty(TextDosingWeight.Text) && !string.IsNullOrEmpty(TextL.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextL.Text, out decimal textLValue))
                    {
                        decimal result = 5 * dosingWeight * textLValue;
                        TextLipid.Text = result.ToString();
                    }
                    else
                    {
                        // Handle the case where the text in TextDosingWeight or TextL is not a valid number.
                        // You might want to display an error message to the user.
                        TextLipid.Text = "0"; // Or display a more user-friendly message
                    }
                }

                if (!string.IsNullOrEmpty(TextL.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && TextL.Text != "0")
                    {
                        decimal result = 1 * dosingWeight;
                        TextMVI.Text = result.ToString();
                    }
                    else
                    {
                        TextMVI.Text = "0";
                    }
                }
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
            //else
            //{
            //    // Handle the case where TextDosingWeight or TextL is empty.
            //    TextLipid.Text = string.Empty; // Or set it to a default value like "0"
            //}
        }

        protected void Syring1_Celcel_TextChanged()
        {
            try
            {


                if (!string.IsNullOrWhiteSpace(TextCelcelInput.Text))
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && TextCelcelInput.Text != "0")
                    {
                        decimal result = 0.5m * dosingWeight;
                        TextCelcel.Text = result.ToString();
                    }
                    else
                    {
                        TextCelcel.Text = "0";
                    }
                }


                if (!string.IsNullOrEmpty(TextCelcel.Text) || !string.IsNullOrEmpty(TextMVI.Text) || !string.IsNullOrEmpty(TextLipid.Text))
                {
                    if (decimal.TryParse(TextCelcel.Text, out decimal TextCelcelvalue) &&
                        decimal.TryParse(TextMVI.Text, out decimal textLValue) &&
                        decimal.TryParse(TextLipid.Text, out decimal TextLipidvalue))
                    {
                        decimal result = ((TextCelcelvalue + textLValue + TextLipidvalue) / 24);
                        TextSyringe1Total.Text = result.ToString("N2"); // Format to 2 decimal places
                    }
                    else
                    {
                        TextSyringe1Total.Text = "0"; // Handle invalid decimal input
                    }

                }
                else
                {
                    TextSyringe1Total.Text = "0"; // Handle the empty case
                    TextCelcel.Text = "0";
                    TextMVI.Text = "0";
                    //if (decimal.TryParse(TextCelcel.Text, out decimal TextCelcelvalue) &&
                    //    decimal.TryParse(TextMVI.Text, out decimal textLValue) &&
                    //    decimal.TryParse(TextLipid.Text, out decimal TextLipidvalue))
                    //{
                    //    decimal result = ((TextCelcelvalue + textLValue + TextLipidvalue) / 24);
                    //    TextSyringe1Total.Text = result.ToString("N2"); // Format to 2 decimal places
                    //}
                    //else
                    //{
                    //    TextSyringe1Total.Text = "Invalid Input"; // Handle invalid decimal input
                    //}
                }

            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }


        }

        protected void DropdownSodiumSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sodium_source();
        }

        protected void Sodium_source()
        {
            try
            {


                decimal naValue = 0;
                decimal naInIvmValue = 0;
                decimal dosingWeightValue = 0;
                decimal result = 0;

                // Try to parse the values from the TextBoxes.  Use decimal.TryParse to avoid exceptions.
                if (decimal.TryParse(TextNa.Text, out naValue) &&
                    decimal.TryParse(TextNaInIvm.Text, out naInIvmValue) &&
                    decimal.TryParse(TextDosingWeight.Text, out dosingWeightValue))
                {
                    // Get the selected value from the DropDownList.
                    string sodiumSource = DropdownSodiumSource.SelectedValue;

                    if (sodiumSource == "CRL")
                    {
                        result = (naValue - naInIvmValue) * dosingWeightValue / 3;
                    }
                    else
                    {
                        result = (naValue - naInIvmValue) * dosingWeightValue * 2;
                    }
                    // Custom rounding: round to one decimal place, but round up if hundredths > 5
                    decimal rounded = Math.Floor(result * 10) / 10; // get one decimal place without rounding
                    decimal hundredths = (result * 100) % 10; // extract hundredths digit

                    if (hundredths > 5)
                    {
                        rounded += 0.1m;
                    }

                    TextNaclPer.Text = rounded.ToString("0.0"); // display with one decimal place
                }
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }

        protected void Total_TextChanged()
        {
            try
            {


                if (string.IsNullOrEmpty(TextPo4.Text) || TextDosingWeight.Text != "0")
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextPo4.Text, out decimal TextPo4value))
                    {
                        decimal result = (dosingWeight * TextPo4value) / 93;
                        TextPotPhos.Text = result.ToString("N2");
                    }
                    else
                    {
                        TextPotPhos.Text = "0";
                    }
                }

                //////15% KCl
                if (!string.IsNullOrEmpty(TextK.Text) && !string.IsNullOrEmpty(TextPotPhos.Text) && TextDosingWeight.Text != "0")
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) && decimal.TryParse(TextK.Text, out decimal TextKValue) && decimal.TryParse(TextPotPhos.Text, out decimal TextPotPhosValue))
                    {
                        decimal result = (TextKValue - (4.4m * TextPotPhosValue / dosingWeight)) * (dosingWeight / 2);
                        TextKclPer.Text = result.ToString("N2");
                    }
                    else
                    {
                        TextKclPer.Text = "0";
                    }
                }
                if (string.IsNullOrEmpty(TextCalciumViaTPN.Text) || TextCalciumViaTPN.Text == "0")
                {
                    if (decimal.TryParse(TextDosingWeight.Text, out decimal dosingWeight) &&
                        decimal.TryParse(TextCa.Text, out decimal caValue))
                    {
                        decimal result = (caValue * dosingWeight) / 9.3m;
                        TextCalcium10.Text = result.ToString("0.##");
                    }
                    else
                    {
                        TextCalcium10.Text = "0";
                    }
                }
                else
                {
                    TextCalcium10.Text = "0";
                }
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }



        protected void CalculateValues()
        {
            try
            {


                // Parse inputs
                double L8 = double.TryParse(TextTpnGlucoseG.Text, out double valL8) ? valL8 : 0;
                double L9 = double.TryParse(TextFluidForGlucose.Text, out double valL9) ? valL9 : 0;


                // Set F13 = 1 when 5% (value "1") is selected, else F14 = 1
                double F13 = DropdownBaseSolution.SelectedValue == "1" ? 1 : 0;
                double F14 = DropdownBaseSolution.SelectedValue == "0" ? 1 : 0;


                // Set F15 = 1 when 25% (value "1") is selected, else F16 = 1
                double F15 = DropdownConcSolution.SelectedValue == "1" ? 1 : 0;
                double F16 = DropdownConcSolution.SelectedValue == "0" ? 1 : 0;
                // Formula
                double numerator = ((5 * L9 * F16) + (2.5 * L9 * F15)) - (10 * L8);
                double denominator = ((5 * F16) + (2.5 * F15)) - ((0.5 * F13) + F14);

                if (denominator != 0)
                {
                    double result = numerator / denominator;

                    // Custom rounding logic
                    double rounded = Math.Floor(result * 10) / 10;
                    double hundredths = (result * 100) % 10;

                    if (hundredths > 5)
                    {
                        rounded += 0.1;
                    }

                    TextDextroseBasePer.Text = rounded.ToString("0.0", CultureInfo.InvariantCulture);
                }
                else
                {
                    TextDextroseBasePer.Text = "Denominator is zero. Cannot divide.";
                }
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }



        protected void CalculateValues50_Dext()
        {
            try
            {


                double l9 = 0;
                double.TryParse(TextFluidForGlucose.Text?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out l9);

                // Set F13 = 1 when 5% (value "1") is selected, else F14 = 1
                double F13 = DropdownBaseSolution.SelectedValue == "1" ? 1 : 0;
                double F14 = DropdownBaseSolution.SelectedValue == "0" ? 1 : 0;

                string dextroseInput = TextDextroseBasePer.Text?.Trim().Replace("%", "");
                double b12 = 0;
                double.TryParse(dextroseInput, NumberStyles.Any, CultureInfo.InvariantCulture, out b12);

                double l8 = 0;
                double.TryParse(TextTpnGlucoseG.Text?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out l8);

                // Set F15 = 1 when 25% (value "1") is selected, else F16 = 1
                double F15 = DropdownConcSolution.SelectedValue == "1" ? 1 : 0;
                double F16 = DropdownConcSolution.SelectedValue == "0" ? 1 : 0;

                double formulaResult;
                double intermediate = (0.1 * l9 * F14) + (0.05 * l9 * F13);

                if (F15 == 0 && intermediate > l8)
                {
                    formulaResult = 0;
                }
                else
                {
                    formulaResult = l9 - b12;
                }

                // Custom rounding: round to 1 decimal, round up if hundredths > 5
                double rounded = Math.Floor(formulaResult * 10) / 10;
                double hundredths = (formulaResult * 100) % 10;

                if (hundredths > 5)
                {
                    rounded += 0.1;
                }

                TextDextroseConcPer.Text = rounded.ToString("0.0", CultureInfo.InvariantCulture);

                ////// TextDextroseConcPer.Text = formulaResult.ToString("0.##", CultureInfo.InvariantCulture);

                //// Show result (you can bind it to another TextBox or Label)
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Result: " + formulaResult + "');", true);
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }


        //// In your .aspx.cs file
        //protected void DropdownPreNanStrength_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CalculateValues();
        //}

        protected void DropdownBaseSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropdownBaseSolution.SelectedValue == "1")
            {
                LabelDextroseBasePer.Text = "5% Dextrose (per)";
                LabelDextroseBase50ml.Text = "5% Dextrose(50 ml)";
            }
            else if (DropdownBaseSolution.SelectedValue == "0")
            {
                LabelDextroseBasePer.Text = "10% Dextrose (per)";
                LabelDextroseBase50ml.Text = "10 % Dextrose(50 ml)";
                // You might also want to update the value in TextDextroseBasePer here if needed
                // TextDextroseBasePer.Text = GetValueFor10Percent();
            }
        }

        protected void DropdownConcSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropdownConcSolution.SelectedValue == "1")
            {
                LabelDextroseConcPer.Text = "25% Dextrose (per)";
                LabelDextroseConc50ml.Text = "25 % Dextrose(50 ml)";
            }
            else if (DropdownConcSolution.SelectedValue == "0")
            {
                LabelDextroseConcPer.Text = "50% Dextrose (per)";
                LabelDextroseConc50ml.Text = "50 % Dextrose(50 ml)";
                // You might also want to update the value in TextDextroseBasePer here if needed
                // TextDextroseBasePer.Text = GetValueFor10Percent();
            }

        }

        //protected void DropdownTypeOfOralFeed_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CalculateValues();
        //}

        protected void CalculateTotalVolume()
        {
            try
            {


                double aminoven = ParseDouble(TextAminovenPer.Text);
                double nacl = ParseDouble(TextNaclPer.Text);
                double kcl = ParseDouble(TextKclPer.Text);
                double calcium = ParseDouble(TextCalciumPer.Text);
                double mgso4 = ParseDouble(TextMgso4Per.Text);
                double dextroseBase = ParseDouble(TextDextroseBasePer.Text);
                double dextroseConc = ParseDouble(TextDextroseConcPer.Text);

                double total = aminoven + nacl + kcl + calcium + mgso4 + dextroseBase + dextroseConc;

                TextTotalVolume.Text = SmartRound(total).ToString("0.0", CultureInfo.InvariantCulture);


                ////////////50 ml total 


                //double aminoven50 = ParseDouble(TextAminoven50ml.Text);
                //double nacl50 = ParseDouble(TextNacl50ml.Text);
                //double kcl50 = ParseDouble(TextKcl50ml.Text);
                //double calcium50 = ParseDouble(TextCalcium50ml.Text);
                //double mgso450 = ParseDouble(TextMgso450ml.Text);
                //double dextroseBase50 = ParseDouble(TextDextroseBase50ml.Text);
                //double dextroseConc50 = ParseDouble(TextDextroseConc50ml.Text);

                //double total50 = aminoven50 + nacl50 + kcl50 + calcium50 + mgso450 + dextroseBase50 + dextroseConc50;

                // Calculate 50ml total per hour
                double totalPer50 = total / 24.0;
                // Apply custom rounding (round to 1 decimal, round up if hundredths > 5)
                double rounded = Math.Floor(totalPer50 * 10) / 10;
                double hundredths = (totalPer50 * 100) % 10;

                if (hundredths > 5)
                {
                    rounded += 0.1;
                }

                TextTotalPer50.Text = rounded.ToString("0.0", CultureInfo.InvariantCulture);

            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }

        // Helper to safely parse textbox text to double
        private double ParseDouble(string input)
        {
            return double.TryParse(input, out double value) ? value : 0;
        }
        protected void Syringe2_50ml()
        {
            try
            {


                if (!string.IsNullOrEmpty(TextAminovenPer.Text))
                {
                    bool isAminovenValid = decimal.TryParse(TextAminovenPer.Text, out decimal aminovenPer);
                    bool isOverfillValid = decimal.TryParse(TextOverfillFactor.Text, out decimal overfillFactor);
                    bool isTotalVolumeValid = decimal.TryParse(TextTotalVolume.Text, out decimal totalVolume);

                    if (isAminovenValid && isOverfillValid && isTotalVolumeValid && totalVolume != 0)
                    {
                        decimal factor = (50 * overfillFactor) / totalVolume;

                        // Aminoven
                        decimal result = CustomRound(aminovenPer * factor);
                        TextAminoven50ml.Text = result.ToString("0.0");

                        // NaCl
                        decimal.TryParse(TextNaclPer.Text, out decimal naclPer);
                        decimal naclResult = CustomRound(naclPer * factor);
                        TextNacl50ml.Text = naclResult.ToString("0.0");

                        // KCl
                        decimal.TryParse(TextKclPer.Text, out decimal kclPer);
                        decimal kclResult = CustomRound(kclPer * factor);
                        TextKcl50ml.Text = kclResult.ToString("0.0");

                        // Calcium
                        decimal.TryParse(TextCalciumPer.Text, out decimal calciumPer);
                        decimal calciumResult = CustomRound(calciumPer * factor);
                        TextCalcium50ml.Text = calciumResult.ToString("0.0");

                        // MgSO4
                        decimal.TryParse(TextMgso4Per.Text, out decimal mgso4Per);
                        decimal mgso4Result = CustomRound(mgso4Per * factor);
                        TextMgso450ml.Text = mgso4Result.ToString("0.0");

                        // Dextrose Base
                        decimal.TryParse(TextDextroseBasePer.Text, out decimal dextroseBasePer);
                        decimal dextroseBaseResult = CustomRound(dextroseBasePer * factor);
                        TextDextroseBase50ml.Text = dextroseBaseResult.ToString("0.0");

                        // Dextrose Conc
                        decimal.TryParse(TextDextroseConcPer.Text, out decimal dextroseConcPer);
                        decimal dextroseConcResult = CustomRound(dextroseConcPer * factor);
                        TextDextroseConc50ml.Text = dextroseConcResult.ToString("0.0");
                    }
                    else
                    {
                        TextAminoven50ml.Text = "0.0";
                        TextNacl50ml.Text = "0.0";
                    }
                }
            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }
        }


        private decimal CustomRound(decimal value)
        {
            decimal rounded = Math.Floor(value * 10) / 10;
            decimal hundredths = (value * 100) % 10;
            if (hundredths > 5)
            {
                rounded += 0.1m;
            }
            return Math.Round(rounded, 1); // ensure it's exactly one decimal
        }


        protected void Osmolarity()
        {
            try
            {


                decimal.TryParse(TextLipid.Text, out decimal lipid);
                decimal.TryParse(TextAminovenPer.Text, out decimal aminoven);
                decimal.TryParse(TextDextroseBasePer.Text, out decimal dextroseBase);
                decimal.TryParse(TextDextroseConcPer.Text, out decimal dextroseConc);
                decimal.TryParse(TextNaclPer.Text, out decimal nacl);
                decimal.TryParse(TextKclPer.Text, out decimal kcl);

                // Numerator calculation
                decimal numerator = (0.26m * lipid) +
                                    (aminoven * 0.885m) +
                                    (dextroseBase * 0.555m) +
                                    (dextroseConc * 2.78m) +
                                    (nacl * 1.027m) +
                                    (kcl * 4);

                // Denominator calculation
                decimal denominator = lipid + aminoven + nacl + dextroseBase + dextroseConc + kcl;

                decimal result = 0;

                if (denominator != 0)
                {
                    result = (numerator / denominator) * 1000;
                }

                // Example: display in a label or output textbox
                TextOsmolarity.Text = result.ToString("0.##");


                //////Dextrose %
                decimal.TryParse(TextTpnGlucoseG.Text, out decimal TextTpnGlucoseGvalue);

                decimal.TryParse(TextTotalVolume.Text, out decimal TextTotalVolumeGvalue);
                decimal resultmain = 0;
                resultmain = TextTpnGlucoseGvalue * 100 / TextTotalVolumeGvalue;

                TextDextrose.Text = resultmain.ToString("0.##");

                //////CNR %
                decimal.TryParse(TextG.Text, out decimal gcnr); // F8
                decimal.TryParse(TextL.Text, out decimal lcnr); // F7
                decimal.TryParse(TextA.Text, out decimal a); // F6

                decimal cnr = a != 0 ? (6.25m * ((4.9m * gcnr) + (9m * lcnr)) / a) : 0;

                // Display the result (CNR %)
                TextCnr.Text = cnr.ToString("0.##");



                ////////////////////Calories

                // Parsing numeric values
                decimal.TryParse(TextA.Text, out decimal dosingWeight); // F6
                decimal.TryParse(TextL.Text, out decimal l);            // F7
                decimal.TryParse(TextG.Text, out decimal g);            // F8
                decimal.TryParse(TextFeed.Text, out decimal feed);      // F4

                // Getting dropdown values
                string typeOfOralFeed = DropdownTypeOfOralFeed.SelectedValue; // I8
                string preNanStrength = DropdownPreNanStrength.SelectedValue; // I9

                // Calculate formula-specific constants
                decimal formulaMultiplier = 0;
                if (typeOfOralFeed == "Formula")
                    formulaMultiplier = 0.78m;
                else if (typeOfOralFeed == "EBM/PDHM")
                    formulaMultiplier = 0.65m;
                // NPO = 0 by default

                decimal preNanMultiplier = 0;
                if (typeOfOralFeed != "NPO") // only apply this if not NPO
                {
                    switch (preNanStrength)
                    {
                        case "Quarter":
                            preNanMultiplier = 0.04m;
                            break;
                        case "Half":
                            preNanMultiplier = 0.08m;
                            break;
                        case "Full":
                            preNanMultiplier = 0.16m;
                            break;
                    }
                }

                // Final calculation
                decimal resultCalories = (dosingWeight * 4) +
                                 (l * 9) +
                                 (g * 5) +
                                 (feed * formulaMultiplier) +
                                 (feed * preNanMultiplier);

                // Output result
                TextCaloriesForToday.Text = resultCalories.ToString("0.##");




                ////////////Protein 

                // Parse numeric values
                decimal.TryParse(TextA.Text, out decimal dosingWeightP); // F6
                decimal.TryParse(TextFeed.Text, out decimal feedP);       // F4

                // Get selected values
                string typeOfOralFeedP = DropdownTypeOfOralFeed.SelectedValue; // I8
                string preNanStrengthP = DropdownPreNanStrength.SelectedValue; // I9

                // Protein from formula/EBM
                decimal proteinFromFeed = 0;
                if (typeOfOralFeedP == "Formula")
                    proteinFromFeed = 0.019m;
                else if (typeOfOralFeedP == "EBM/PDHM")
                    proteinFromFeed = 0.0095m;

                // Additional protein from PreNan strength
                decimal preNanProtein = 0;
                if (typeOfOralFeedP != "NPO")
                {
                    switch (preNanStrengthP)
                    {
                        case "Quarter":
                            preNanProtein = 0.003m;
                            break;
                        case "Half":
                            preNanProtein = 0.006m;
                            break;
                        case "Full":
                            preNanProtein = 0.012m;
                            break;
                    }
                }

                // Final protein calculation
                decimal totalProtein = dosingWeightP + (feedP * proteinFromFeed) + (feedP * preNanProtein);

                // Output
                TextProteinsForToday.Text = totalProtein.ToString("0.##");



                //////Potphos
                decimal.TryParse(TextPotPhos.Text, out decimal TextPotPhosvalue); // F8
                decimal.TryParse(TextDosingWeight.Text, out decimal TextDosingWeightvalue); // F7


                decimal Potphos = 4.4m * TextPotPhosvalue / TextDosingWeightvalue;

                // Display the result (CNR %)
                TextKInPotphos.Text = Potphos.ToString("0.##");


            }

            catch (Exception ex)
            {

                ExceptionLogging.SendExcepToDB(ex);

            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

            Utility.ClearForm(Page.Form.Controls);
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            // Your any server-side logic here

            //string script = "window.close();";
            //ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", $"<script>{script}</script>");
            Response.Redirect("TransactionPage.aspx"); // or wherever you want to go
        }
        protected void wrongvalue()
        {
            double n5 = ParseDouble1(TextN5.Text);
            double n2 = ParseDouble1(TextN2.Text);
            double ns = ParseDouble1(TextNS.Text);
            double dex10 = ParseDouble1(TextDex10.Text);
            double ivm = ParseDouble1(TextIVM.Text);

            double totalVolume = n5 + n2 + ns + dex10;

            if (totalVolume > ivm)
            {
                LabelMessage.Text = "Wrong IVM volume";
                HighlightTextBoxes(true);
            }
            else
            {
                LabelMessage.Text = "";
                HighlightTextBoxes(false);
            }

        }

        private double ParseDouble1(string input)
        {
            double result;
            return double.TryParse(input, out result) ? result : 0;
        }

        private void HighlightTextBoxes(bool isError)
        {
            string cssClass = isError ? "form-control is-invalid" : "form-control";

            TextN5.CssClass = cssClass;
            TextN2.CssClass = cssClass;
            TextNS.CssClass = cssClass;
            TextDex10.CssClass = cssClass;
        }
        private double SmartRound(double value)
        {
            // Round normally to 1 decimal first
            double rounded = Math.Round(value, 1, MidpointRounding.AwayFromZero);

            // If the decimal part is near 0.9, bump to the next whole number
            double decimalPart = rounded - Math.Floor(rounded);

            if (decimalPart >= 0.9)
            {
                rounded = Math.Ceiling(rounded);
            }

            return rounded;
        }

    }
}