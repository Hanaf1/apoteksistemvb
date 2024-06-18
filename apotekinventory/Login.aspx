<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="apotekinventory.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login Page</title>
    <!-- Menggunakan bundel Bootstrap CSS -->
    <link href="Content/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.0/font/bootstrap-icons.min.css"/>
    <style>
        /* Custom CSS untuk menyesuaikan halaman */
        body {
            background: url('Images/bg.jpg') no-repeat center center fixed;
            background-size: cover;
            background-color: #f0f0f0; /* Warna latar belakang alternatif jika gambar tidak ada */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center align-items-center" style="height: 100vh;">
                <div class="col-md-4">
                    <!-- Card untuk form login -->
                    <div class="card">
                        <div class="card-body">
                            <h3 class="card-title text-center">Login</h3>
                            <!-- Form untuk input username -->
                            <div class="form-group">
                                <label for="txtUsername">Username:</label>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <!-- Form untuk input password dengan fitur lihat kata sandi -->
                            <div class="form-group">
                                <label for="txtPassword">Password:</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text" onclick="togglePassword()">
                                            <i class="bi bi-eye" id="togglePasswordIcon"></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <!-- Tombol untuk login -->
                            <div class="form-group mt-2">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                            </div>
                            <!-- Label untuk pesan error -->
                            <div class="form-group mt-4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger text-center d-none"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Menggunakan bundel Bootstrap JavaScript -->
    <script src="Content/js/bootstrap.bundle.min.js"></script>
    <script>
        function togglePassword() {
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            var togglePasswordIcon = document.getElementById('togglePasswordIcon');
            if (passwordField.type === 'password') {
                passwordField.type = 'text';
                togglePasswordIcon.classList.remove('bi-eye');
                togglePasswordIcon.classList.add('bi-eye-slash');
            } else {
                passwordField.type = 'password';
                togglePasswordIcon.classList.remove('bi-eye-slash');
                togglePasswordIcon.classList.add('bi-eye');
            }
        }
    </script>
</body>
</html>
