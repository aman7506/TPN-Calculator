<%@ Page Title="New Design" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="new_design.aspx.cs" Inherits="TNS_Calculations.new_design" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
    /* Professional UI Enhancements */
    .container {
        margin-top: 30px;
    }

    .page-title {
        text-align: center;
        color: #0056b3;
        font-size: 24px;
        font-weight: 700;
        text-transform: uppercase;
    }

    .card {
        background: #ffffff;
        border-radius: 12px;
        border: none;
        box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.1);
    }

    .table {
        border-radius: 10px;
        overflow: hidden;
    }

    .table thead {
        background: #343a40;
        color: white;
        font-weight: bold;
    }

    .table tbody tr:hover {
        background: #f8f9fa;
    }

    .entry-box {
        background-color: #fdfdfd;
        border: 1px solid #ccc;
        text-align: center;
        transition: all 0.3s ease;
        border-radius: 6px;
        padding: 6px;
        font-size: 16px;
    }

    .entry-box:focus {
        border: 2px solid #0056b3;
        box-shadow: 0 0 8px rgba(0, 86, 179, 0.2);
    }

    .highlight-box {
        background-color: #ffff99 !important; /* Light yellow */
        border: 2px solid #ffcc00;
        font-weight: bold;
    }

    .display-label {
        background-color: #f8f9fa;
        text-align: center;
        padding: 8px;
        border-radius: 6px;
        border: 1px solid #ddd;
        font-weight: bold;
        color: #333;
    }

    .btn-save {
        background: linear-gradient(135deg, #007bff, #0056b3);
        border: none;
        color: white;
        font-size: 18px;
        padding: 10px 25px;
        border-radius: 8px;
        transition: all 0.3s ease;
        text-transform: uppercase;
        font-weight: bold;
    }

    .btn-save:hover {
        background: linear-gradient(135deg, #0056b3, #003d80);
        box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.2);
    }
</style>

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    $(document).ready(function () {
        $(".entry-box").focus(function () {
            $(this).css("border", "2px solid #007bff").css("box-shadow", "0 0 10px rgba(0, 123, 255, 0.2)");
        }).blur(function () {
            $(this).css("border", "1px solid #ccc").css("box-shadow", "none");
        });
    });
</script>

    <div class="container">
        <h2 class="page-title">Dynamic Data Entry</h2>
        <div class="row justify-content-center">
            <div class="col-md-10">
                <div class="card shadow-lg p-4">
                    <table class="table table-striped">
                        <thead class="table-dark">
                            <tr>
                                <th>Parameter</th>
                                <th>Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Dosing Weight</td>
                                <td><asp:TextBox ID="txtDosingWt" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>TFR (ml/kg)</td>
                                <td><asp:TextBox ID="txtTFR" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Feed (ml/kg)</td>
                                <td><asp:TextBox ID="txtFeed" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>IVM (ml)</td>
                                <td><asp:TextBox ID="txtIVM" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>A (g/kg/day)</td>
                                <td><asp:Label ID="lblA" runat="server" CssClass="form-control display-label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Na (mEq/kg/day)</td>
                                <td><asp:TextBox ID="txtNa" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Mg (mg/kg/day)</td>
                                <td><asp:TextBox ID="txtMg" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>25% Dextrose</td>
                                <td><asp:TextBox ID="txtDextrose25" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Type of Oral</td>
                                <td><asp:Label ID="lblOralType" runat="server" CssClass="form-control display-label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>PreNan Strength</td>
                                <td><asp:Label ID="lblPreNanStr" runat="server" CssClass="form-control display-label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Overfill Factor</td>
                                <td><asp:TextBox ID="txtOverfill" runat="server" CssClass="form-control entry-box"></asp:TextBox></td>
                            </tr>
                            
                            <!-- New Yellow Highlighted Fields -->
                            <tr>
                                <td>PO₄ (mg/kg/d)</td>
                                <td><asp:TextBox ID="txtPO4" runat="server" CssClass="form-control entry-box highlight-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Calcium via TPN</td>
                                <td><asp:TextBox ID="txtCalciumTPN" runat="server" CssClass="form-control entry-box highlight-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Sodium Source</td>
                                <td><asp:TextBox ID="txtSodiumSource" runat="server" CssClass="form-control entry-box highlight-box"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Celcel</td>
                                <td><asp:TextBox ID="txtCelcel" runat="server" CssClass="form-control entry-box highlight-box"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="text-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save Data" CssClass="btn btn-primary btn-save" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

