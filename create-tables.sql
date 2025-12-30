CreateScript
------------
CREATE TABLE [Authenticate] ([ID] int NOT NULL, [UID] varchar(50), [Password] varchar(250), [LocationId] varchar(50), [CreatedBy] varchar(50), [CreatedOn] datetime2, [UpdatedBy] varchar(50), [UpdatedOn] datetime, [IsActive] varchar(50), [Role] varchar(50))
CREATE TABLE [Dosing_Wt] ([ID] int NOT NULL, [Dosing_wt_kg] varchar(50), [TFR_ml_kg] varchar(50), [Feed_ml_kg] varchar(50), [IVM_ml] varchar(50), [A_g_kg_day] varchar(50), [L_g_kg_day] varchar(50), [G_mg_kg_min] varchar(50), [Na_mEq_kg_d] varchar(50), [K_m
CREATE TABLE [Syringe_1] ([ID] int NOT NULL, [Lipid_20_percent] varchar(50), [MVI] varchar(50), [Celcel] varchar(50), [CreatedBy] varchar(100), [CreatedOn] datetime, [UpdatedBy] varchar(100), [UpdatedOn] datetime, [LocationId] varchar(50), [Baby_Registrati
CREATE TABLE [Syringe_2] ([ID] int NOT NULL, [Aminoven_10_percent] varchar(50), [NaCl_3_percent] varchar(50), [KCl_15_percent] varchar(50), [Calcium_10_percent] varchar(50), [MgSO4_50_percent] varchar(50), [Syring2_Dextrose_5_percent] varchar(50), [Syring2
CREATE TABLE [Tbl_ExceptionLoggingToDataBase] ([Logid] bigint NOT NULL, [ExceptionMsg] varchar(100), [ExceptionType] varchar(100), [ExceptionSource] nvarchar(MAX), [ExceptionURL] varchar(100), [Logdate] datetime);
CREATE TABLE [Total_Calculations] ([ID] int NOT NULL, [TFR_ml_kg] varchar(50), [TFV_ml] varchar(50), [Feed_ml] varchar(50), [IVF_ml_kg] varchar(50), [IVF_ml] varchar(50), [TPN_fluid_ml] varchar(50), [TPN_Glucose_g] varchar(50), [Fluid_for_Glucose] varchar(
CREATE TABLE [Trigerred_Data] ([ID] int NOT NULL, [EncounterId] varchar(50), [RegistrationNo] varchar(50), [EncounterNo] varchar(50), [RegistrationID] varchar(50), [AdmissionDate] varchar(50), [Dischargedate] varchar(50), [PatientName] varchar(255), [Facil
CREATE TABLE [Usermaster] ([Uid] int NOT NULL, [EmpID] varchar(50), [Name] varchar(50), [Locationid] varchar(50), [CreatedOn] varchar(50), [Createdby] varchar(50), [UpdatedOn] varchar(50), [Updatedby] varchar(50), [IsActive] varchar(50), [EmpType] varchar(
CREATE TABLE [Users] ([Id] int NOT NULL, [Name] varchar(50));

(9 rows affected)
