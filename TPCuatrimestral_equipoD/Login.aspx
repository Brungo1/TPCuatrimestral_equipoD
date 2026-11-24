<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPCuatrimestral_equipoD.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Resto WEB - Iniciar Sesion</title>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="min-vh-100 d-flex align-items-center justify-content-center">
            <div class="card shadow-sm" style="width: 100%; max-width: 400px;">
                <div class="card-body p-4">
                    <div class="text-center mb-4">
                        <h2 class="fw-bold mb-1">Resto WEB</h2>
                        <p class="text-secondary mb-0">Iniciar Sesion</p>
                    </div>

                    <asp:Label
                        ID="lblError"
                        runat="server"
                        CssClass="alert alert-danger d-block"
                        Visible="false">
                    </asp:Label>

                    <div class="mb-3">
                        <label class="form-label">Usuario</label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario" />
                    </div>

                    <div class="mb-4">
                        <label class="form-label">Contrase&ntilde;a</label>
                        <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password" placeholder="Contrase&#241;a" />
                    </div>

                    <asp:Button
                        ID="btnIngresar"
                        runat="server"
                        Text="Ingresar"
                        CssClass="btn btn-primary w-100"
                        OnClick="btnIngresar_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

