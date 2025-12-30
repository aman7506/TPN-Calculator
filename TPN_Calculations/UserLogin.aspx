<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="TNS_Calculations.UserLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Login</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap" rel="stylesheet" />

<style>
    body {
        background-color: #e0f2f7; /* Very light cyan background */
        font-family: 'Inter', sans-serif;
        display: flex;
        flex-direction: column;
        min-height: 100vh;
        margin: 0;
    }

    .hospital-header {
        background: linear-gradient(to bottom, #00acc1, #008080); /* Gradient teal header */
        color: white;
        padding: 1rem 2rem;
        text-align: center;
        border-bottom: 1px solid #b2dfdb;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Subtle header shadow */
    }

    .hospital-header-content {
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .hospital-logo {
        max-height: 50px;
        margin: 0 1rem;
    }

    .hospital-info {
        text-align: center;
    }

    .login-wrapper {
        flex-grow: 1;
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 2rem;
    }

    .login-box {
        background-color: #ffffff;
        border-radius: 8px;
        padding: 2rem;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15); /* More pronounced box shadow */
        width: 100%;
        max-width: 400px;
        border: 1px solid #b2dfdb;
    }

    .login-title {
        font-weight: 600;
        color: #008080;
        margin-bottom: 1.5rem;
        text-align: center;
        text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.05); /* Subtle text shadow */
    }

    .form-label {
        font-weight: 500;
        color: #495057;
    }

    .form-control {
        border: 1px solid #ced4da;
        border-radius: 4px;
        padding: 0.75rem 1rem;
        margin-bottom: 1rem;
        font-size: 1rem;
        box-shadow: inset 0 1px 2px rgba(0, 0, 0, 0.05); /* Subtle inner shadow */
    }

    .form-control:focus {
        border-color: #00acc1; /* Slightly brighter teal on focus */
        box-shadow: 0 0 0 0.2rem rgba(0, 172, 193, 0.25); /* Brighter focus shadow */
    }

    .btn-primary {
        background: linear-gradient(to bottom, #00acc1, #008080); /* Gradient teal button */
        border: none; /* Remove default border for gradient effect */
        color: #fff;
        padding: 0.75rem 1.5rem;
        border-radius: 4px;
        font-weight: 500;
        font-size: 1rem;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.15); /* Button shadow */
    }

    .btn-primary:hover {
        background: linear-gradient(to bottom, #008080, #00acc1); /* Reverse gradient on hover */
        box-shadow: 0 3px 6px rgba(0, 0, 0, 0.2); /* Slightly stronger hover shadow */
    }

    .btn-primary:active {
        box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.2); /* Inset shadow on click */
    }

    .text-danger {
        color: #dc3545 !important;
    }

    .mt-3 {
        margin-top: 1.5rem !important;
    }

    .d-block {
        display: block !important;
    }

    .text-center {
        text-align: center !important;
    }

    .hospital-logo-container {
        display: flex;
        align-items: center;
        justify-content: center;
        margin-bottom: 1.5rem;
    }

    .hospital-logo-login {
        max-height: 80px;
    }

    footer {
        background-color: #f8f9fa;
        padding: 0.5rem 0;
        text-align: center;
        color: #6c757d;
        border-top: 1px solid #dee2e6;
        box-shadow: 0 -1px 2px rgba(0, 0, 0, 0.05); /* Subtle top shadow on footer */
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hospital-header">
            <div class="hospital-header-content">
                <img src="Image/logo.png" alt="Hospital Logo" class="hospital-logo" />
                <div class="hospital-info">
                    <h5 class="mb-0 fw-bold">SRI BALAJI ACTION MEDICAL INSTITUTE</h5>
                    <small class="d-block">A - 4, Paschim Vihar, New Delhi - 110063</small>
                </div>
                <img src="Image/nabh.png" alt="NABH Logo" class="hospital-logo" />
            </div>
        </div>

        <div class="login-wrapper">
            <div class="login-box">
                <div class="hospital-logo-container">
                    <img src="Image/logo.png" alt="Hospital Main Logo" class="hospital-logo-login" />
                </div>
                <h4 class="login-title">User Login</h4>

                <div class="mb-3">
                    <label for="txtUsername" class="form-label">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter your username" />
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password" />
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />

                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block text-center" Visible="false" />
            </div>
        </div>

        <footer class="bg-light py-3 text-center text-muted">
            <small>&copy; <%= DateTime.Now.Year %> SRI BALAJI ACTION MEDICAL INSTITUTE</small>
        </footer>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>