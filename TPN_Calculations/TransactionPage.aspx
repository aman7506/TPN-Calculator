<%@ Page Title="Patient Transactions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransactionPage.aspx.cs" Inherits="TNS_Calculations.TransactionPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />

    <script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        var textbox = document.getElementById('<%= TextBoxUHID.ClientID %>');
        textbox.addEventListener("keydown", function (e) {
            if (e.key === "Enter") {
                e.preventDefault(); // Prevent form submit
                // Optionally trigger a specific function or button click
                // document.getElementById('<%= btnSearch.ClientID %>').click();
            }
        });
    });
    </script>

    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f6f9;
        }

        .page-wrapper {
            margin: 40px auto;
            max-width: 1300px;
        }

        .card-table {
            border-radius: 12px;
            background: #ffffff;
            box-shadow: 0 4px 16px rgba(0, 0, 0, 0.06);
            padding: 20px;
        }

        .table thead {
            background-color: #1e88e5;
            color: white;
        }

        .table th, .table td {
            vertical-align: middle !important;
            font-size: 0.92rem;
        }

        .table tr:hover {
            background-color: #f1f1f1;
        }

        .action-column .btn {
            padding: 6px 10px;
            font-size: 0.85rem;
            margin-right: 4px;
            border-radius: 6px;
            text-decoration: none; /* Prevent underlines on buttons */
        }

        .btn-view {
            background-color: #1976d2;
            color: white;
            border: none;
        }

        .btn-edit {
            background-color: #ffb300;
            color: white;
            border: none;
        }

        .btn-update {
            background-color: #43a047;
            color: white;
            border: none;
        }

        .btn-cancel {
            background-color: #757575;
            color: white;
            border: none;
        }

        .form-control {
            font-size: 0.85rem;
        }

        .table-responsive {
            overflow-x: auto;
        }

        .table-title {
            font-size: 1.4rem;
            font-weight: 600;
            color: #333;
            margin-bottom: 20px;
        }
        .btn-gradient-blue {
    background: linear-gradient(135deg, #1e88e5, #42a5f5);
    color: #fff;
    border: none;
    border-radius: 8px;
    font-weight: 60;
    transition: background 0.3s ease-in-out;
}

.btn-gradient-blue:hover {
    background: linear-gradient(135deg, #1565c0, #2196f3);
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
    color: #fff;
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


.pagination-container {
        text-align: center;
        padding: 10px;
    }

    .pagination-container table {
        margin: 0 auto;
    }

    .pagination-container a, .pagination-container span {
        display: inline-block;
        margin: 0 4px;
        padding: 6px 12px;
        font-size: 14px;
        color: #007bff;
        text-decoration: none;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        transition: all 0.2s ease-in-out;
    }

    .pagination-container a:hover {
        background-color: #007bff;
        color: #fff;
    }

    .pagination-container span {
        background-color: #007bff;
        color: white;
        font-weight: bold;
    }


  .fixed-width {
    width: 110px;
    text-align: center;
    font-weight: bold;
    font-size: 14px;
    box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.3);
    border-radius: 6px;
    padding: 6px 12px;
    transition: all 0.5s ease; /* slower transition (0.5s) and smooth ease */
}

.fixed-width:hover {
    transform: translateY(-3px); /* slight more lift */
    box-shadow: 4px 4px 10px rgba(0, 0, 0, 0.5); /* deeper 3D shadow on hover */
}

.readonly-box {
    background-color: #f0f8ff; /* Light blue or any color you want */
    color: #333;               /* Optional: change text color */
}



    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-wrapper">
        <div class="card-table">
      <asp:Panel ID="pnlSearch" runat="server" CssClass="mb-4">
    <div class="card p-3 mb-4 shadow-sm rounded-3 border-0">
        <div class="row g-3 align-items-end">

            <div class="col-md-3">
                <label for="txtFromDate" class="form-label fw-semibold">
                    <i class="fa-regular fa-calendar-days me-1 text-primary"></i>From Date
                </label>
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" 
                             Text='<%# DateTime.Now.ToString("yyyy-MM-dd") %>' 
                             TextMode="Date" />
            </div>
            <div class="col-md-3">
                <label for="txtToDate" class="form-label fw-semibold">
                    <i class="fa-regular fa-calendar-days me-1 text-primary"></i>To Date
                </label>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" 
                             Text='<%# DateTime.Now.ToString("yyyy-MM-dd") %>' 
                             TextMode="Date" />
            </div>

      <div class="col-md-3">
    <label for="TextBoxUHID" class="form-label fw-semibold">
        <i class="fa-regular fa-id-badge me-1 text-primary"></i> UHID
    </label>
    <asp:TextBox ID="TextBoxUHID" runat="server"
        CssClass="form-control readonly-box"
        />
</div>

            <div class="col-md-3">
              <asp:Button ID="btnSearch" runat="server" 
    CssClass="btn btn-sm btn-primary px-3 py-2 shadow-sm fw-semibold" 
    Text="🔍 Search" 
    OnClick="btnSearch_Click" />
            </div>

        </div>
    </div>
</asp:Panel>

            <div class="table-title">Patient Records</div>
            <div class="table-responsive">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
    CssClass="table table-bordered table-hover"
    OnRowCommand="GridView1_RowCommand"
    OnRowEditing="GridView1_RowEditing"
    OnRowCancelingEdit="GridView1_RowCancelingEdit"
    OnRowDataBound="GridView1_RowDataBound"
    DataKeyNames="id"
    AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"   PagerStyle-CssClass="pagination-container"
    PagerSettings-Mode="Numeric">

                    <Columns>
                        <asp:TemplateField HeaderText="SR. NO.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblField_id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UHID">
                            <ItemTemplate>
                                <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("RegistrationNo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRegNo" CssClass="form-control" runat="server" Text='<%# Bind("RegistrationNo") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Encounter No">
                            <ItemTemplate>
                                <asp:Label ID="lblEncounter" runat="server" Text='<%# Bind("EncounterNO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEncounter" CssClass="form-control" runat="server" Text='<%# Bind("EncounterNO") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admission Date">
                            <ItemTemplate>
                                <asp:Label ID="lblAdmissionDate" runat="server" Text='<%# Bind("AdmissionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAdmissionDate" CssClass="form-control" runat="server" Text='<%# Bind("AdmissionDate", "{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Patient Name">
                            <ItemTemplate>
                                <asp:Label ID="lblPatientName" runat="server" Text='<%# Bind("PatientName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPatientName" CssClass="form-control" runat="server" Text='<%# Bind("PatientName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Age / Gender" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAgeGender" runat="server" Text='<%# Bind("AgeGender") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAgeGender" CssClass="form-control" runat="server" Text='<%# Bind("AgeGender") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="DoctorName">
         <ItemTemplate>
             <asp:Label ID="lblDoctorName" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label>
         </ItemTemplate>
         <EditItemTemplate>
             <asp:TextBox ID="txtDoctorName" CssClass="form-control" runat="server" Text='<%# Bind("DoctorName") %>'></asp:TextBox>
         </EditItemTemplate>
     </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ward" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblWard" runat="server" Text='<%# Bind("WardName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtWard" CssClass="form-control" runat="server" Text='<%# Bind("WardName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bed No">
                            <ItemTemplate>
                                <asp:Label ID="lblBedNo" runat="server" Text='<%# Bind("BedNO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBedNo" CssClass="form-control" runat="server" Text='<%# Bind("BedNO") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                               <asp:TemplateField HeaderText="Days Of TPN" ItemStyle-HorizontalAlign="Center">
           <ItemTemplate>
               <asp:Label ID="lblDaysOfTPN" runat="server" Text='<%# Bind("DaysOfTPN") %>'></asp:Label>
           </ItemTemplate>
           <EditItemTemplate>
               <asp:TextBox ID="txtDaysOfTPN" CssClass="form-control" runat="server" Text='<%# Bind("DaysOfTPN") %>'></asp:TextBox>
           </EditItemTemplate>
       </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Task Date" Visible="false" >
    <ItemTemplate>
        <asp:Label ID="Task_Datelbl" runat="server" Text='<%# Bind("Task_Date") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="Task_Datetxt" CssClass="form-control" runat="server" Text='<%# Bind("Task_Date") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>
                             <asp:TemplateField HeaderText="Status" Visible="false">
         <ItemTemplate>
             <asp:Label ID="FinalSavelbl" runat="server" Text='<%# Bind("FinalSave") %>'></asp:Label>
         </ItemTemplate>
         <EditItemTemplate>
             <asp:TextBox ID="FinalSavetxt" CssClass="form-control" runat="server" Text='<%# Bind("FinalSave") %>'></asp:TextBox>
         </EditItemTemplate>
     </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DOB" Visible="false">
    <ItemTemplate>
        <asp:Label ID="DOBlbl" runat="server" Text='<%# Bind("DOB") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="DOBtxt" CssClass="form-control" runat="server" Text='<%# Bind("DOB") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="action-column">
                    <ItemTemplate>
    <asp:Button ID="btnPending" runat="server" Text="Pending"
        CssClass="btn btn-sm text-white fixed-width" Style="background-color: #ff6f00; border: none;"
        CommandName="PendingDetails" CommandArgument='<%# Eval("id") %>' />

    <asp:Button ID="btnEdit" runat="server" Text="Draft"
        CssClass="btn btn-sm btn-warning text-white ms-1 fixed-width"
        CommandName="Draft" CommandArgument='<%# Eval("id") %>' />

    <asp:Button ID="btnComplete" runat="server" Text="Completed"
        CssClass="btn btn-sm btn-success text-white ms-1 fixed-width"
        CommandName="ViewDetails" CommandArgument='<%# Eval("id") %>' />
</ItemTemplate>


                          
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>