<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FormularioUsuario.aspx.cs" Inherits="TPCuatrimestral_equipoD.FormularioUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center">

        <div class="col-md-6">

            <div class="card shadow-sm">

                <div class="card-body">

                    <h2 class="mb-4">Usuario</h2>

                    <asp:Label ID="lblError"
                        runat="server"
                        CssClass="alert alert-danger d-block"
                        Visible="false">
                    </asp:Label>


                    <div class="mb-3">
                        <label class="form-label">Nombre de Usuario</label>

                        <asp:TextBox ID="txtUsuario"
                            runat="server"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>


                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>

                        <asp:TextBox ID="txtContrasenia"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Password">
                        </asp:TextBox>
                    </div>


                    <div class="mb-3">
                        <label class="form-label">Nombre</label>

                        <asp:TextBox ID="txtNombre"
                            runat="server"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>


                    <div class="mb-3">
                        <label class="form-label">Apellido</label>

                        <asp:TextBox ID="txtApellido"
                            runat="server"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>


                    <div class="mb-3">
                        <label class="form-label">Perfil</label>

                        <asp:DropDownList ID="ddlPerfil"
                            runat="server"
                            CssClass="form-select">

                            <asp:ListItem Value="1">Gerente</asp:ListItem>
                            <asp:ListItem Value="2">Mesero</asp:ListItem>

                        </asp:DropDownList>
                    </div>


                    <div class="d-flex gap-2">

                        <asp:Button ID="btnAceptar"
                            runat="server"
                            Text="Guardar"
                            CssClass="btn btn-primary"
                            OnClick="btnAceptar_Click" />

                        <a href="Usuarios.aspx"
                            class="btn btn-secondary">
                            Cancelar
                        </a>

                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>