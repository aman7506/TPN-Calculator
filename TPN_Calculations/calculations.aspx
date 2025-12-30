<%@ Page Title="Baby Details & Calculations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="calculations.aspx.cs" Inherits="TNS_Calculations.calculations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
body {
    font-family: Arial, sans-serif;
    background-color: #f4f4f4;
    margin: 20px;
}

.container {
    display: flex;
    gap: 20px;
}

.card {
    background: white;
    padding: 15px;
    border-radius: 8px;
    box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
    width: 45%;
}

h2 {
    color: #0056b3;
    border-bottom: 2px solid #0056b3;
    padding-bottom: 5px;
}

.row {
    display: flex;
    flex-direction: column;
    margin-bottom: 10px;
}

label {
    font-weight: bold;
    margin-bottom: 5px;
}

.input-box {
    width: 100%;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #f1f1f1;
}

    </style>

        <div class="container">
            <div class="card">
                <h2>Syringe 2</h2>
                <div class="row">
                    <label>10% Aminoven (per)</label>
                    <asp:TextBox ID="txtAminovenPer" runat="server" CssClass="input-box" ReadOnly="true" Text="40"></asp:TextBox>
                    <label>10% Aminoven (50 ml)</label>
                    <asp:TextBox ID="txtAminoven50" runat="server" CssClass="input-box" ReadOnly="true" Text="0.8"></asp:TextBox>
                </div>
                <div class="row">
                    <label>3% NaCl (per)</label>
                    <asp:TextBox ID="txtNaClPer" runat="server" CssClass="input-box" ReadOnly="true" Text="11.69"></asp:TextBox>
                    <label>3% NaCl (50 ml)</label>
                    <asp:TextBox ID="txtNaCl50" runat="server" CssClass="input-box" ReadOnly="true" Text="0.23"></asp:TextBox>
                </div>
                <div class="row">
                    <label>15% KCl (per)</label>
                    <asp:TextBox ID="txtKClPer" runat="server" CssClass="input-box" ReadOnly="true" Text="2"></asp:TextBox>
                    <label>15% KCl (50 ml)</label>
                    <asp:TextBox ID="txtKCl50" runat="server" CssClass="input-box" ReadOnly="true" Text="0.04"></asp:TextBox>
                </div>
            </div>
            <div class="card">
                <h2>Total</h2>
                <div class="row">
                    <label>Total Volume (ml)</label>
                    <asp:TextBox ID="txtTotalVolume" runat="server" CssClass="input-box" ReadOnly="true" Text="74.73"></asp:TextBox>
                    <label>Total Per 50 ml</label>
                    <asp:TextBox ID="txtTotalPer50" runat="server" CssClass="input-box" ReadOnly="true" Text="3.11"></asp:TextBox>
                </div>
            </div>
        </div>
   
</asp:Content>
